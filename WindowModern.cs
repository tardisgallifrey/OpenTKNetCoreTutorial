using System;
using OpenTK;
using OpenTK.Graphics.OpenGL4;
using System.IO;

namespace GLwin
{
    public class WindowModern : GameWindow
    {

        	
        int pgmID;      //shader program ID
        int vsID;       //Vertex shader ID
        int fsID;       //Fragment Shader ID
        int attribute_vcol; //column attribute ID
        int attribute_vpos; //Position attribute ID
        int uniform_mview;  //Matrix ID
        int vbo_position;   //vertex buffer object position address
        int vbo_color;      //vertex buffer object color address
        int vbo_mview;      //vertex buffer object matrix vew address
        Vector3[] vertdata; //Vector arrays to use in VBO
        Vector3[] coldata;
        Matrix4[] mviewdata;





        public WindowModern(int width, int height)
            :base(width,height)
        {
            
        }

        //Window Initialise program, 
        //Inits, shaders and everything.
        void initProgram()
        {
            pgmID = GL.CreateProgram();     //Create Shader Progrm
            loadShader("./vs.glsl", ShaderType.VertexShader, pgmID, out vsID);      //load vertex shader file
            loadShader("./fs.glsl", ShaderType.FragmentShader, pgmID, out fsID);    //load fragment shader file
            GL.LinkProgram(pgmID);
            Console.WriteLine(GL.GetProgramInfoLog(pgmID));
            attribute_vpos = GL.GetAttribLocation(pgmID, "vPosition");
            attribute_vcol = GL.GetAttribLocation(pgmID, "vColor");
            uniform_mview = GL.GetUniformLocation(pgmID, "modelview");
 
            if (attribute_vpos == -1 || attribute_vcol == -1 || uniform_mview == -1)
            {
                Console.WriteLine("Error binding attributes");
            }

            GL.GenBuffers(1, out vbo_position);
            GL.GenBuffers(1, out vbo_color);
            GL.GenBuffers(1, out vbo_mview);

        }

        void loadShader(String filename,ShaderType type, int program, out int address)
        {
            address = GL.CreateShader(type);
            using (StreamReader sr = new StreamReader(filename))
            {
                GL.ShaderSource(address, sr.ReadToEnd());
            }
            GL.CompileShader(address);
            GL.AttachShader(program, address);
            Console.WriteLine(GL.GetShaderInfoLog(address));
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            initProgram();
 
            vertdata = new Vector3[] { 
                //These are the same as the GL.Vertex3
                new Vector3(-0.8f, -0.8f, 0f),
                new Vector3( 0.8f, -0.8f, 0f),
                new Vector3( 0f,  0.8f, 0f)};
 
 
            coldata = new Vector3[] { 
                new Vector3(1f, 0f, 0f),
                new Vector3( 0f, 0f, 1f),
                new Vector3( 0f,  1f, 0f)};
 
 
            mviewdata = new Matrix4[]{
                Matrix4.Identity
            };
 
            Title = "New Style Shaders and Vertexes";
 
            GL.ClearColor(Color.CornflowerBlue);
            GL.PointSize(5f);
        }

        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            base.OnUpdateFrame(e);

            GL.BindBuffer(BufferTarget.ArrayBuffer, vbo_position);
            GL.BufferData<Vector3>(BufferTarget.ArrayBuffer, (IntPtr)(vertdata.Length * Vector3.SizeInBytes), 
                vertdata, BufferUsageHint.StaticDraw);
            GL.VertexAttribPointer(attribute_vpos, 3, VertexAttribPointerType.Float, false, 0, 0);
 
 
 
            GL.BindBuffer(BufferTarget.ArrayBuffer, vbo_color);
            GL.BufferData<Vector3>(BufferTarget.ArrayBuffer, (IntPtr)(coldata.Length * Vector3.SizeInBytes), 
                coldata, BufferUsageHint.StaticDraw);
            GL.VertexAttribPointer(attribute_vcol, 3, VertexAttribPointerType.Float, true, 0, 0);

            GL.UniformMatrix4(uniform_mview, false, ref mviewdata[0]);

            GL.UseProgram(pgmID);
 
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);


        }
        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);
 
            GL.Viewport(0, 0, Width, Height);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            GL.Enable(EnableCap.DepthTest);

            GL.EnableVertexAttribArray(attribute_vpos);
            GL.EnableVertexAttribArray(attribute_vcol);

            GL.DrawArrays(PrimitiveType.Triangles, 0, 3);

            GL.DisableVertexAttribArray(attribute_vpos);
            GL.DisableVertexAttribArray(attribute_vcol);
 
            GL.Flush();
 
            SwapBuffers();
        }
    }
}