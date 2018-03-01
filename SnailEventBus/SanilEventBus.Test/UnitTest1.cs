using System;
using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SnailEventBus;

namespace SanilEventBus.Test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void RegisterTest()
        {
            //EventBus.Instance.Register<OrderEvent>(new EmailHandler());
            EventBus.Instance.Register<OrderEvent>(new EmailHandler<OrderEvent>());
            //UnRegisterEventHandler();
            //PublishTest();

            UnRegisterEvent();
        }

        [TestMethod]
        public void PublishTest()
        {
            EventBus.Instance.Publish<OrderEvent>(new OrderEvent() { MyProperty = 10 });
        }

        public void UnRegisterEventHandler()
        {
            EventBus.Instance.UnRegisterHandler<OrderEvent>(new EmailHandler<OrderEvent>());
        }

        public void UnRegisterEvent()
        {
            EventBus.Instance.UnRegisterEvent<OrderEvent>();
        }
    }

    public class OrderEvent : IEvent
    {
        public int MyProperty { get; set; }
    }

    public class EmailHandler : IEventHandler<OrderEvent>
    {
        public void Excute(OrderEvent eve)
        {
            throw new NotImplementedException();
        }
    }

    public class EmailHandler<TEvent> : IEventHandler<TEvent> where TEvent : class, IEvent
    {
        public void Excute(TEvent eve)
        {
            if (eve is OrderEvent)
            {

            }
            throw new NotImplementedException();
        }
    }
}
