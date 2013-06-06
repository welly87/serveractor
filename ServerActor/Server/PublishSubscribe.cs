using System;
using Messages;

namespace Server
{
    public class PublishSubscribe : IProtocolXProcessor
    {
        // we have a state here
        public void Handle(IMessage message)
        {
            // we can attach the message processor to processs the message that can process parallel 

            throw new NotImplementedException();
            
        }
    }
}