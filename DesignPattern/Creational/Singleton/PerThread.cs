using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DesignPattern.Creational.Singleton
{
    public sealed class PerThreadSingleton
    {
        private static ThreadLocal<PerThreadSingleton> _threadInstance
            = new ThreadLocal<PerThreadSingleton>(
                () => new PerThreadSingleton());

        public int Id;

        private PerThreadSingleton()
        {
            Id = Thread.CurrentThread.ManagedThreadId;
        }

        public static PerThreadSingleton Instance => _threadInstance.Value;
    }

    [TestClass]
    public class PerThread : IExecute
    {
        [TestMethod]
        public void Execute()
        {
            var t1 = Task.Factory.StartNew(() => { Console.WriteLine($"t1: " + PerThreadSingleton.Instance.Id); });
            var t2 = Task.Factory.StartNew(() =>
            {
                Console.WriteLine($"t2: " + PerThreadSingleton.Instance.Id);
                Console.WriteLine($"t2 again: " + PerThreadSingleton.Instance.Id);
            });
            Task.WaitAll(t1, t2);
        }
    }
}