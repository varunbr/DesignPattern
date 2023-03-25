using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DesignPattern.Structural.Adapter
{
    // Vector2f, Vector3i
    public interface IInteger
    {
        int Value { get; }
    }

    public static class Dimensions
    {
        public class Two : IInteger
        {
            public int Value => 2;
        }

        public class Three : IInteger
        {
            public int Value => 3;
        }
    }

    public class Vector<TSelf, T, TD>
        where TD : IInteger, new()
        where TSelf : Vector<TSelf, T, TD>, new()
    {
        protected T[] Data;

        public Vector()
        {
            Data = new T[new TD().Value];
        }

        public Vector(params T[] values)
        {
            var requiredSize = new TD().Value;
            Data = new T[requiredSize];

            var providedSize = values.Length;

            for (int i = 0; i < Math.Min(requiredSize, providedSize); ++i)
                Data[i] = values[i];
        }

        public static TSelf Create(params T[] values)
        {
            var result = new TSelf();
            var requiredSize = new TD().Value;
            result.Data = new T[requiredSize];

            var providedSize = values.Length;

            for (int i = 0; i < Math.Min(requiredSize, providedSize); ++i)
                result.Data[i] = values[i];

            return result;
        }

        public T this[int index]
        {
            get => Data[index];
            set => Data[index] = value;
        }

        public T X
        {
            get => Data[0];
            set => Data[0] = value;
        }
    }

    public class VectorOfFloat<TSelf, TD>
        : Vector<TSelf, float, TD>
        where TD : IInteger, new()
        where TSelf : Vector<TSelf, float, TD>, new()
    {
    }

    public class VectorOfInt<TD> : Vector<VectorOfInt<TD>, int, TD>
        where TD : IInteger, new()
    {
        public VectorOfInt()
        {
        }

        public VectorOfInt(params int[] values) : base(values)
        {
        }

        public static VectorOfInt<TD> operator +
            (VectorOfInt<TD> lhs, VectorOfInt<TD> rhs)
        {
            var result = new VectorOfInt<TD>();
            var dim = new TD().Value;
            for (int i = 0; i < dim; i++)
            {
                result[i] = lhs[i] + rhs[i];
            }

            return result;
        }
    }

    public class Vector2I : VectorOfInt<Dimensions.Two>
    {
        public Vector2I()
        {
        }

        public Vector2I(params int[] values) : base(values)
        {
        }
    }

    public class Vector3F
        : VectorOfFloat<Vector3F, Dimensions.Three>
    {
        public override string ToString()
        {
            return $"{string.Join(",", Data)}";
        }
    }

    [TestClass]
    public class GenericValue : IExecute
    {
        [TestMethod]
        public void Execute()
        {
            var v = new Vector2I(1, 2);
            v[0] = 0;

            var vv = new Vector2I(3, 2);

            var result = v + vv;

            Vector3F u = Vector3F.Create(3.5f, 2.2f, 1);
        }
    }
}