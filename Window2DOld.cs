using System;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;

//This is very similar to the Old Style Window class,
//Except that I make use of GL.Ortho to set the screen
//perspective to 2D which I wish to use for plotting
//
//I had a lot of trouble getting it to work.
//The screen is actually much larger than you see in the window
//OpenGL will gladly draw things outside that window where you'll
//never see them without additional controls.
//
//I finally made it work by trial and error with GL.Ortho and a 
//red line diagonally across the screen.  When my line appeared
//like it should, I knew I had it right.
//
//I added OpenTK.Input and tried out some extra math and 
//made the square move right and left.

namespace OpenTKNetStandardTut
{
    public sealed class Window2DOld : GameWindow
    {
        //These two are class properties for moving X and Y points 
        //while the window is open
        public int Xadjust {get;set;}
        public int Yadjust {get;set;}

        //In this call to our constructor, we build
        //Window2DOld so that we can set the size when opened.
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
            
            //This is the setting for a 2D screen
            //In GL.Ortho, the active values are left to right
            //Left edge coordinate, Right edge coord, Bottom edge coord, Top edge coord
            //The last two have control of Z and these are standard values.
            //GL.Ortho changes our coordinate system to
            // 0,0 is now the top left coordinate.
            // Window Width and Window Height are the bottom right coordinate.
            //For our default in Program that is
            // 0,0 to 800,600
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadIdentity();
            GL.Ortho(0, Width, Height,0, -1, 1);

            GL.Begin(PrimitiveType.Lines);
            //A Vertex3 sets an X,Y,Z coordinate
            //X,Y origin is center of window drawing area
            //A value of 1.0 seems to be half way to edge, sort of.
            //Z axis is on a diagonal, but can't tell how to use it yet.
            
                // Draw a Red Line diagonally across screen
                //Vertex 2 only uses X,Y which now match 
                //screen coordinates, not normalized ones
                //These are integers.
                //OpenTK overloads the Vertex class so we 
                //can use various types without changing callse
                //In C/C++ we would have to use glVertex2i
                //If we want integers
                GL.Color3(Color.Red); // Red
                GL.Vertex2(0, 0); 
                GL.Vertex2(Width,Height);   // x, y
                GL.Flush();
            GL.End();

            //LineLoop draws a squre with the provided coordinates.
            //By using this math here, we put the square at screen center
            //and moving to the right.
            //The Xadjust will move the square right or left as we get values from
            //the Keyboard handler below.
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

        //OnUpdateFrame is also overriden from GameWindow
        //
        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            base.OnUpdateFrame(e);

            //Get the state of all keys pressed during the space
            //between renders
            var Keys = Keyboard.GetState();

            //Check to see if Keys shows a Right Key is held down
            //If so, add 5 pixels to Xadjust each time we pass through here.
            if(Keys.IsKeyDown(Key.Right))
            {
                Xadjust += 5;
            }

            //Same as above, but if the Left Key is held down
            //subtract 5 pixels from the Xadjust property
            if(Keys.IsKeyDown(Key.Left))
            {
                Xadjust -= 5;
            }
        }

    }
}