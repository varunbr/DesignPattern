using Microsoft.VisualStudio.TestTools.UnitTesting;
using static System.Console;

namespace DesignPattern.Behavioral.Observer
{
    [TestClass]
    public class ObserverInterfaces : IExecute
    {
        [TestMethod]
        public void Execute()
        {
            var person = new Person();

            var observer = new Observer();

            using var sub = person.Subscribe(observer);

            // OR using Rx - Reactive Extensions
            //person.OfType<FallsIllEvent>().Subscribe(args => WriteLine($"A doctor has been called to {args.Address}"));

            person.CatchACold();
        }

        public class Observer : IObserver<Event>
        {
            public void OnNext(Event value)
            {
                if (value is FallsIllEvent args)
                    WriteLine($"A doctor has been called to {args.Address}");
            }

            public void OnError(Exception error)
            {
            }

            public void OnCompleted()
            {
            }
        }


        public class Event
        {
        }

        public class FallsIllEvent : Event
        {
            public string Address;
        }

        public class Person : IObservable<Event>
        {
            private readonly HashSet<Subscription> subscriptions
                = new HashSet<Subscription>();

            public IDisposable Subscribe(IObserver<Event> observer)
            {
                var subscription = new Subscription(this, observer);
                subscriptions.Add(subscription);
                return subscription;
            }

            public void CatchACold()
            {
                foreach (var sub in subscriptions)
                    sub.Observer.OnNext(new FallsIllEvent { Address = "123 London Road" });
            }

            private class Subscription : IDisposable
            {
                private Person person;
                public IObserver<Event> Observer;

                public Subscription(Person person, IObserver<Event> observer)
                {
                    this.person = person;
                    Observer = observer;
                }

                public void Dispose()
                {
                    person.subscriptions.Remove(this);
                }
            }
        }
    }
}