using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnailEventBus
{
    /// <summary>
    /// 事件管理接口
    /// </summary>
    public interface IEventStore
    {
        /// <summary>
        /// 注册一个事件
        /// </summary>
        /// <typeparam name="TEvent"></typeparam>
        /// <param name="act"></param>
        void Register<TEvent>(IEventHandler<TEvent> handler) where TEvent : class, IEvent;
        /// <summary>
        /// 注销领域
        /// </summary>
        /// <typeparam name="TEvent"></typeparam>
        void UnRegisterEvent<TEvent>() where TEvent : IEvent;

        /// <summary>
        /// 注销领域事件
        /// </summary>
        /// <typeparam name="TEvent"></typeparam>
        /// <param name="act"></param>
        void UnRegisterHandler<TEvent>(IEventHandler<TEvent> handler) where TEvent : IEvent;

        /// <summary>
        /// 发布事件
        /// </summary>
        /// <typeparam name="Tevent"></typeparam>
        /// <param name="eventItem"></param>
        void Publish<Tevent>(Tevent eventItem) where Tevent : class, IEvent;
        /// <summary>
        /// 异步发布事件
        /// </summary>
        /// <typeparam name="TEvent"></typeparam>
        /// <param name="eventItem"></param>
        void PublishAsync<TEvent>(TEvent eventItem) where TEvent : IEvent;
    }

}
