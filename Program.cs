using System;


namespace GLwin
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Choose which Window?");
            Console.WriteLine("1. 3D Window OLD GL style.");
            Console.WriteLine("2. 3D Window Modern OpenGL4 style.");
            Console.WriteLine("3. 2D Window Old GL Style.");
            var choice = Console.ReadLine();

            switch(Convert.ToInt32(choice))
            {
                case 1:
                    var win = new WindowOldStyle();
                    win.Run(1,10);
                    Console.Clear();
                    break;
                case 2:
                    var win2 = new WindowModern(600,400);
                    win2.Run(1,10);
                    Console.Clear();
                    break;
                case 3:
                    var win3 = new Window2DOld(800,600);
                    win3.Run(1,10);
                    Console.Clear();
                    break;
                default:
                    Console.WriteLine("You didn't make a good choice.  Bye:");
                    break;
            }
    
        }
    }
}
