using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DesignPattern.Behavioral.Observer
{
    [TestClass]
    public class ObserverViaEvent : IExecute
    {
        [TestMethod]
        public void Execute()
        {
            var person = new Person();

            person.FallsIll += CallDoctor;

            person.CatchACold();
        }

        private static void CallDoctor(object sender, FallsIllEventArgs eventArgs)
        {
            Console.WriteLine($"A doctor has been called to {eventArgs.Address}");
        }

        public class FallsIllEventArgs
        {
            public string Address;
        }

        public class Person
        {
            public void CatchACold()
            {
                FallsIll?.Invoke(this,
                    new FallsIllEventArgs { Address = "123 London Road" });
            }

            public event EventHandler<FallsIllEventArgs> FallsIll;
        }
    }
}