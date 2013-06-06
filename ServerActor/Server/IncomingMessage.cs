using SuperWebSocket;

namespace Server
{
    public class IncomingMessage
    {
        public string Message { get; set; }

        public WebSocketSession Session { get; set; }
    }
}