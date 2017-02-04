using System;

namespace KeyEngine.Core
{
    [Serializable]
    public struct Vector3
    {
        public float X;
        public float Y;
        public float Z;

        public float this[int index]
        {
            get
            {
                switch (index)
                {
                    case 0: return X;
                    case 1: return Y;
                    case 2: return Z;
                    default: throw new ArgumentOutOfRangeException();
                }
            }
            set
            {
                switch (index)
                {
                    case 0: X = value; break;
                    case 1: Y = value; break;
                    case 2: Z = value; break;
                    default: throw new ArgumentOutOfRangeException();
                }
            }
        }

        public Vector3(float x, float y, float z)
        {
            X = x; Y = y; Z = z;
        }

        public Vector3(float x, float y)
        {
            X = x; Y = y; Z = 0.0f;
        }

        public static Vector3 operator +(Vector3 a, Vector3 b)
        {
            return new Vector3(a.X + b.X, a.Y + b.Y, a.Z + b.Z);
        }

        public static Vector3 operator -(Vector3 a, Vector3 b)
        {
            return new Vector3(a.X - b.X, a.Y - b.Y, a.Z - b.Z);
        }

        public static Vector3 operator -(Vector3 a)
        {
            return new Vector3(-a.X, -a.Y, -a.Z);
        }

        public static Vector3 operator *(Vector3 a, float value)
        {
            return new Vector3(a.X * value, a.Y * value, a.Z * value);
        }

        public static Vector3 operator *(float value, Vector3 a)
        {
            return new Vector3(a.X * value, a.Y * value, a.Z * value);
        }

        public static Vector3 operator /(Vector3 a, float value)
        {
            return new Vector3(a.X / value, a.Y / value, a.Z / value);
        }

        public static bool operator ==(Vector3 a, Vector3 b)
        {
            return Dot(a, b) < float.Epsilon * float.Epsilon;
        }

        public static bool operator !=(Vector3 a, Vector3 b)
        {
            return Dot(a, b) >= float.Epsilon * float.Epsilon;
        }

        public static Vector3 Cross(Vector3 a, Vector3 b)
        {
            return new Vector3(a.Y * b.Z - a.Z * b.Y, a.Z * b.X - a.X * b.Z, a.X * b.Y - a.Y * b.X);
        }

        public static float Dot(Vector3 a, Vector3 b)
        {
            return (a.X * b.X + a.Y * b.Y + a.Z * b.Z);
        }

        public float SqrMagnitude()
        {
            return (X * X) + (Y * Y) + (Z * Z);
        }

        public float Magnitude()
        {
            return (float)Math.Sqrt(SqrMagnitude());
        }

        public static float Distance(Vector3 a, Vector3 b)
        {
            return (a - b).Magnitude();
        }

        public void Normalize()
        {
            float magnitude = Magnitude();
            if (magnitude > float.Epsilon)
                this = this / magnitude;
            else
                this = Zero;
        }

        public static Vector3 Normalize(Vector3 value)
        {
            float magnitude = value.Magnitude();
            if (magnitude > float.Epsilon)
                return value / magnitude;
            else
                return Zero;
        }

        public Vector3 normalized { get { return Normalize(this); } }

        public override bool Equals(object other)
        {
            if (!(other is Vector3))
                return false;

            Vector3 b = (Vector3)other;
            return X.Equals(b.X) && Y.Equals(b.Y) && Z.Equals(b.Z);
        }

        public override int GetHashCode()
        {
            return X.GetHashCode() ^ (Y.GetHashCode() << 2) ^ (Z.GetHashCode() >> 2);
        }

        public static Vector3 Zero { get { return new Vector3(0f, 0f, 0f); } }
        public static Vector3 One { get { return new Vector3(1f, 1f, 1f); } }
        public static Vector3 Forward { get { return new Vector3(0f, 0f, 1f); } }
        public static Vector3 Back { get { return new Vector3(0f, 0f, -1f); } }
        public static Vector3 Up { get { return new Vector3(0f, 1f, 0f); } }
        public static Vector3 Down { get { return new Vector3(0f, -1f, 0f); } }
        public static Vector3 Left { get { return new Vector3(-1f, 0f, 0f); } }
        public static Vector3 Right { get { return new Vector3(1f, 0f, 0f); } }
    }
}
