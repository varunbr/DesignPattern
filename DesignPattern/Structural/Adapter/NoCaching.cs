﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using MoreLinq;
using System.Collections.ObjectModel;
using static System.Console;

namespace DesignPattern.Structural.Adapter
{
    [TestClass]
    public class NoCaching : IExecute
    {
        public class Point
        {
            public int X;
            public int Y;

            public Point(int x, int y)
            {
                this.X = x;
                this.Y = y;
            }

            public override string ToString()
            {
                return $"{nameof(X)}: {X}, {nameof(Y)}: {Y}";
            }
        }

        public class Line
        {
            public Point Start;
            public Point End;

            public Line(Point start, Point end)
            {
                this.Start = start;
                this.End = end;
            }
        }

        public class VectorObject : Collection<Line>
        {
        }

        public class VectorRectangle : VectorObject
        {
            public VectorRectangle(int x, int y, int width, int height)
            {
                Add(new Line(new Point(x, y), new Point(x + width, y)));
                Add(new Line(new Point(x + width, y), new Point(x + width, y + height)));
                Add(new Line(new Point(x, y), new Point(x, y + height)));
                Add(new Line(new Point(x, y + height), new Point(x + width, y + height)));
            }
        }

        public class LineToPointAdapter : Collection<Point>
        {
            private static int _count = 0;

            public LineToPointAdapter(Line line)
            {
                WriteLine(
                    $"{++_count}: Generating points for line [{line.Start.X},{line.Start.Y}]-[{line.End.X},{line.End.Y}] (no caching)");

                int left = Math.Min(line.Start.X, line.End.X);
                int right = Math.Max(line.Start.X, line.End.X);
                int top = Math.Min(line.Start.Y, line.End.Y);
                int bottom = Math.Max(line.Start.Y, line.End.Y);
                int dx = right - left;
                int dy = line.End.Y - line.Start.Y;

                if (dx == 0)
                {
                    for (int y = top; y <= bottom; ++y)
                    {
                        Add(new Point(left, y));
                    }
                }
                else if (dy == 0)
                {
                    for (int x = left; x <= right; ++x)
                    {
                        Add(new Point(x, top));
                    }
                }
            }
        }

        private static readonly List<VectorObject> VectorObjects = new List<VectorObject>
        {
            new VectorRectangle(1, 1, 10, 10),
            new VectorRectangle(3, 3, 6, 6)
        };

        // the interface we have
        public static void DrawPoint(Point p)
        {
            Write(".");
        }

        private static void Draw()
        {
            foreach (var vo in VectorObjects)
            {
                foreach (var line in vo)
                {
                    var adapter = new LineToPointAdapter(line);
                    adapter.ForEach(DrawPoint);
                }
            }
        }

        [TestMethod]
        public void Execute()
        {
            Draw();
            Draw();
        }
    }
}