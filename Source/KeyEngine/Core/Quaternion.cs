using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeyEngine.Core
{
    [Serializable]
    public struct Quaternion
    {
        public float X;
        public float Y;
        public float Z;
        public float W;

        public Quaternion(float x, float y, float z, float w)
        {
            X = x; Y = y; Z = z; W = w;
        }

        public static Quaternion AngleAxis(float angle, Vector3 axis)
        {
            Quaternion result;
            Vector3 normalize = axis.normalized;

            float half = angle * 0.5f;
            float sin = (float)Math.Sin(half);
            float cos = (float)Math.Cos(half);

            result.X = normalize.X * sin;
            result.Y = normalize.Y * sin;
            result.Z = normalize.Z * sin;
            result.W = cos;

            return result;
        }

        public static Quaternion Euler(float x, float y, float z)
        {
            Quaternion result;
            float halfX = x * 0.5f;
            float halfY = y * 0.5f;
            float halfZ = z * 0.5f;

            float sinX = (float)Math.Sin(halfX);
            float cosX = (float)Math.Cos(halfX);

            float sinY = (float)Math.Sin(halfY);
            float cosY = (float)Math.Cos(halfY);

            float sinZ = (float)Math.Sin(halfZ);
            float cosZ = (float)Math.Cos(halfZ);

            Quaternion qX = new Quaternion(sinX, 0.0F, 0.0F, cosX);
            Quaternion qY = new Quaternion(0.0F, sinY, 0.0F, cosY);
            Quaternion qZ = new Quaternion(0.0F, 0.0F, sinZ, cosZ);

            result = (qY * qX) * qZ;
 
            return result;
        }

        public static Quaternion operator +(Quaternion a, Quaternion b)
        {
            return new Quaternion(a.X + b.X, a.Y + b.Y, a.Z + b.Z, a.W + b.W);
        }

        public static Quaternion operator -(Quaternion a, Quaternion b)
        {
            return new Quaternion(a.X - b.X, a.Y - b.Y, a.Z - b.Z, a.W - b.W);
        }

        public static Quaternion operator -(Quaternion a)
        {
            return new Quaternion(-a.X, -a.Y, -a.Z, -a.W);
        }

        public static Quaternion operator *(Quaternion a, float scale)
        {
            Quaternion result;
            result.X = a.X * scale;
            result.Y = a.Y * scale;
            result.Z = a.Z * scale;
            result.W = a.W * scale;
            return result;
        }

        public static Quaternion operator *(Quaternion a, Quaternion b)
        {
            Quaternion result;
            result.X = (b.X * a.W + a.X * b.W + b.Y * a.Z) - (b.Z * a.Y);
            result.Y = (b.Y * a.W + a.Y * b.W + b.Z * a.X) - (b.X * a.Z);
            result.Z = (b.Z * a.W + a.Z * b.W + b.X * a.Y) - (b.Y * a.X);
            result.W = (b.W * a.W) - (b.X * a.X + b.Y * a.Y + b.Z * a.Z);
            return result;
        }

        public static Quaternion operator /(Quaternion a, float value)
        {
            return new Quaternion(a.X / value, a.Y / value, a.Z / value, a.W / value);
        }

        public static bool operator ==(Quaternion a, Quaternion b)
        {
            return Dot(a, b) > 1.0f - float.Epsilon;
        }

        public static bool operator !=(Quaternion a, Quaternion b)
        {
            return Dot(a, b) <= 1.0f - float.Epsilon;
        }

        public float SqrMagnitude()
        {
            return (X * X) + (Y * Y) + (Z * Z) + (W * W);
        }

        public float Magnitude()
        {
            return (float)Math.Sqrt(SqrMagnitude());
        }

        public void Normalize()
        {
            float magnitude = Magnitude();
            if (magnitude > float.Epsilon)
                this = this / magnitude;
            else
                this = Identity;
        }

        public static Quaternion Normalize(Quaternion value)
        {
            float magnitude = value.Magnitude();
            if (magnitude > float.Epsilon)
                return value / magnitude;
            else
                return Identity;
        }

        public Quaternion normalized { get { return Normalize(this); } }

        public static float Dot(Quaternion a, Quaternion b)
        {
            return (a.X * b.X + a.Y * b.Y + a.Z * b.Z + a.W * b.W);
        }

        public static Quaternion Lerp(Quaternion start, Quaternion end, float amount)
        {
            float inverse = 1.0f - amount;

            Quaternion result;
            if (Dot(start, end) >= 0.0f)
            {
                result.X = (inverse * start.X) + (amount * end.X);
                result.Y = (inverse * start.Y) + (amount * end.Y);
                result.Z = (inverse * start.Z) + (amount * end.Z);
                result.W = (inverse * start.W) + (amount * end.W);
            }
            else
            {
                result.X = (inverse * start.X) - (amount * end.X);
                result.Y = (inverse * start.Y) - (amount * end.Y);
                result.Z = (inverse * start.Z) - (amount * end.Z);
                result.W = (inverse * start.W) - (amount * end.W);
            }
            result.Normalize();
            return result;
        }

        public override bool Equals(object other)
        {
            if (!(other is Quaternion)) return false;

            Quaternion b = (Quaternion)other;
            return X.Equals(b.X) && Y.Equals(b.Y) && Z.Equals(b.Z) && W.Equals(b.W);
        }

        public override int GetHashCode()
        {
            return X.GetHashCode() ^ (Y.GetHashCode() << 2) ^ (Z.GetHashCode() >> 2) ^ (W.GetHashCode() >> 1);
        }

        public static Quaternion Identity { get { return new Quaternion(0F, 0F, 0F, 1F); } }
    }
}
