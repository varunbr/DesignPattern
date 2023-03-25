using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DesignPattern.Structural.Decorator
{
    public interface IBird
    {
        int Weight { get; set; }
        void Fly();
    }

    public class Bird : IBird
    {
        public int Weight { get; set; }

        public void Fly()
        {
            Console.WriteLine($"Crawling in the dirt with weight  {Weight}");
        }
    }

    public interface ILizard
    {
        public int Weight { get; set; }
        void Crawl();
    }

    public class Lizard : ILizard
    {
        public int Weight { get; set; }

        public void Crawl()
        {
            Console.WriteLine($"Crawling in the dirt with weight {Weight}");
        }
    }

    public class Dragon : ILizard, IBird
    {
        private Bird bird;
        private Lizard lizard;
        private int weight;

        public Dragon()
        {
            bird = new Bird();
            lizard = new Lizard();
        }

        public int Weight
        {
            get => weight;
            set
            {
                weight = value;
                bird.Weight = value;
                lizard.Weight = weight;
            }
        }

        public void Crawl()
        {
            lizard.Crawl();
        }

        public void Fly()
        {
            bird.Fly();
        }
    }

    [TestClass]
    public class MultipleInheritance : IExecute
    {
        [TestMethod]
        public void Execute()
        {
            var d = new Dragon();
            d.Crawl();
            d.Fly();
        }
    }
}