using System;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;

namespace GLwin
{
    public sealed class Window2DOld : GameWindow
    {
        public int Xadjust {get;set;}
        public int Yadjust {get;set;}
        public Window2DOld(int width, int height)
            :base(width,height)
        {
            
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            Title = "2D Plotting Old Style";
            GL.ClearColor(Color.CornflowerBlue);

        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);

            
 
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            GL.ClearColor(Color.Blue);
            
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadIdentity();
            GL.Ortho(0, Width, Height,0, -1, 1);

            GL.Begin(PrimitiveType.Lines);
            //A Vertex3 sets an X,Y,Z coordinate
            //X,Y origin is center of window drawing area
            //A value of 1.0 seems to be half way to edge, sort of.
            //Z axis is on a diagonal, but can't tell how to use it yet.
            
                // Draw a Red 1x1 Square centered at origin
                GL.Color3(Color.Red); // Red
                GL.Vertex2(0, 0); 
                GL.Vertex2(Width,Height);   // x, y
                GL.Flush();
            GL.End();

            GL.Begin(PrimitiveType.LineLoop);
                GL.Vertex2((Width / 2.0) + Xadjust, Height / 2.0);
                GL.Vertex2((Width / 2.0) + 100 + Xadjust,Height / 2.0);
                GL.Vertex2((Width / 2.0) + 100 + Xadjust,(Height / 2.0) + 100);
                GL.Vertex2(Width / 2.0 + Xadjust, (Height / 2.0) + 100);
            GL.Flush();
            GL.End();

            SwapBuffers();
        }

        protected override void OnResize(EventArgs e)
        {
 
            base.OnResize(e);
 
            GL.Viewport(ClientRectangle.X, ClientRectangle.Y, ClientRectangle.Width, ClientRectangle.Height);
            Matrix4 projection = Matrix4.CreatePerspectiveFieldOfView((float)Math.PI / 4,
             Width / (float)Height, 1.0f, 64.0f);
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadMatrix(ref projection);

        }

        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            base.OnUpdateFrame(e);

            var Keys = Keyboard.GetState();

            if(Keys.IsKeyDown(Key.Right))
            {
                Xadjust += 5;
            }

            if(Keys.IsKeyDown(Key.Left))
            {
                Xadjust -= 5;
            }
        }

    }
}