namespace consensus.net.bus.Contracts
{
    /// <summary>
    /// Event Bus interface
    /// </summary>
    public interface IEventBus
    {
        void Start();

        void Publish<T>(T @event) where T: class;

        void Subscribe<T, TH>() where T : Event, new() where TH : EventHandler<T>, new();

        void SubscribeGeneric<TH>(string eventName) where TH : GenericEventHandler, new();

        void UnsubscribeGeneric<TH>(string eventName) where TH : GenericEventHandler;

        void Unsubscribe<T, TH>() where TH : EventHandler<T>, new() where T : Event, new();
    }
}