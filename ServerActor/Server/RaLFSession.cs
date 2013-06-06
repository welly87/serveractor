using Messages;
using SuperWebSocket;

namespace Server
{
    public class RaLFSession
    {
        public WebSocketSession WSSession { get; set; }

        private RaLFProtocol _protocol;

        public RaLFSession()
        {
            _protocol = new RaLFProtocol();
        }

        public void Handle(IMessage message)
        {
            // guard until common protocol sucess. it also has a 

            _protocol.Handle(message);

            //_protocolProcessors[message.Protocol].Handle(message);
        }
    }
}
