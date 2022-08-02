namespace BasePlugin.Interfaces
{
    public interface IMessageSender
    {
        void SendMessage(string userId, string data);
    }
}