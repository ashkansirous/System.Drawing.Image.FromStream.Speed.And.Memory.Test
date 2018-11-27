using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ImageTester
{
    class Program
    {
        static void Main(string[] args)
        {
            var fileBytes= File.ReadAllBytes("c:\\temp\\testimage.jpg");
            Console.WriteLine( $"Loaded {fileBytes.LongLength:##,###} Bytes into memory");

            //CallAlotOfTimes(fileBytes, CallingWithDefault);
            CallAlotOfTimes(fileBytes, CallingWithDefaultDisabledValidation);

            Console.ReadLine();
        }

        private static void CallAlotOfTimes(byte[] fileBytes, Func<MemoryStream, int > methodFunc)
        {
            var s = new Stopwatch();
            s.Start();
            Console.WriteLine($"#################### {methodFunc.Method}  #########################");

            for (int i = 0; i <= 200; i++)
            {
                using (var m = new MemoryStream(fileBytes))
                {
                    methodFunc(m);
                    if (i % 50 == 0)
                        Console.WriteLine($"Taking the image for the {i} time has completed");
                }
            }

            s.Stop();
            Console.WriteLine($"process took {s.ElapsedMilliseconds} Milliseconds");
        }

       public static int CallingWithDefault(MemoryStream m)
        {
            var image = System.Drawing.Image.FromStream(m);
            return image.Width;
        }

       public static int CallingWithDefaultDisabledValidation(MemoryStream m)
        {
            var image = System.Drawing.Image.FromStream(m,false, false);
            return image.Width;
        }

    }
}
