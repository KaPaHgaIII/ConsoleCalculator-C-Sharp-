using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab13console
{
    class Test
    {
        private Calculator calc;

        public Test(Calculator calc)
        {
            this.calc = calc;
        }

        public void testCalc()
        {
            Dictionary<String, String> tests = new Dictionary<String, String>();

            tests.Add("1.5 + (2+4)/3", "3.5");
            tests.Add("1.5^2 + (2+4)/3", "4.25");
            tests.Add("sqrt(4)+2", "4");
            tests.Add("sin(30) - tan(45)", "-0.5");
            tests.Add("log(sqrt(64);2)", "3");
            tests.Add("ln(e^(3*sqrt(sqrt(16))))", "6");
            tests.Add("pi", "3.14...");
            tests.Add("sqrt(sqrt(256))", "4");
            tests.Add("5/0", "4");
            tests.Add("2^-3", "0.125");
            tests.Add("2^--3", "8");
            tests.Add("(-3)", "-3");
            tests.Add("2^2^3", "256");
            tests.Add("3+++5", "8");
            tests.Add("7*-(3+2)", "-35");
            tests.Add("2*(3+5)*(8+7)", "240");
            tests.Add("-3*(5+7)", "-36");
            tests.Add("(((4+3)*2+1)*6)","90");

            foreach (KeyValuePair<string, string> pair in tests)
            {
                try
                {
                    Console.WriteLine(pair.Key + " = " + calc.calculate(pair.Key) + "  ( " + pair.Value + " )");
                }
                catch (Exception)
                {
                    Console.WriteLine("Parse error: " + pair.Key);
                }
                Console.WriteLine();
            }
        }
    }
}
