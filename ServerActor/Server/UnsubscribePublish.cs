using System;

namespace Server
{
    public class UnsubscribePublish : IProtocolXProcessor
    {
        // spesified the message that can be handle by this protocol

        // we have a state here

        public void Handle(Messages.IMessage message)
        {
            // we can attach the message processor to processs the message that can process parallel 
            
            throw new NotImplementedException();
        }
    }
}
