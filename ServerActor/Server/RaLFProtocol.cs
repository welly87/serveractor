using System.Collections.Generic;
using Messages;

namespace Server
{
    public class RaLFProtocol
    {
        private readonly IDictionary<int, IProtocolXProcessor> _protocolProcessors = new Dictionary<int, IProtocolXProcessor>();

        public RaLFProtocol()
        {
            _protocolProcessors.Add(1, new UnsubscribePublish());
            _protocolProcessors.Add(2, new PublishSubscribe());
        }

        // this protocol can have state 

        internal void Handle(IMessage message)
        {
            if (IsRaLFProtocolMessage(message.Protocol))
            {
                HandleInternal(message);
            }
            else // subprotocol message
            {
                // check if the ralf protocol message allowed executing subprotocol

                _protocolProcessors[message.Protocol].Handle(message);
            }
        }

        private void HandleInternal(IMessage message)
        {
            // let state handle the message
        }

        private bool IsRaLFProtocolMessage(int p)
        {
            return p == 0;
        }
    }
}