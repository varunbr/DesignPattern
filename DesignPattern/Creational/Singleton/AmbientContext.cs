using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Text;

namespace DesignPattern.Creational.Singleton
{
    // non-thread-safe global context
    public sealed class BuildingContext : IDisposable
    {
        public int WallHeight = 0;
        public int WallThickness = 300; // etc.

        private static Stack<BuildingContext> _stack
            = new Stack<BuildingContext>();

        static BuildingContext()
        {
            // ensure there's at least one state
            _stack.Push(new BuildingContext(0));
        }

        public BuildingContext(int wallHeight)
        {
            WallHeight = wallHeight;
            _stack.Push(this);
        }

        public static BuildingContext Current => _stack.Peek();

        public void Dispose()
        {
            // not strictly necessary
            if (_stack.Count > 1)
                _stack.Pop();
        }
    }

    public class Building
    {
        public readonly List<Wall> Walls = new List<Wall>();

        public override string ToString()
        {
            var sb = new StringBuilder();
            foreach (var wall in Walls)
                sb.AppendLine(wall.ToString());
            return sb.ToString();
        }
    }

    public struct Point
    {
        private int x, y;

        public Point(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public override string ToString()
        {
            return $"{nameof(x)}: {x}, {nameof(y)}: {y}";
        }
    }

    public class Wall
    {
        public Point Start, End;
        public int Height;

        public const int UseAmbient = Int32.MinValue;

        // public Wall(Point start, Point end, int elevation = UseAmbient)
        // {
        //   Start = start;
        //   End = end;
        //   Elevation = elevation;
        // }

        public Wall(Point start, Point end)
        {
            Start = start;
            End = end;
            //Elevation = BuildingContext.Elevation;
            Height = BuildingContext.Current.WallHeight;
        }

        public override string ToString()
        {
            return $"{nameof(Start)}: {Start}, {nameof(End)}: {End}, " +
                   $"{nameof(Height)}: {Height}";
        }
    }

    [TestClass]
    public class AmbientContext : IExecute
    {
        [TestMethod]
        public void Execute()
        {
            var house = new Building();

            // ground floor
            //var e = 0;
            house.Walls.Add(new Wall(new Point(0, 0), new Point(5000, 0) /*, e*/));
            house.Walls.Add(new Wall(new Point(0, 0), new Point(0, 4000) /*, e*/));

            // first floor
            //e = 3500;
            using (new BuildingContext(3500))
            {
                house.Walls.Add(new Wall(new Point(0, 0), new Point(5000, 0) /*, e*/));
                house.Walls.Add(new Wall(new Point(0, 0), new Point(0, 4000) /*, e*/));
            }

            // back to ground again
            // e = 0;
            house.Walls.Add(new Wall(new Point(5000, 0), new Point(5000, 4000) /*, e*/));

            Console.WriteLine(house);
        }
    }
}