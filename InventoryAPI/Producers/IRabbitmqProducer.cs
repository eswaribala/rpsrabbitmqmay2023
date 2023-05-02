namespace InventoryAPI.Producers
{
    public interface IRabbitmqProducer
    {
        void SendMessage<T>(T message);
    }
}
