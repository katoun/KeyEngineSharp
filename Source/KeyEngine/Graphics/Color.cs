using System;
using System.Runtime.InteropServices;
using KeyEngine.Core;

namespace KeyEngine.Graphics
{
	[StructLayout(LayoutKind.Sequential)]
	public struct Color : IEquatable<Color>
	{
		public float R;
		public float G;
		public float B;
		public float A;

		public float this[int index]
		{
			get
			{
				switch (index)
				{
					case 0: return R;
					case 1: return G;
					case 2: return B;
					case 3: return A;
					default: throw new ArgumentOutOfRangeException();
				}
			}
			set
			{
				switch (index)
				{
					case 0: R = value; break;
					case 1: G = value; break;
					case 2: B = value; break;
					case 3: A = value; break;
					default: throw new ArgumentOutOfRangeException();
				}
			}
		}

		public Color(float r, float g, float b, float a)
		{
			R = r; G = g; B = b; A = a;
		}

		public Color(float r, float g, float b)
		{
			R = r; G = g; B = b; A = 1.0f;
		}

		public static Color operator +(Color a, Color b)
		{
			return new Color(a.R + b.R, a.G + b.G, a.B + b.B, a.A + b.A);
		}

		public static Color operator -(Color a, Color b)
		{
			return new Color(a.R - b.R, a.G - b.G, a.B - b.B, a.A - b.A);
		}

		public static Color operator *(Color a, Color b)
		{
			return new Color(a.R * b.R, a.G * b.G, a.B * b.B, a.A * b.A);
		}

		public static Color operator *(Color a, float b)
		{
			return new Color(a.R * b, a.G * b, a.B * b, a.A * b);
		}

		public static Color operator *(float b, Color a)
		{
			return new Color(a.R * b, a.G * b, a.B * b, a.A * b);
		}

		public static Color operator /(Color a, float b)
		{
			return new Color(a.R / b, a.G / b, a.B / b, a.A / b);
		}

		public static bool operator ==(Color a, Color b)
		{
			return a.Equals(b);
		}

		public static bool operator !=(Color a, Color b)
		{
			return !a.Equals(b);
		}

		public static Color Lerp(Color a, Color b, float t)
		{
			return new Color(MathUtils.Lerp(a.R, b.R, t), MathUtils.Lerp(a.G, b.G, t), MathUtils.Lerp(a.B, b.B, t), MathUtils.Lerp(a.A, b.A, t));
		}

		public bool Equals(Color value)
		{
			return R.Equals(value.R) && G.Equals(value.G) && B.Equals(value.B) && A.Equals(value.A);
		}

		public override bool Equals(object value)
		{
			if (value == null)
				return false;

			if (!(value is Color))
				return false;

			return Equals((Color)value);
		}

		public override int GetHashCode()
		{
			return R.GetHashCode() ^ (G.GetHashCode() << 2) ^ (B.GetHashCode() >> 2) ^ (A.GetHashCode() >> 1);
		}

		public static readonly Color Red = new Color(1f, 0f, 0f, 1f);
		public static readonly Color Green = new Color(0f, 1f, 0f, 1f);
		public static readonly Color Blue = new Color(0f, 0f, 1f, 1f);
		public static readonly Color White = new Color(1f, 1f, 1f, 1f);
		public static readonly Color Black = new Color(0f, 0f, 0f, 1f);
		public static readonly Color Yellow = new Color(1f, 1f, 0f, 1f);
		public static readonly Color Cyan = new Color(0f, 1f, 1f, 1f);
		public static readonly Color Magenta = new Color(1f, 0f, 1f, 1f);
		public static readonly Color Gray = new Color(0.5f, 0.5f, 0.5f, 1f);
		public static readonly Color Grey = new Color(0.5f, 0.5f, 0.5f, 1f);
		public static readonly Color Clear = new Color(0f, 0f, 0f, 0f);
	}
}
