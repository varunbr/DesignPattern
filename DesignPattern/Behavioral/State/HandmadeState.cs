using Microsoft.VisualStudio.TestTools.UnitTesting;
using static System.Console;

namespace DesignPattern.Behavioral.State
{
    [TestClass]
    public class HandmadeState : IExecute
    {
        private static readonly Dictionary<State, List<(Trigger, State)>> Rules
            = new()
            {
                [State.OffHook] = new List<(Trigger, State)>
                {
                    (Trigger.CallDialed, State.Connecting)
                },
                [State.Connecting] = new List<(Trigger, State)>
                {
                    (Trigger.HungUp, State.OffHook),
                    (Trigger.CallConnected, State.Connected)
                },
                [State.Connected] = new List<(Trigger, State)>
                {
                    (Trigger.LeftMessage, State.OffHook),
                    (Trigger.HungUp, State.OffHook),
                    (Trigger.PlacedOnHold, State.OnHold)
                },
                [State.OnHold] = new List<(Trigger, State)>
                {
                    (Trigger.TakenOffHold, State.Connected),
                    (Trigger.HungUp, State.OffHook)
                }
            };

        [TestMethod]
        public void Execute()
        {
            var state = State.OffHook;
            while (true)
            {
                WriteLine($"The phone is currently {state}");
                WriteLine("Select a trigger:");

                // foreach to for
                for (var i = 0; i < Rules[state].Count; i++)
                {
                    var (t, _) = Rules[state][i];
                    WriteLine($"{i}. {t}");
                }

                WriteLine("-1. Exit");

                int input = int.Parse(ReadLine());

                if (input < 0)
                    break;

                var (_, s) = Rules[state][input];
                state = s;
            }
        }

        public enum State
        {
            OffHook,
            Connecting,
            Connected,
            OnHold
        }

        public enum Trigger
        {
            CallDialed,
            HungUp,
            CallConnected,
            PlacedOnHold,
            TakenOffHold,
            LeftMessage
        }
    }
}
