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

        public float this[int index]
        {
            get
            {
                switch (index)
                {
                    case 0: return X;
                    case 1: return Y;
                    case 2: return Z;
                    case 3: return W;
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
                    case 3: W = value; break;
                    default: throw new ArgumentOutOfRangeException();
                }
            }
        }

        public Quaternion(float x, float y, float z, float w)
        {
            X = x; Y = y; Z = z; W = w;
        }

        public static Quaternion AngleAxis(float angle, Vector3 axis)
        {
            Quaternion result;
            Vector3 normalize = axis.normalized;

            float half = MathUtils.ToRadians(angle) * 0.5f;
            float sin = (float)Math.Sin(half);

            result.X = normalize.X * sin;
            result.Y = normalize.Y * sin;
            result.Z = normalize.Z * sin;
            result.W = (float)Math.Cos(half);

            return result;
        }

        public static Quaternion EulerAngles(Vector3 value)
        {
            Quaternion result;
            float halfX = MathUtils.ToRadians(value.X) * 0.5f;
            float halfY = MathUtils.ToRadians(value.Y) * 0.5f;
            float halfZ = MathUtils.ToRadians(value.Z) * 0.5f;

            float sinX = (float)Math.Sin(halfX);
            float cosX = (float)Math.Cos(halfX);

            float sinY = (float)Math.Sin(halfY);
            float cosY = (float)Math.Cos(halfY);

            float sinZ = (float)Math.Sin(halfZ);
            float cosZ = (float)Math.Cos(halfZ);

            result.X = (cosY * sinX * cosZ) + (sinY * cosX * sinZ);
            result.Y = (sinY * cosX * cosZ) - (cosY * sinX * sinZ);
            result.Z = (cosY * cosX * sinZ) - (sinY * sinX * cosZ);
            result.W = (cosY * cosX * cosZ) + (sinY * sinX * sinZ);

            return result;
        }

        public static Quaternion RotationMatrix(Matrix4x4 matrix)
        {
            float scale = 1f + matrix.M11 + matrix.M22 + matrix.M33;

            Quaternion result;

            if (scale > 0f)
            {
                float sqrt = (float)Math.Sqrt(scale + 1f);

                result.W = sqrt * 0.5f;

                sqrt = 0.5f / sqrt;

                result.X = (matrix.M23 - matrix.M32) * sqrt;
                result.Y = (matrix.M31 - matrix.M13) * sqrt;
                result.Z = (matrix.M12 - matrix.M21) * sqrt;
            }
            else if (matrix.M11 >= matrix.M22 && matrix.M11 >= matrix.M33)
            {
                float sqrt = (float)Math.Sqrt(1 + matrix.M11 - matrix.M22 - matrix.M33);

                result.X = sqrt * 0.5f;

                sqrt = 0.5f / sqrt;

                result.Y = (matrix.M12 + matrix.M21) * sqrt;
                result.Z = (matrix.M13 + matrix.M31) * sqrt;
                result.W = (matrix.M23 - matrix.M32) * sqrt;
            }
            else if (matrix.M22 > matrix.M33)
            {
                float sqrt = (float)Math.Sqrt(1 + matrix.M22 - matrix.M11 - matrix.M33);

                result.Y = sqrt * 0.5f;

                sqrt = 0.5f / sqrt;

                result.X = (matrix.M21 + matrix.M12) * sqrt;
                result.Z = (matrix.M32 + matrix.M23) * sqrt;
                result.W = (matrix.M31 - matrix.M13) * sqrt;
            }
            else
            {
                float sqrt = (float)Math.Sqrt(1.0f + matrix.M33 - matrix.M11 - matrix.M22);

                result.Z = sqrt * 0.5f;

                sqrt = 0.5f / sqrt;

                result.X = (matrix.M31 + matrix.M13) * sqrt;
                result.Y = (matrix.M32 + matrix.M23) * sqrt;
                result.W = (matrix.M12 - matrix.M21) * sqrt;
            }

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

        public static Vector3 operator *(Quaternion q, Vector3 v)
        {
            Vector3 result;
            result.X = v.X - (q.Y * q.Y * 2f + q.Z * q.Z * 2f) * v.X + (q.X * q.Y * 2f - q.Z * q.W * 2f) * v.Y + (q.X * q.Z * 2f + q.Y * q.W * 2f) * v.Z;
            result.Y = v.Y + (q.X * q.Y * 2f + q.Z * q.W * 2f) * v.X - (q.X * q.X * 2f + q.Z * q.Z * 2f) * v.Y + (q.Y * q.Z * 2f - q.X * q.W * 2f) * v.Z;
            result.Z = v.Z + (q.X * q.Z * 2f - q.Y * q.W * 2f) * v.X + (q.Y * q.Z * 2f + q.X * q.W * 2f) * v.Y - (q.X * q.X * 2f + q.Y * q.Y * 2f) * v.Z;
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

        public void Conjugate()
        {
            X = -X; Y = -Y; Z = -Z;
        }

        public static Quaternion Conjugate(Quaternion value)
        {
            return new Quaternion(-value.X, -value.Y, -value.Z, value.W);
        }

        public Quaternion conjugate { get { return Conjugate(this); } }

        public static Quaternion Inverse(Quaternion value)
        {
            float inverseNorm = 1f / value.SqrMagnitude();

            return new Quaternion(-value.X * inverseNorm, -value.Y * inverseNorm, -value.Z * inverseNorm, value.W * inverseNorm);
        }

        public Quaternion inverse { get { return Inverse(this); } }

        public static Vector3 EulerAngles(Quaternion value)
        {
            const float SINGULARITY_CUTOFF = 0.499999f;

            Vector3 result;

            float singularity = value.Y * value.Z - value.X * value.W;
            float z1 = 2f * (-value.X * value.Y + value.Z * value.W);
            float z2 = value.Y * value.Y - value.Z * value.Z - value.X * value.X + value.W * value.W;
            float x1 = -1f;
            float x2 = 2f * singularity;

            if (Math.Abs(singularity) < SINGULARITY_CUTOFF)
            {
                float y1 = 2f * (value.X * value.Z + value.Y * value.W);
                float y2 = value.Z * value.Z - value.X * value.X - value.Y * value.Y + value.W * value.W;

                result.X = x1 * (float)Math.Asin(x2);
                result.Y = (float)Math.Atan2(y1, y2);
                result.Z = (float)Math.Atan2(z1, z2);
            }
            else
            {
                float y1 = (value.X * value.Y + value.Z * value.W) * (value.Y * value.Z + value.X * value.W) + (-value.Y * value.Z + value.X * value.W) * (value.X * value.Y - value.Z * value.W);
                float y2 = (-value.Y * value.Z + value.X * value.W) * (value.Y * value.Z + value.X * value.W) - (value.X * value.Y + value.Z * value.W) * (value.X * value.Y - value.Z * value.W);

                result.X = x1 * (float)Math.Asin(x2);
                result.Y = (float)Math.Atan2(y1, y2);
                result.Z = 0f;
            }

            return result;
        }

        public Vector3 eulerAngles { get { return EulerAngles(this); } }

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

        public static Quaternion Identity { get { return new Quaternion(0f, 0f, 0f, 1f); } }
    }
}
