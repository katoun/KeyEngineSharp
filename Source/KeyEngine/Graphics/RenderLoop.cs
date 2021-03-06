using System;
using System.Collections.Generic;

using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;

using KeyEngine.Core;

namespace KeyEngine.Graphics
{
	public class RenderLoop
	{
		struct Particle
		{
			public Vector2 Position;
			public Vector2 Velocity;
			public Color4 Color;
		}

		List<Particle> particles = new List<Particle>();
		Random rand = new Random();

		const float GravityAccel = -9.81f;

		public void Start()
		{
			for (int i = 0; i < 64; i++)
			{
				Particle p = new Particle();
				p.Position = new Vector2((float)rand.NextDouble() * 2 - 1, (float)rand.NextDouble() * 2 - 1);
				p.Color.R = (float)rand.NextDouble();
				p.Color.G = (float)rand.NextDouble();
				p.Color.B = (float)rand.NextDouble();
				particles.Add(p);
			}

			GL.ClearColor(0, 0, 1, 1);
			GL.Enable(EnableCap.DepthTest);
			GL.Enable(EnableCap.PointSmooth);
			GL.PointSize(16);
		}

		public void Update(float dt)
		{
			// For simplicity, we use simple Euler integration to simulate particle movement.
			// This is not accurate, especially under varying timesteps (as is the case here).
			// A better solution would have been time-corrected Verlet integration, as
			// described here:
			// http://www.gamedev.net/reference/programming/features/verlet/
			for (int i = 0; i < particles.Count; i++)
			{
				Particle p = particles[i];

				p.Velocity.X = Math.Abs(p.Position.X) >= 1 ? -p.Velocity.X * 0.92f : p.Velocity.X * 0.97f;
				p.Velocity.Y = Math.Abs(p.Position.Y) >= 1 ? -p.Velocity.Y * 0.92f : p.Velocity.Y * 0.97f;
				if (p.Position.Y > -0.99)
				{
					p.Velocity.Y += GravityAccel * dt;
				}
				else
				{
					if (Math.Abs(p.Velocity.Y) < 0.02)
					{
						p.Velocity.Y = 0;
						p.Position.Y = -1;
					}
					else
					{
						p.Velocity.Y *= 0.9f;
					}
				}

				p.Position += p.Velocity * dt;
				if (p.Position.Y <= -1)
					p.Position.Y = -1;

				particles[i] = p;
			}
		}

		public void Render()
		{
			Matrix4 perspective = Matrix4.CreateOrthographic(2, 2, -1, 1);
			GL.MatrixMode(MatrixMode.Projection);
			GL.LoadMatrix(ref perspective);

			GL.MatrixMode(MatrixMode.Modelview);
			GL.LoadIdentity();

			GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

			GL.Begin(BeginMode.Points);
			foreach (Particle p in particles)
			{
				GL.Color4(p.Color);
				GL.Vertex2(p.Position);
			}
			GL.End();
		}

		public void Stop()
		{
			//TODO!!!
		}

		public void HandleViewportResized(int width, int height)
		{
			GL.Viewport(0, 0, width, height);
		}

		public void HandleWindowMoved(float deltaX, float deltaY)
		{
			for (int i = 0; i < particles.Count; i++)
			{
				Particle p = particles[i];
				p.Velocity += new Vector2(
					16 * (deltaX + 0.05f * (float)(rand.NextDouble() - 0.5)),
					32 * (deltaY + 0.05f * (float)(rand.NextDouble() - 0.5)));
				particles[i] = p;
			}
		}

		public static RenderLoop Current = new RenderLoop();
	}
}