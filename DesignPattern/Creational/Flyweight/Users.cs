using JetBrains.dotMemoryUnit;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DesignPattern.Creational.Flyweight
{
    public class User
    {
        private string fullName;

        public User(string fullName)
        {
            this.fullName = fullName;
        }
    }

    public class User2
    {
        static List<string> strings = new List<string>();
        private int[] names;

        public User2(string fullName)
        {
            int getOrAdd(string s)
            {
                int idx = strings.IndexOf(s);
                if (idx != -1) return idx;
                else
                {
                    strings.Add(s);
                    return strings.Count - 1;
                }
            }

            names = fullName.Split(' ').Select(getOrAdd).ToArray();
        }

        public string FullName => string.Join(" ", names);
    }

    [TestClass]
    public class DemoUsers : IExecute
    {
        public void ForceGC()
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();
        }

        public static string RandomString()
        {
            Random rand = new Random();
            return new string(
                Enumerable.Range(0, 10).Select(i => (char)('a' + rand.Next(26))).ToArray());
        }

        [TestMethod]
        public void TestUser()
        {
            var users = new List<User>();

            var firstNames = Enumerable.Range(0, 100).Select(_ => RandomString());
            var lastNames = Enumerable.Range(0, 100).Select(_ => RandomString());

            foreach (var firstName in firstNames)
            foreach (var lastName in lastNames)
                users.Add(new User($"{firstName} {lastName}"));

            ForceGC();

            dotMemory.Check(memory => { Console.WriteLine(memory.SizeInBytes); });
        }

        [TestMethod]
        public void TestUser2()
        {
            var users = new List<User2>();

            var firstNames = Enumerable.Range(0, 100).Select(_ => RandomString());
            var lastNames = Enumerable.Range(0, 100).Select(_ => RandomString());

            foreach (var firstName in firstNames)
            foreach (var lastName in lastNames)
                users.Add(new User2($"{firstName} {lastName}"));

            ForceGC();

            dotMemory.Check(memory => { Console.WriteLine(memory.SizeInBytes); });
        }

        public void Execute()
        {
            throw new NotImplementedException();
        }
    }
}