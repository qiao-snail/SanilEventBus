using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnailEventBus
{
    public sealed class EventBus
    {

        private EventBus() { }
        private IEventStore _store;

        private static EventBus _instance;
        public static EventBus Instance
        {
            get
            {
                return _instance ?? (_instance = new EventBus()
                {
                    _store = new InMemoryEventStore()
                });
            }
        }

        public void Register<TEvent>(IEventHandler<TEvent> handler) where TEvent : class, IEvent
        {
            _store.Register<TEvent>(handler);
        }

        public void UnRegisterHandler<TEvent>(IEventHandler<TEvent> handler) where TEvent : IEvent
        {
            _store.UnRegisterHandler<TEvent>(handler);
        }

        public void UnRegisterEvent<TEvent>() where TEvent : IEvent
        {
            _store.UnRegisterEvent<TEvent>();
        }

        public void Publish<TEvent>(TEvent tevent) where TEvent : class, IEvent
        {
            _store.Publish<TEvent>(tevent);
        }
    }
}