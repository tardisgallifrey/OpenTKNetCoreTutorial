using System;
using OpenTK;
using OpenTK.Graphics.OpenGL;
//The above are the minimum namespacesneeded to get a window
//to open with OpenTK.NEtStandard.

//If they don't turn from faded to bright, then check your syntax

//OpenTK has the window manager class.  They call it GameWindow.
//It kind of takes the place of glut, freeglut, or glu in C/C++

//The Graphics.OpenGL is where all the OpenGL classes are
//OpenGL is the older classes we are using here.
//OpenGL4 is the more modern classes
//You can run OpenGL ES as well.

namespace OpenTKNetStandardTut
{
    //When you declare your GameWindow, it must be sealed
    //You will be overriding some of its methods.
    
    public sealed class WindowOldStyle : GameWindow
    {
        //Since we are really only drawing a 2D figure in a 3D space,
        //We can set the Z-axis toa  default value
        //I set it to 2.4 as it made the triangle fill my window, edge to edge
        //In 3D, the Z-axis is used to make things appear near or far away.
        //It's the only normalized value that goes beyond -1 to +1
        float z = 2.4f;     //Set Z-axis so that X and Y max is 1.0f
                            //to edge of screen

        //We must use a constructor with GameWindow windows
        //In this one, we use the :base to set a default window size
        public WindowOldStyle()
            :base(600,600)
        {
            
        }

        //We override the OnLoad handler to add what we want 
        //to happen when the window loads.  The EventArgs is 
        //boilerplate code, but it can be used sometimes
        //
        //Here, we give the window a title and reset the
        //background color.
        //GL.ClearColor in C/C++ would have been
        //glClearColor( (red value, green value, blue value, alpha value);
        //The values being floats between 0.0 and 1.0
        //OpenTK has Color structs that have it preloaded for a given color
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            Title = "Hello OpenTK!";
            GL.ClearColor(Color.CornflowerBlue);

        }

        //We also override the OnRenderFrame method.
        //This is what is run with the .Run(1,10) which is
        //run 1 frame per cycle at 10 frames per second
        //Very slow so any animation can be monitored while 
        //programming.
        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);
 
            //First, we clear the color and depth bit so we can
            //change colors if we wish
            //Then we set our perspective for a 3D view
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            Matrix4 modelview = Matrix4.LookAt(Vector3.Zero, Vector3.UnitZ, Vector3.UnitY);
 
            GL.MatrixMode(MatrixMode.Modelview);
 
            GL.LoadMatrix(ref modelview);

            //I set these two for use
            //It allows us to see what our
            //Window size is as we change it 
            //with the mouse during display

            int Xmax = ClientRectangle.Width;
            int Ymax = ClientRectangle.Height;

            Console.WriteLine($"{Xmax}, {Ymax}");

            //This begins the drawing
            //GL.Begin is nearly the same as glBegin() in C/C++
            //We tell OpenGL in our video card that we are giving 
            //it a triangle to draw with our points (Vertex)
            GL.Begin(PrimitiveType.Triangles);
            //A Vertex3 sets an X,Y,Z coordinate
            //X,Y origin is center of window drawing area
            //A value of 1.0 seems to be half way to edge, sort of.
            //Z axis is positive moving towards the viewer and 
            //negative if moving away from viewer

            //The 0.0 float is a normalized coordinate 
            //our x,y = (0,0) to begin which is the 
            //center of the screen
            //We tell this portion of the triangle to be red
            
                GL.Color4(Color.Red); 
                GL.Vertex3(0.0f, 0.0f, z);
 
            //Now, we move along the X-axis to 1.0
            //It should be the right edge, but it is 
            //showing up on the left edge.  Don't know why yet.

                GL.Color4(Color.Green);
                GL.Vertex3(1.0f, 0.0f, z);
 
            //Going back to X = 0 and Y = 1, which should 
            //be top dead center and finishes our triangle
            //coordinates.

                GL.Color4(Color.Blue);
                GL.Vertex3(0.0f, 1.0f, z);

            //We call GL.End to finish.
            //In some cases, we may need to call GL.Flush before GL.End
            GL.End();

            //Everything we just drew is in a buffer off-screen
            //We finalize the frame with SwapBuffers()  to place
            //the one we just drew onto the screen.
            //If everything worked, a multi-colored triangle
            //will appear on screen.
            SwapBuffers();
        }

        //This overriden method is needed "as is" on most 
        //Windows.  It resets our viewport to match 
        //screen size.  It also resets the perspective view
        //to match as well.

        //How this works is that when you change the window size
        //With your mouse, Everthing in the window changes
        //In order to keep the proper perspective

        //What it does here, is keep the triangle mostly 
        //properly centered in the window as we made it originally.
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