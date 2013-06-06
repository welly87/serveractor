using System;
using System.Threading.Tasks.Dataflow;
using ServiceStack.Text;
using SuperWebSocket;
using System.Threading;
using Messages;

namespace Server
{
    internal class Program
    {
        private static readonly ActionBlock<IncomingMessage> Processors = new ActionBlock<IncomingMessage>(msg =>
            {
                msg.Session.Send(msg.Message);
                Thread.Sleep(2000);
            }, new ExecutionDataflowBlockOptions
                {
                    MaxDegreeOfParallelism = 100
                });

        private static void Main(string[] args)
        {
            Console.WriteLine("Press any key to start the WebSocketServer!");

            Console.ReadKey();
            Console.WriteLine();

            var appServer = new WebSocketServer();
            
            //Setup the appServer
            if (!appServer.Setup(2012)) //Setup with listening port
            {
                Console.WriteLine("Failed to setup!");
                Console.ReadKey();
                return;
            }

            appServer.NewSessionConnected += appServer_NewSessionConnected;
            appServer.NewMessageReceived += appServer_NewMessageReceived;

            Console.WriteLine();

            //Try to start the appServer
            if (!appServer.Start())
            {
                Console.WriteLine("Failed to start!");
                Console.ReadKey();
                return;
            }

            Console.WriteLine("The server started successfully, press key 'q' to stop it!");

            while (Console.ReadKey().KeyChar != 'q')
            {
                Console.WriteLine();
                continue;
            }

            //Stop the appServer
            appServer.Stop();

            //_processors = new ActionBlock<string>();

            Console.WriteLine();
            Console.WriteLine("The server was stopped!");
            Console.ReadKey();
        }

        static void appServer_NewSessionConnected(WebSocketSession session)
        {
            session.Items.Add("session", new RaLFSession
                {
                    WSSession = session
                });
        }

        private static void appServer_NewMessageReceived(WebSocketSession session, string rawMessage)
        {
            //Send the received message back
            //session.Send("Server: " + message + " " + Thread.CurrentThread.ManagedThreadId);
            //Thread.Sleep(2000);

            var message = Deserialize(rawMessage);

            var rlfSession = (RaLFSession) session.Items["session"];
            rlfSession.Handle(message);

            //Processors.SendAsync(new IncomingMessage
            //    {
            //        Message = message,
            //        Session = session
            //    });
        }

        private static IMessage Deserialize(string rawMessage)
        {
            return JsonSerializer.DeserializeFromString<IMessage>(rawMessage);
        }
    }
}