using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections;

namespace DesignPattern.Behavioral.Iterator
{
    [TestClass]
    public class ArrayBackedProperties : IExecute
    {
        public class Creature : IEnumerable<int>
        {
            private int[] stats = new int[3];

            private const int strength = 0;

            public int Strength
            {
                get => stats[strength];
                set => stats[strength] = value;
            }

            public int Agility { get; set; }
            public int Intelligence { get; set; }

            public double AverageStat =>
                stats.Average();

            public IEnumerator<int> GetEnumerator()
            {
                return stats.AsEnumerable().GetEnumerator();
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }

            public int this[int index]
            {
                get { return stats[index]; }
                set { stats[index] = value; }
            }
        }

        [TestMethod]
        public void Execute()
        {
            throw new NotImplementedException();
        }
    }
}