﻿namespace DesignPattern.Creational.Factories
{
    public class Point
    {
        private double _x, _y;

        protected Point(double x, double y)
        {
            this._x = x;
            this._y = y;
        }

        public Point(double a,
            double b, // names do not communicate intent
            CoordinateSystem cs = CoordinateSystem.Cartesian)
        {
            switch (cs)
            {
                case CoordinateSystem.Polar:
                    _x = a * Math.Cos(b);
                    _y = a * Math.Sin(b);
                    break;
                default:
                    _x = a;
                    _y = b;
                    break;
            }

            // steps to add a new system
            // 1. augment CoordinateSystem
            // 2. change ctor
        }

        // factory property

        public static Point Origin => new Point(0, 0);

        // singleton field
        public static Point Origin2 = new Point(0, 0);

        // factory method

        public static Point NewCartesianPoint(double x, double y)
        {
            return new Point(x, y);
        }

        public static Point NewPolarPoint(double rho, double theta)
        {
            //...
            return null;
        }

        public enum CoordinateSystem
        {
            Cartesian,
            Polar
        }

        // make it lazy
        public static class Factory
        {
            public static Point NewCartesianPoint(double x, double y)
            {
                return new Point(x, y);
            }
        }
    }

    class PointFactory
    {
        public static Point NewCartesianPoint(float x, float y)
        {
            return new Point(x, y); // needs to be public
        }
    }

    internal class Factory : IExecute
    {
        public void Execute()
        {
            var p1 = new Point(2, 3, Point.CoordinateSystem.Cartesian);
            var origin = Point.Origin;

            var p2 = Point.Factory.NewCartesianPoint(1, 2);
        }
    }
}