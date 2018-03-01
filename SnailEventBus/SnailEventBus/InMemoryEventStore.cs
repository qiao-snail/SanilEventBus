using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnailEventBus
{
    /// <summary>
    /// 内存事件管理
    /// </summary>
    public class InMemoryEventStore : IEventStore
    {
        /// <summary>
        /// 源和事件的字典
        /// </summary>
        private readonly Dictionary<Type, List<object>> _registeredEventsDic = new Dictionary<Type, List<object>>();

        public void Publish<TEvent>(TEvent tevent) where TEvent : class, IEvent
        {
            if (_registeredEventsDic.ContainsKey(typeof(TEvent)))
            {
                var list = _registeredEventsDic[typeof(TEvent)];
                foreach (var item in list)
                {
                    var s = item as IEventHandler<TEvent>;
                    s.Excute(tevent);
                }
            }
        }
        private int _timeOut = 4000;
        public void PublishAsync<TEvent>(TEvent eventItem) where TEvent : IEvent
        {
            List<Task> taskList = new List<Task>();
            if (_registeredEventsDic.ContainsKey(typeof(TEvent)))
            {
                var events = _registeredEventsDic[typeof(TEvent)];
                //foreach (var item in events)
                //{
                //    var s = item as IEventHandler<TEvent>;
                //    taskList.Add(Task.Run(() => s.Excute(eventItem)));
                //}

                //events.ForEach((e) => taskList.Add(Task.Run(() =>
                //      (e as IEventHandler<TEvent>).Excute(eventItem))));


                Parallel.ForEach(events, e => taskList.Add(Task.Run(() =>
                {
                    (e as IEventHandler<TEvent>).Excute(eventItem);
                })));


                if (_timeOut > 0)
                    Task.WaitAll(taskList.ToArray(), _timeOut);
                else
                    Task.WaitAll(taskList.ToArray());
            }
        }

        public void Register<TEvent>(IEventHandler<TEvent> handler) where TEvent : class, IEvent
        {
            if (handler == null)
                throw new ArgumentNullException(nameof(handler));
            if (!_registeredEventsDic.ContainsKey(typeof(TEvent)))
            {
                _registeredEventsDic.Add(typeof(TEvent), new List<object>() { handler });
            }
            else
            {
                _registeredEventsDic[typeof(TEvent)].Add(handler);
            }
        }

        public void UnRegister<TEvent>() where TEvent : IEvent
        {
            throw new NotImplementedException();
        }

        public void UnRegisterEvent<TEvent>() where TEvent : IEvent
        {
            if (_registeredEventsDic.ContainsKey(typeof(TEvent)))
            {
                _registeredEventsDic.Remove(typeof(TEvent));
            }

        }

        public void UnRegisterHandler<TEvent>(IEventHandler<TEvent> handler) where TEvent : IEvent
        {
            if (_registeredEventsDic.ContainsKey(typeof(TEvent)))
            {
                var list = _registeredEventsDic[typeof(TEvent)];
                var item = list.FirstOrDefault(x => x.ToString() == handler.ToString());
                list.Remove(item);
            }
        }
    }
}
