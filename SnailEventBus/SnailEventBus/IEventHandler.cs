using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnailEventBus
{
    /// <summary>
    /// 执行事件接口（领域事件）
    /// </summary>
    public interface IEventHandler<TEvent> where TEvent : IEvent
    {
        /// <summary>
        /// 执行事件
        /// </summary>
        /// <param name="eve">事件源</param>
        void Excute(TEvent eve);
    }
}
