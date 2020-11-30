using System;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace GLwin
{
    public sealed class WindowOldStyle : GameWindow
    {
        float z = 2.4f;     //Set Z-axis so that X and Y max is 1.0f
        
        public WindowOldStyle()
            :base(600,600)
        {
            
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            Title = "Hello OpenTK!";
            GL.ClearColor(Color.CornflowerBlue);

        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);
 
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            Matrix4 modelview = Matrix4.LookAt(Vector3.Zero, Vector3.UnitZ, Vector3.UnitY);
 
            GL.MatrixMode(MatrixMode.Modelview);
 
            GL.LoadMatrix(ref modelview);

            int Xmax = ClientRectangle.Width;
            int Ymax = ClientRectangle.Height;

            Console.WriteLine($"{Xmax}, {Ymax}");

            GL.Begin(PrimitiveType.Triangles);
            //A Vertex3 sets an X,Y,Z coordinate
            //X,Y origin is center of window drawing area
            //A value of 1.0 seems to be half way to edge, sort of.
            //Z axis is on a diagonal, but can't tell how to use it yet.
            
                GL.Color4(Color.Red); 
                GL.Vertex3(0.0f, 0.0f, z);
 
                GL.Color4(Color.Green);
                GL.Vertex3(1.0f, 0.0f, z);
 
                GL.Color4(Color.Blue);
                GL.Vertex3(0.0f, 1.0f, z);
            GL.End();

            SwapBuffers();
        }

        protected override void OnResize(EventArgs e)
        {
 
            base.OnResize(e);
 
            GL.Viewport(0, 0, ClientRectangle.Width, ClientRectangle.Height);
            Matrix4 projection = Matrix4.CreatePerspectiveFieldOfView((float)Math.PI / 4,
             Width / (float)Height, 1.0f, 64.0f);
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadMatrix(ref projection);

        }

    }
}