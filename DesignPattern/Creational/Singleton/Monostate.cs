﻿using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DesignPattern.Creational.Singleton
{
    public class ChiefExecutiveOfficer
    {
        private static string name;
        private static int age;

        public string Name
        {
            get => name;
            set => name = value;
        }

        public int Age
        {
            get => age;
            set => age = value;
        }

        public override string ToString()
        {
            return $"{nameof(Name)}: {Name}, {nameof(Age)}: {Age}";
        }
    }

    [TestClass]
    public class Monostate : IExecute
    {
        [TestMethod]
        public void Execute()
        {
            var ceo = new ChiefExecutiveOfficer();
            ceo.Name = "Adam Smith";
            ceo.Age = 55;

            var ceo2 = new ChiefExecutiveOfficer();
            Console.WriteLine(ceo2);
        }
    }
}