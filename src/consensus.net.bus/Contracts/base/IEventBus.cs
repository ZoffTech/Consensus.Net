using System;
using System.Collections.Generic;
using System.Text;

namespace consensus.net.bus.Contracts
{
    interface IEventBus
    {
        void Publish(Event @event);

        void Subscribe<T, TH>() where T : Event where TH : IIntegrationEventHandler<T>;

        void SubscribeDynamic<TH>(string eventName) where TH : IDynamicIntegrationEventHandler;

        void UnsubscribeDynamic<TH>(string eventName) where TH : IDynamicIntegrationEventHandler;

        void Unsubscribe<T, TH>() where TH : IIntegrationEventHandler<T> where T : Event;
    }
}
