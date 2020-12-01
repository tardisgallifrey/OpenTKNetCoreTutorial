using System;  //In Program.cs, we only need System namespace


namespace OpenTKNetStandardTut
{
    class Program
    {
        static void Main(string[] args)
        {
            //Use Console.WriteLine and a switch/case 
            //To build a short menu
            Console.WriteLine("Choose which Window?");
            Console.WriteLine("1. 3D Window OLD GL style.");
            Console.WriteLine("2. 3D Window Modern OpenGL4 style.");
            Console.WriteLine("3. 2D Window Old GL Style.");
            var choice = Console.ReadLine();

            //Console.ReadLine returns characters
            //Convert.ToInt32 needed to use in a case, easily.
            switch(Convert.ToInt32(choice))
            {
                //Everything between case 1
                //and break is run when you 
                //choose 1 in the menu
                case 1:
                    //Create an instance of the Old Style Window
                    var win = new WindowOldStyle();
                    //Tell your new window to run
                    //with 1 update per second
                    //and 10 frames per second
                    win.Run(1,10);
                    //Run a Console.Clear to clean up 
                    //Console screen when you close
                    //the window
                    Console.Clear();
                    break;
                case 2:
                    //Same routine here, but we choose the 
                    //modern style window.
                    //It's instance can receive a window size
                    var win2 = new WindowModern(600,400);
                    win2.Run(1,10);
                    Console.Clear();
                    break;
                case 3:
                    //Same here for the 2D window
                    var win3 = new Window2DOld(800,600);
                    win3.Run(1,10);
                    Console.Clear();
                    break;
                default:
                    //A little bit of error checking.
                    Console.WriteLine("You didn't make a good choice.  Bye:");
                    break;
            }
    
        }
    }
}
