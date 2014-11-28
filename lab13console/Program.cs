using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab13console
{
    class Program
    {
        static void Main(string[] args)
        {
            Calculator calc = new Calculator();
            if (args.Length>1 && args[1].Equals("-r"))
            {
                calc.setRadians();
            }
            Console.WriteLine(args[0] + " = " + calc.calculate(args[0]));
        }
    }
}
