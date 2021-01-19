using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TD2
{
    class Program
    {
        static void Main(string[] args)
        {
            MyImage image = new MyImage("./Images/Test001.bmp");
            image.From_Image_To_File("./Images/File.bmp");
            Console.ReadKey();
        }
    }
}
