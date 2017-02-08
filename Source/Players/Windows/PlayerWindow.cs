using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Threading;

using OpenTK;
using OpenTK.Input;

using KeyEngine.Graphics;

namespace KeyEngine
{
    public class PlayerWindow : GameWindow
    {
        Thread renderingThread;

        object updateLock = new object();

        bool viewportResized = true;
        bool windowMoved = true;

        int viewportWidth, viewportHeight;
        int positionX, positionY;
        float positionDX, positionDY;

        bool exit = false;

        public PlayerWindow() : base(800, 600)
        {
            // Make sure initial position are correct
            positionX = X;
            positionY = Y;

            Keyboard.KeyDown += delegate (object sender, KeyboardKeyEventArgs e)
            {
                if (e.Key == Key.Escape)
                {
                    Core.Application.Quit();
                }
            };

            Keyboard.KeyUp += delegate (object sender, KeyboardKeyEventArgs e)
            {
                if (e.Key == Key.F11)
                {
                    if (WindowState == WindowState.Fullscreen)
                        WindowState = WindowState.Normal;
                    else
                        WindowState = WindowState.Fullscreen;
                }
                    
            };

            Resize += delegate (object sender, EventArgs e)
            {
                // Note that we cannot call any OpenGL methods directly. What we can do is set
                // a flag and respond to it from the rendering thread.
                lock (updateLock)
                {
                    viewportResized = true;
                    viewportWidth = Width;
                    viewportHeight = Height;
                }
            };

            Move += delegate (object sender, EventArgs e)
            {
                // Note that we cannot call any OpenGL methods directly. What we can do is set
                // a flag and respond to it from the rendering thread.
                lock (updateLock)
                {
                    windowMoved = true;
                    positionDX = (positionX - X) / (float)Width;
                    positionDY = (positionY - Y) / (float)Height;
                    positionX = X;
                    positionY = Y;
                }
            };
        }

        protected override void OnLoad(EventArgs e)
        {
            Context.MakeCurrent(null); // Release the OpenGL context so it can be used on the new thread.

            renderingThread = new Thread(RenderLoopThread);
            renderingThread.IsBackground = true;
            renderingThread.Start();
        }

        protected override void OnUnload(EventArgs e)
        {
            exit = true; // Set a flag that the rendering thread should stop running.
            renderingThread.Join();

            base.OnUnload(e);
        }

        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            // Nothing to do!
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            // Nothing to do. Release the CPU to other threads.
            Thread.Sleep(1);
        }

        void RenderLoopThread()
        {
            MakeCurrent(); // The context now belongs to this thread. No other thread may use it!

            VSync = VSyncMode.On;

            // Since we don't use OpenTK's timing mechanism, we need to keep time ourselves;
            Stopwatch render_watch = new Stopwatch();
            Stopwatch update_watch = new Stopwatch();
            update_watch.Start();
            render_watch.Start();

            RenderLoop.Current.Start();           

            while (!exit)
            {
                Update(update_watch.Elapsed.TotalSeconds);
                update_watch.Reset();
                update_watch.Start();

                Render(render_watch.Elapsed.TotalSeconds);
                render_watch.Reset(); //  Stopwatch may be inaccurate over larger intervals.
                render_watch.Start(); // Plus, timekeeping is easier if we always start counting from 0.

                SwapBuffers();
            }

            Context.MakeCurrent(null);
        }

        void Update(double time)
        {
            lock (updateLock)
            {
                // When the user moves the window we make the particles react to
                // this movement. The reaction is semi-random and not physically
                // correct. It looks quite good, however.
                if (windowMoved)
                {
                    RenderLoop.Current.HandleWindowMoved(positionDX, positionDY);

                    windowMoved = false;
                }
            }

            RenderLoop.Current.Update(time);
        }

        public void Render(double time)
        {
            lock (updateLock)
            {
                if (viewportResized)
                {
                    RenderLoop.Current.HandleViewportResized(viewportWidth, viewportHeight);

                    viewportResized = false;
                }
            }

            RenderLoop.Current.Render(time);
        }
    }
}
