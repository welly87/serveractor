using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SuperSocket.ClientEngine;
using WebSocket4Net;
using System.Threading;
using Messages;
using ServiceStack.Text;

namespace Client
{
    class Program
    {
        private static WebSocket websocket;

        private static IList<WebSocket> lotsClient = new List<WebSocket>();

        static void Main(string[] args)
        {
            websocket = new WebSocket("ws://localhost:2012/");
            websocket.Opened += new EventHandler(websocket_Opened);
            websocket.Error += new EventHandler<ErrorEventArgs>(websocket_Error);
            websocket.Closed += new EventHandler(websocket_Closed);
            websocket.MessageReceived += websocket_MessageReceived;
            websocket.Open();

            Console.ReadLine();
        }

        static void websocket_MessageReceived(object sender, MessageReceivedEventArgs e)
        {
            Console.WriteLine("Received : " + e.Message);
        }

        private static void websocket_Closed(object sender, EventArgs e)
        {
            Console.WriteLine("Closed");
        }

        private static void websocket_Error(object sender, ErrorEventArgs e)
        {
            Console.WriteLine("Error");
        }

        private static void websocket_Opened(object sender, EventArgs e)
        {
            Thread.Sleep(1000);
            Console.WriteLine("Open");

            for (int i = 0; i < 100; i++)
            {
                websocket.Send(Serialize(new SyalomMessage
                    {
                        Nats = "Halelya"
                    }));
                Console.WriteLine("Sending");
            }
        }

        private static string Serialize(IMessage message)
        {
            return JsonSerializer.SerializeToString(message);
        }
    }
}
