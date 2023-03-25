using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Text;
using static System.Console;

namespace DesignPattern.Structural.Decorator
{
    public abstract class Shape
    {
        public virtual string AsString() => string.Empty;
    }

    public sealed class Circle : Shape
    {
        private float radius;

        public Circle() : this(0)
        {
        }

        public Circle(float radius)
        {
            this.radius = radius;
        }

        public void Resize(float factor)
        {
            radius *= factor;
        }

        public override string AsString() => $"A circle of radius {radius}";
    }

    public sealed class Square : Shape
    {
        private readonly float side;

        public Square() : this(0)
        {
        }

        public Square(float side)
        {
            this.side = side;
        }

        public override string AsString() => $"A square with side {side}";
    }

    public abstract class ShapeDecoratorCyclePolicy
    {
        public abstract bool TypeAdditionAllowed(Type type, IList<Type> allTypes);
        public abstract bool ApplicationAllowed(Type type, IList<Type> allTypes);
    }

    public class ThrowOnCyclePolicy : ShapeDecoratorCyclePolicy
    {
        private bool Handler(Type type, IList<Type> allTypes)
        {
            if (allTypes.Contains(type))
                throw new InvalidOperationException(
                    $"Cycle detected! Type is already a {type.FullName}!");
            return true;
        }

        public override bool TypeAdditionAllowed(Type type, IList<Type> allTypes)
        {
            return Handler(type, allTypes);
        }

        public override bool ApplicationAllowed(Type type, IList<Type> allTypes)
        {
            return Handler(type, allTypes);
        }
    }

    public class AbsorbCyclePolicy : ShapeDecoratorCyclePolicy
    {
        public override bool TypeAdditionAllowed(Type type, IList<Type> allTypes)
        {
            return true;
        }

        public override bool ApplicationAllowed(Type type, IList<Type> allTypes)
        {
            return !allTypes.Contains(type);
        }
    }

    public class CyclesAllowedPolicy : ShapeDecoratorCyclePolicy
    {
        public override bool TypeAdditionAllowed(Type type, IList<Type> allTypes)
        {
            return true;
        }

        public override bool ApplicationAllowed(Type type, IList<Type> allTypes)
        {
            return true;
        }
    }

    public abstract class ShapeDecorator : Shape
    {
        protected internal readonly List<Type> Types = new();
        protected internal Shape Shape { get; set; }

        public ShapeDecorator(Shape shape)
        {
            this.Shape = shape;
            if (shape is ShapeDecorator sd)
                Types.AddRange(sd.Types);
        }
    }

    public abstract class ShapeDecorator<TSelf, TCyclePolicy> : ShapeDecorator
        where TCyclePolicy : ShapeDecoratorCyclePolicy, new()
    {
        protected readonly TCyclePolicy Policy = new();

        public ShapeDecorator(Shape shape) : base(shape)
        {
            if (Policy.TypeAdditionAllowed(typeof(TSelf), Types))
                Types.Add(typeof(TSelf));
        }
    }

    // can determine one policy for all classes
    public class ShapeDecoratorWithPolicy<T>
        : ShapeDecorator<T, ThrowOnCyclePolicy>
    {
        public ShapeDecoratorWithPolicy(Shape shape) : base(shape)
        {
        }
    }

    // dynamic
    public class ColoredShape
        : ShapeDecorator<ColoredShape, AbsorbCyclePolicy>
    {
        private readonly string color;

        public ColoredShape(Shape shape, string color) : base(shape)
        {
            this.color = color;
        }

        public override string AsString()
        {
            var sb = new StringBuilder($"{Shape.AsString()}");

            if (Policy.ApplicationAllowed(Types[0], Types.Skip(1).ToList()))
                sb.Append($" has the color {color}");

            return sb.ToString();
        }
    }

    [TestClass]
    public class DynamicDecoratorCycles : IExecute
    {
        [TestMethod]
        public void Execute()
        {
            var circle = new Circle(2);
            var colored1 = new ColoredShape(circle, "red");
            var colored2 = new ColoredShape(colored1, "blue");

            WriteLine(circle.AsString());
            WriteLine(colored1.AsString());
            WriteLine(colored2.AsString());
        }
    }
}