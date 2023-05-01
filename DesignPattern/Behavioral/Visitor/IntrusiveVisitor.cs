﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Text;

namespace DesignPattern.Behavioral.Visitor
{
    [TestClass]
    public class IntrusiveVisitor : IExecute
    {
        public abstract class Expression
        {
            // adding a new operation
            public abstract void Print(StringBuilder sb);
        }

        public class DoubleExpression : Expression
        {
            private double value;

            public DoubleExpression(double value)
            {
                this.value = value;
            }

            public override void Print(StringBuilder sb)
            {
                sb.Append(value);
            }
        }

        public class AdditionExpression : Expression
        {
            private Expression left, right;

            public AdditionExpression(Expression left, Expression right)
            {
                this.left = left ?? throw new ArgumentNullException(paramName: nameof(left));
                this.right = right ?? throw new ArgumentNullException(paramName: nameof(right));
            }

            public override void Print(StringBuilder sb)
            {
                sb.Append(value: "(");
                left.Print(sb);
                sb.Append(value: "+");
                right.Print(sb);
                sb.Append(value: ")");
            }
        }

        [TestMethod]
        public void Execute()
        {
            var e = new AdditionExpression(
                left: new DoubleExpression(1),
                right: new AdditionExpression(
                    left: new DoubleExpression(2),
                    right: new DoubleExpression(3)));
            var sb = new StringBuilder();
            e.Print(sb);
            Console.WriteLine(sb);

            // what is more likely: new type o rnew operation
        }
    }
}
