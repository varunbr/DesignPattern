﻿using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DesignPattern.Behavioral.State
{
    enum Chest
    {
        Open,
        Closed,
        Locked
    }

    enum Action
    {
        Open,
        Close
    }

    [TestClass]
    public class SwitchExpressions : IExecute
    {
        static Chest Manipulate(Chest chest,
            Action action, bool haveKey) =>
            (chest, action, haveKey) switch
            {
                (Chest.Closed, Action.Open, _) => Chest.Open,
                (Chest.Locked, Action.Open, true) => Chest.Open,
                (Chest.Open, Action.Close, true) => Chest.Locked,
                (Chest.Open, Action.Close, false) => Chest.Closed,

                _ => chest
            };

        static Chest Manipulate2(Chest chest,
            Action action, bool haveKey)
        {
            switch (chest, action, haveKey)
            {
                case (Chest.Closed, Action.Open, _):
                    return Chest.Open;
                case (Chest.Locked, Action.Open, true):
                    return Chest.Open;
                case (Chest.Open, Action.Close, true):
                    return Chest.Locked;
                case (Chest.Open, Action.Close, false):
                    return Chest.Closed;
                default:
                    Console.WriteLine("Chest unchanged");
                    return chest;
            }
        }

        [TestMethod]
        public void Execute()
        {
            Chest chest = Chest.Locked;
            Console.WriteLine($"Chest is {chest}");

            // unlock with key
            chest = Manipulate(chest, Action.Open, true);
            Console.WriteLine($"Chest is now {chest}");

            // close it!
            chest = Manipulate(chest, Action.Close, false);
            Console.WriteLine($"Chest is now {chest}");

            // close it again!
            chest = Manipulate(chest, Action.Close, false);
            Console.WriteLine($"Chest is now {chest}");
        }
    }
}
