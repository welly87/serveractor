
namespace Server
{
    interface IProtocolXProcessor
    {
        void Handle(Messages.IMessage message);
    }
}
