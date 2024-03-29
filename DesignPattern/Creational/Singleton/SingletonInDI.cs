﻿using Autofac;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static System.Console;

namespace DesignPattern.Creational.Singleton
{
    [TestClass]
    public class SingletonInDi : IExecute
    {
        public class Foo
        {
            public EventBroker Broker;

            public Foo(EventBroker broker)
            {
                Broker = broker ?? throw new ArgumentNullException(paramName: nameof(broker));
            }
        }

        public class EventBroker
        {
        }

        [TestMethod]
        public void Execute()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<EventBroker>().SingleInstance();
            builder.RegisterType<Foo>();

            using (var c = builder.Build())
            {
                var foo1 = c.Resolve<Foo>();
                var foo2 = c.Resolve<Foo>();

                WriteLine(ReferenceEquals(foo1, foo2));
                WriteLine(ReferenceEquals(foo1.Broker, foo2.Broker));
            }
        }
    }
}