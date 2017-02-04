using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeyEngine.Core
{
    [Serializable]
    public struct Matrix4x4
    {
        public float M11;
        public float M12;
        public float M13;
        public float M14;
        public float M21;
        public float M22;
        public float M23;
        public float M24;
        public float M31;
        public float M32;
        public float M33;
        public float M34;
        public float M41;
        public float M42;
        public float M43;
        public float M44;

        public float this[int index]
        {
            get
            {
                switch (index)
                {
                    case 0: return M11;
                    case 1: return M12;
                    case 2: return M13;
                    case 3: return M14;
                    case 4: return M21;
                    case 5: return M22;
                    case 6: return M23;
                    case 7: return M24;
                    case 8: return M31;
                    case 9: return M32;
                    case 10: return M33;
                    case 11: return M34;
                    case 12: return M41;
                    case 13: return M42;
                    case 14: return M43;
                    case 15: return M44;
                }
                throw new ArgumentOutOfRangeException();
            }
            set
            {
                switch (index)
                {
                    case 0: M11 = value; break;
                    case 1: M12 = value; break;
                    case 2: M13 = value; break;
                    case 3: M14 = value; break;
                    case 4: M21 = value; break;
                    case 5: M22 = value; break;
                    case 6: M23 = value; break;
                    case 7: M24 = value; break;
                    case 8: M31 = value; break;
                    case 9: M32 = value; break;
                    case 10: M33 = value; break;
                    case 11: M34 = value; break;
                    case 12: M41 = value; break;
                    case 13: M42 = value; break;
                    case 14: M43 = value; break;
                    case 15: M44 = value; break;
                    default: throw new ArgumentOutOfRangeException();
                }
            }
        }

        public float this[int row, int column]
        {
            get
            {
                if (row < 0 || row > 3)
                    throw new ArgumentOutOfRangeException();
                if (column < 0 || column > 3)
                    throw new ArgumentOutOfRangeException();

                return this[(row * 4) + column];
            }
            set
            {
                if (row < 0 || row > 3)
                    throw new ArgumentOutOfRangeException();
                if (column < 0 || column > 3)
                    throw new ArgumentOutOfRangeException();

                this[(row * 4) + column] = value;
            }
        }

        public Matrix4x4(float m11, float m12, float m13, float m14, float m21, float m22, float m23, float m24, float m31, float m32, float m33, float m34, float m41, float m42, float m43, float m44)
        {
            M11 = m11; M12 = m12; M13 = m13; M14 = m14;
            M21 = m21; M22 = m22; M23 = m23; M24 = m24;
            M31 = m31; M32 = m32; M33 = m33; M34 = m34;
            M41 = m41; M42 = m42; M43 = m43; M44 = m44;
        }

        public static Matrix4x4 operator +(Matrix4x4 a, Matrix4x4 b)
        {
            Matrix4x4 result;
            result.M11 = a.M11 + b.M11; result.M12 = a.M12 + b.M12; result.M13 = a.M13 + b.M13; result.M14 = a.M14 + b.M14;
            result.M21 = a.M21 + b.M21; result.M22 = a.M22 + b.M22; result.M23 = a.M23 + b.M23; result.M24 = a.M24 + b.M24;
            result.M31 = a.M31 + b.M31; result.M32 = a.M32 + b.M32; result.M33 = a.M33 + b.M33; result.M34 = a.M34 + b.M34;
            result.M41 = a.M41 + b.M41; result.M42 = a.M42 + b.M42; result.M43 = a.M43 + b.M43; result.M44 = a.M44 + b.M44;
            return result;
        }

        public static Matrix4x4 operator -(Matrix4x4 a, Matrix4x4 b)
        {
            Matrix4x4 result;
            result.M11 = a.M11 - b.M11; result.M12 = a.M12 - b.M12; result.M13 = a.M13 - b.M13; result.M14 = a.M14 - b.M14;
            result.M21 = a.M21 - b.M21; result.M22 = a.M22 - b.M22; result.M23 = a.M23 - b.M23; result.M24 = a.M24 - b.M24;
            result.M31 = a.M31 - b.M31; result.M32 = a.M32 - b.M32; result.M33 = a.M33 - b.M33; result.M34 = a.M34 - b.M34;
            result.M41 = a.M41 - b.M41; result.M42 = a.M42 - b.M42; result.M43 = a.M43 - b.M43; result.M44 = a.M44 - b.M44;
            return result;
        }

        public static Matrix4x4 operator -(Matrix4x4 a)
        {
            return new Matrix4x4(-a.M11, -a.M12, -a.M13, -a.M14, -a.M21, -a.M22, -a.M23, -a.M24, -a.M31, -a.M32, -a.M33, -a.M34, -a.M41, -a.M42, -a.M43, -a.M44);
        }

        public static Matrix4x4 operator *(Matrix4x4 a, float value)
        {
            Matrix4x4 result;
            result.M11 = a.M11 * value; result.M12 = a.M12 * value; result.M13 = a.M13 * value; result.M14 = a.M14 * value;
            result.M21 = a.M21 * value; result.M22 = a.M22 * value; result.M23 = a.M23 * value; result.M24 = a.M24 * value;
            result.M31 = a.M31 * value; result.M32 = a.M32 * value; result.M33 = a.M33 * value; result.M34 = a.M34 * value;
            result.M41 = a.M41 * value; result.M42 = a.M42 * value; result.M43 = a.M43 * value; result.M44 = a.M44 * value;
            return result;
        }

        public static Vector3 operator *(Matrix4x4 a, Vector3 v)
        {
            Vector3 result;
            result.X = (a.M11 * v.X + a.M12 * v.Y + a.M13 * v.Z);
            result.Y = (a.M21 * v.X + a.M22 * v.Y + a.M23 * v.Z);
            result.Z = (a.M31 * v.X + a.M32 * v.Y + a.M33 * v.Z);
            return result;
        }

        public static Matrix4x4 operator *(Matrix4x4 a, Matrix4x4 b)
        {
            Matrix4x4 result;
            result.M11 = (((a.M11 * b.M11) + (a.M12 * b.M21)) + (a.M13 * b.M31)) + (a.M14 * b.M41);
            result.M12 = (((a.M11 * b.M12) + (a.M12 * b.M22)) + (a.M13 * b.M32)) + (a.M14 * b.M42);
            result.M13 = (((a.M11 * b.M13) + (a.M12 * b.M23)) + (a.M13 * b.M33)) + (a.M14 * b.M43);
            result.M14 = (((a.M11 * b.M14) + (a.M12 * b.M24)) + (a.M13 * b.M34)) + (a.M14 * b.M44);
            result.M21 = (((a.M21 * b.M11) + (a.M22 * b.M21)) + (a.M23 * b.M31)) + (a.M24 * b.M41);
            result.M22 = (((a.M21 * b.M12) + (a.M22 * b.M22)) + (a.M23 * b.M32)) + (a.M24 * b.M42);
            result.M23 = (((a.M21 * b.M13) + (a.M22 * b.M23)) + (a.M23 * b.M33)) + (a.M24 * b.M43);
            result.M24 = (((a.M21 * b.M14) + (a.M22 * b.M24)) + (a.M23 * b.M34)) + (a.M24 * b.M44);
            result.M31 = (((a.M31 * b.M11) + (a.M32 * b.M21)) + (a.M33 * b.M31)) + (a.M34 * b.M41);
            result.M32 = (((a.M31 * b.M12) + (a.M32 * b.M22)) + (a.M33 * b.M32)) + (a.M34 * b.M42);
            result.M33 = (((a.M31 * b.M13) + (a.M32 * b.M23)) + (a.M33 * b.M33)) + (a.M34 * b.M43);
            result.M34 = (((a.M31 * b.M14) + (a.M32 * b.M24)) + (a.M33 * b.M34)) + (a.M34 * b.M44);
            result.M41 = (((a.M41 * b.M11) + (a.M42 * b.M21)) + (a.M43 * b.M31)) + (a.M44 * b.M41);
            result.M42 = (((a.M41 * b.M12) + (a.M42 * b.M22)) + (a.M43 * b.M32)) + (a.M44 * b.M42);
            result.M43 = (((a.M41 * b.M13) + (a.M42 * b.M23)) + (a.M43 * b.M33)) + (a.M44 * b.M43);
            result.M44 = (((a.M41 * b.M14) + (a.M42 * b.M24)) + (a.M43 * b.M34)) + (a.M44 * b.M44);
            return result;
        }

        public static Matrix4x4 operator /(Matrix4x4 a, float value)
        {
            Matrix4x4 result;
            result.M11 = a.M11 / value;
            result.M12 = a.M12 / value;
            result.M13 = a.M13 / value;
            result.M14 = a.M14 / value;
            result.M21 = a.M21 / value;
            result.M22 = a.M22 / value;
            result.M23 = a.M23 / value;
            result.M24 = a.M24 / value;
            result.M31 = a.M31 / value;
            result.M32 = a.M32 / value;
            result.M33 = a.M33 / value;
            result.M34 = a.M34 / value;
            result.M41 = a.M41 / value;
            result.M42 = a.M42 / value;
            result.M43 = a.M43 / value;
            result.M44 = a.M44 / value;
            return result;
        }

        public static Matrix4x4 operator /(Matrix4x4 a, Matrix4x4 b)
        {
            Matrix4x4 result;
            result.M11 = a.M11 / b.M11;
            result.M12 = a.M12 / b.M12;
            result.M13 = a.M13 / b.M13;
            result.M14 = a.M14 / b.M14;
            result.M21 = a.M21 / b.M21;
            result.M22 = a.M22 / b.M22;
            result.M23 = a.M23 / b.M23;
            result.M24 = a.M24 / b.M24;
            result.M31 = a.M31 / b.M31;
            result.M32 = a.M32 / b.M32;
            result.M33 = a.M33 / b.M33;
            result.M34 = a.M34 / b.M34;
            result.M41 = a.M41 / b.M41;
            result.M42 = a.M42 / b.M42;
            result.M43 = a.M43 / b.M43;
            result.M44 = a.M44 / b.M44;
            return result;
        }

        public static bool operator ==(Matrix4x4 a, Matrix4x4 b)
        {
            return (a.M11 == b.M11 && a.M12 == b.M12 && a.M13 == b.M13 && a.M14 == b.M14 &&
                a.M21 == b.M21 && a.M22 == b.M22 && a.M23 == b.M23 && a.M24 == b.M24 &&
                a.M31 == b.M31 && a.M32 == b.M32 && a.M33 == b.M33 && a.M34 == b.M34 &&
                a.M41 == b.M41 && a.M42 == b.M42 && a.M43 == b.M43 && a.M44 == b.M44);
        }

        public static bool operator !=(Matrix4x4 a, Matrix4x4 b)
        {
            return (a.M11 != b.M11 || a.M12 != b.M12 || a.M13 != b.M13 || a.M14 != b.M14 ||
                a.M21 != b.M21 || a.M22 != b.M22 || a.M23 != b.M23 || a.M24 != b.M24 ||
                a.M31 != b.M31 || a.M32 != b.M32 || a.M33 != b.M33 || a.M34 != b.M34 ||
                a.M41 != b.M41 || a.M42 != b.M42 || a.M43 != b.M43 || a.M44 != b.M44);
        }

        public static Matrix4x4 Transpose(Matrix4x4 value)
        {
            Matrix4x4 result;
            result.M11 = value.M11;
            result.M12 = value.M21;
            result.M13 = value.M31;
            result.M14 = value.M41;

            result.M21 = value.M12;
            result.M22 = value.M22;
            result.M23 = value.M32;
            result.M24 = value.M42;

            result.M31 = value.M13;
            result.M32 = value.M23;
            result.M33 = value.M33;
            result.M34 = value.M43;

            result.M41 = value.M14;
            result.M42 = value.M24;
            result.M43 = value.M34;
            result.M44 = value.M44;
            return result;
        }

        public Matrix4x4 transpose { get { return Transpose(this); } }

        public static Matrix4x4 Inverse(Matrix4x4 value)
        {
            float b0 = (value.M31 * value.M42) - (value.M32 * value.M41);
            float b1 = (value.M31 * value.M43) - (value.M33 * value.M41);
            float b2 = (value.M34 * value.M41) - (value.M31 * value.M44);
            float b3 = (value.M32 * value.M43) - (value.M33 * value.M42);
            float b4 = (value.M34 * value.M42) - (value.M32 * value.M44);
            float b5 = (value.M33 * value.M44) - (value.M34 * value.M43);

            float d11 = value.M22 * b5 + value.M23 * b4 + value.M24 * b3;
            float d12 = value.M21 * b5 + value.M23 * b2 + value.M24 * b1;
            float d13 = value.M21 * -b4 + value.M22 * b2 + value.M24 * b0;
            float d14 = value.M21 * b3 + value.M22 * -b1 + value.M23 * b0;

            float det = value.M11 * d11 - value.M12 * d12 + value.M13 * d13 - value.M14 * d14;
            if (Math.Abs(det) == 0.0f)
                return Zero;

            Matrix4x4 result;

            det = 1f / det;

            float a0 = (value.M11 * value.M22) - (value.M12 * value.M21);
            float a1 = (value.M11 * value.M23) - (value.M13 * value.M21);
            float a2 = (value.M14 * value.M21) - (value.M11 * value.M24);
            float a3 = (value.M12 * value.M23) - (value.M13 * value.M22);
            float a4 = (value.M14 * value.M22) - (value.M12 * value.M24);
            float a5 = (value.M13 * value.M24) - (value.M14 * value.M23);

            float d21 = value.M12 * b5 + value.M13 * b4 + value.M14 * b3;
            float d22 = value.M11 * b5 + value.M13 * b2 + value.M14 * b1;
            float d23 = value.M11 * -b4 + value.M12 * b2 + value.M14 * b0;
            float d24 = value.M11 * b3 + value.M12 * -b1 + value.M13 * b0;

            float d31 = value.M42 * a5 + value.M43 * a4 + value.M44 * a3;
            float d32 = value.M41 * a5 + value.M43 * a2 + value.M44 * a1;
            float d33 = value.M41 * -a4 + value.M42 * a2 + value.M44 * a0;
            float d34 = value.M41 * a3 + value.M42 * -a1 + value.M43 * a0;

            float d41 = value.M32 * a5 + value.M33 * a4 + value.M34 * a3;
            float d42 = value.M31 * a5 + value.M33 * a2 + value.M34 * a1;
            float d43 = value.M31 * -a4 + value.M32 * a2 + value.M34 * a0;
            float d44 = value.M31 * a3 + value.M32 * -a1 + value.M33 * a0;

            result.M11 = +d11 * det; result.M12 = -d21 * det; result.M13 = +d31 * det; result.M14 = -d41 * det;
            result.M21 = -d12 * det; result.M22 = +d22 * det; result.M23 = -d32 * det; result.M24 = +d42 * det;
            result.M31 = +d13 * det; result.M32 = -d23 * det; result.M33 = +d33 * det; result.M34 = -d43 * det;
            result.M41 = -d14 * det; result.M42 = +d24 * det; result.M43 = -d34 * det; result.M44 = +d44 * det;

            return result;
        }

        public Matrix4x4 inverse { get { return Inverse(this); } }

        public static Matrix4x4 Ortho(float width, float height, float top, float zNear, float zFar)
        {
            Matrix4x4 result;

            result.M11 = 2f / width;
            result.M12 = result.M13 = result.M14 = 0f;
            result.M22 = 2f / height;
            result.M21 = result.M23 = result.M24 = 0f;
            result.M33 = 1f / (zNear - zFar);
            result.M31 = result.M32 = result.M34 = 0f;
            result.M41 = result.M42 = 0f;
            result.M43 = zNear / (zNear - zFar);
            result.M44 = 1f;

            return result;
        }

        public static Matrix4x4 Perspective(float fov, float aspect, float zNear, float zFar)
        {
            if (fov <= 0f || fov >= 3.141593f)
            {
                throw new ArgumentException();
            }
            if (zNear <= 0f)
            {
                throw new ArgumentException();
            }
            if (zFar <= 0f)
            {
                throw new ArgumentException();
            }
            if (zNear >= zFar)
            {
                throw new ArgumentException();
            }

            float num = 1f / (float)Math.Tan(fov * 0.5f);

            Matrix4x4 result;

            result.M11 = num / aspect;
            result.M12 = result.M13 = result.M14 = 0f;
            result.M22 = num;
            result.M21 = result.M23 = result.M24 = 0f;
            result.M33 = zFar / (zNear - zFar);
            result.M31 = result.M32 = 0f;
            result.M34 = -1f;
            result.M41 = result.M42 = result.M44 = 0f;
            result.M43 = (zNear * zFar) / (zNear - zFar);
            result.M44 = 1f;

            return result;
        }

        public static Matrix4x4 Translation(Vector3 position)
        {
            Matrix4x4 result;

            result.M11 = 1f; result.M12 = 0f; result.M13 = 0f; result.M14 = 0f;
            result.M21 = 0f; result.M22 = 1f; result.M23 = 0f; result.M24 = 0f;
            result.M31 = 0f; result.M32 = 0f; result.M33 = 1f; result.M34 = 0f;
            result.M41 = position.X; result.M42 = position.Y; result.M43 = position.Z; result.M44 = 1f;

            return result;
        }

        public static Matrix4x4 Scale(Vector3 scale)
        {
            Matrix4x4 result;

            result.M11 = scale.X;
            result.M12 = result.M13 = result.M14 = result.M21 = 0f;
            result.M22 = scale.Y;
            result.M23 = result.M24 = result.M31 = result.M32 = 0f;
            result.M33 = scale.Z;
            result.M34 = result.M41 = result.M42 = result.M43 = 0f;
            result.M44 = 1f;

            return result;
        }

        public static Matrix4x4 View(Vector3 position, Vector3 forward, Vector3 up)
        {
            Vector3 zaxis = forward - position;
            zaxis.Normalize();

            Vector3 xaxis = Vector3.Cross(up, zaxis);
            xaxis.Normalize();

            Vector3 yaxis = Vector3.Cross(zaxis, xaxis);

            Matrix4x4 result = Matrix4x4.Identity;

            result.M11 = xaxis.X; result.M21 = xaxis.Y; result.M31 = xaxis.Z;
            result.M12 = yaxis.X; result.M22 = yaxis.Y; result.M32 = yaxis.Z;
            result.M13 = zaxis.X; result.M23 = zaxis.Y; result.M33 = zaxis.Z;

            result.M41 = Vector3.Dot(xaxis, position);
            result.M42 = Vector3.Dot(yaxis, position);
            result.M43 = Vector3.Dot(zaxis, position);

            result.M41 = -result.M41;
            result.M42 = -result.M42;
            result.M43 = -result.M43;

            return result;
        }

        public static Matrix4x4 World(Vector3 position, Quaternion rotation, Vector3 scale)
        {
            Matrix4x4 result;

            // Rotation
            result.M11 = 1f - (2f * (rotation.Y * rotation.Y + rotation.Z * rotation.Z));
            result.M12 = 2f * (rotation.X * rotation.Y + rotation.Z * rotation.W);
            result.M13 = 2f * (rotation.Z * rotation.X - rotation.Y * rotation.W);
            result.M21 = 2f * (rotation.X * rotation.Y - rotation.Z * rotation.W);
            result.M22 = 1f - (2f * (rotation.Z * rotation.Z + rotation.X * rotation.X));
            result.M23 = 2f * (rotation.Y * rotation.Z + rotation.X * rotation.W);
            result.M31 = 2f * (rotation.Z * rotation.X + rotation.Y * rotation.W);
            result.M32 = 2f * (rotation.Y * rotation.Z - rotation.X * rotation.W);
            result.M33 = 1f - (2f * (rotation.Y * rotation.Y + rotation.X * rotation.X));

            // Position
            result.M41 = position.X;
            result.M42 = position.Y;
            result.M43 = position.Z;

            // Scale
            if (scale.X != 1f)
            {
                result.M11 *= scale.X;
                result.M12 *= scale.X;
                result.M13 *= scale.X;
            }
            if (scale.Y != 1f)
            {
                result.M21 *= scale.Y;
                result.M22 *= scale.Y;
                result.M23 *= scale.Y;
            }
            if (scale.Z != 1f)
            {
                result.M31 *= scale.Z;
                result.M32 *= scale.Z;
                result.M33 *= scale.Z;
            }

            result.M14 = 0f;
            result.M24 = 0f;
            result.M34 = 0f;
            result.M44 = 1.0f;

            return result;
        }

        public static Matrix4x4 Zero { get { return new Matrix4x4(0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f); } }
        public static Matrix4x4 Identity { get { return new Matrix4x4(1f, 0f, 0f, 0f, 0f, 1f, 0f, 0f, 0f, 0f, 1f, 0f, 0f, 0f, 0f, 1f); } }
    }
}
