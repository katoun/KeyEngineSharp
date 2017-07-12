using System;

using OpenTK;
using OpenTK.Input;

namespace KeyEngine.Graphics
{
    public class NativeWindow : GameWindow
    {
        int positionX, positionY;
        float positionDX, positionDY;

        public NativeWindow() : base(1280, 720)
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
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            RenderLoop.Current.HandleViewportResized(Width, Height);
        }

        protected override void OnMove(EventArgs e)
        {
            base.OnMove(e);

            positionDX = (positionX - X) / (float)Width;
            positionDY = (positionY - Y) / (float)Height;
            positionX = X;
            positionY = Y;

            RenderLoop.Current.HandleWindowMoved(positionDX, positionDY);
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            MakeCurrent();

            Core.Time.Start();
            RenderLoop.Current.Start();
        }

        protected override void OnUnload(EventArgs e)
        {
            base.OnUnload(e);

            RenderLoop.Current.Stop();
            Core.Time.Stop();

            Context.MakeCurrent(null);

            base.OnUnload(e);
        }

        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            base.OnUpdateFrame(e);

            Core.Time.Update();
            RenderLoop.Current.Update(Core.Time.DeltaTime);
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);

            RenderLoop.Current.Render();

            SwapBuffers();
        }
    }
}
