using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnailEventBus
{
    /// <summary>
    /// 事件参数
    /// </summary>
    public interface IEventData
    {
        /// <summary>
        /// 事件触发事件
        /// </summary>
        DateTime EventTime { get; set; }
        /// <summary>
        /// 事件触发源（领域）
        /// </summary>
        Object EventSource { get; set; }
    }
}
