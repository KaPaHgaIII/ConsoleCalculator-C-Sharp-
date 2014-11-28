using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab13console
{
    class Calculator
    {
        private bool degrees = true;

        public void setDegrees()
        {
            degrees = true;
        }

        public void setRadians()
        {
            degrees = false;
        }

        public double calculate(String expr)
        {
            expr = expr.Replace('.', ',');
            expr = expr.Replace(" ", "");
            expr = expr.ToLower();
            return parse(expr);
        }

        public double parse(String expr)
        {
            //Console.WriteLine("Got: " + expr);
            expr = expr == "" ? "0" : expr;
            
            expr = openOuterBrackets(expr);

            if (isNum(expr))
            {
                return Double.Parse(expr);
            }
            try
            {
                return searchPow(expr);
            }
            catch (OperationsNotFoundException)
            {
            }
            try
            {
                return searchPlusMinus(expr);
            }
            catch (OperationsNotFoundException) { }
            try
            {
                return searchMultiplyDevide(expr);
            }
            catch (OperationsNotFoundException) { }
            
            try
            {
                return searchFunctions(expr);
            }
            catch (OperationsNotFoundException) {
            }
            try
            {
                return searchConstants(expr);
            }
            catch (OperationsNotFoundException){}
            
            throw new Exception("Parse Error");
        }

        private double searchPlusMinus(String expr)
        {
            int brackets = 0;
            for (int i = 0; i < expr.Length; i++)
            {
                String a = expr.Substring(i, 1);
                if (a.Equals("("))
                {
                    brackets++;
                    continue;
                }
                if (a.Equals(")"))
                {
                    brackets--;
                    continue;
                }

                if (brackets == 0)
                {
                    String operandA = expr.Substring(0, i);
                    String operandB = expr.Substring(i + 1, expr.Length - i - 1);
                    switch (a)
                    {
                        case "+":
                            return add(operandA, operandB);
                        case "-":
                            return deduct(operandA, operandB);
                    }
                }
            }
            throw new OperationsNotFoundException();
        }

        private double searchMultiplyDevide(String expr)
        {
            int brackets = 0;
            for (int i = 0; i < expr.Length; i++)
            {
                String a = expr.Substring(i, 1);
                if (a.Equals("("))
                {
                    brackets++;
                    continue;
                }
                if (a.Equals(")"))
                {
                    brackets--;
                    continue;
                }

                if (brackets == 0)
                {
                    String operandA = expr.Substring(0, i);
                    String operandB = expr.Substring(i + 1, expr.Length - i - 1);
                    switch (a)
                    {
                        case "*":
                            return multiply(operandA, operandB);
                        case "/":
                            return devide(operandA, operandB);
                    }
                }
            }
            throw new OperationsNotFoundException();
        }

        private double searchPow(String expr)
        {
            int brackets = 0;
            for (int i = 0; i < expr.Length; i++)
            {
                String a = expr.Substring(i, 1);
                if (a.Equals("("))
                {
                    brackets++;
                    continue;
                }
                if (a.Equals(")"))
                {
                    brackets--;
                    continue;
                }

                if (brackets == 0)
                {
                    String operandA = expr.Substring(0, i);
                    String operandB = expr.Substring(i + 1, expr.Length - i - 1);
                    if (a.Equals("^"))
                    {
                            return pow(operandA, operandB);
                    }
                }
            }
            throw new OperationsNotFoundException();
        }

        private double searchFunctions(String expr)
        {
            if (expr.IndexOf("(") != -1)
            {
                String func = expr.Substring(0, expr.IndexOf("("));
                String operand = expr.Substring(expr.IndexOf("(") + 1, expr.Length - expr.IndexOf("(") - 2);

                switch (func)
                {
                    case "sqrt":
                        return sqrt(operand);
                    case "sin":
                        return sin(operand);
                    case "cos":
                        return cos(operand);
                    case "tan":
                        return tan(operand);
                    case "asin":
                        return asin(operand);
                    case "acos":
                        return acos(operand);
                    case "atan":
                        return atan(operand);
                    case "ln":
                        return ln(operand);
                    case "log":
                        return log(operand);
                }
            }
            
            throw new OperationsNotFoundException();
        }

        private double searchConstants(String expr)
        {
            switch (expr)
            {
                case "pi":
                    return Math.PI;
                case "e":
                    return Math.E;
            }

            throw new OperationsNotFoundException();
        }


        private double degreesToRadians(double angle)
        {
            return Math.PI * angle / 180.0;
        }

        private double radiansToDegrees(double angle)
        {
            return 180.0 * angle / Math.PI;
        }

        private bool isNum(String expr)
        {
            expr = expr == "" ? "0" : expr;
            try
            {
                Double.Parse(expr);
            }
            catch (FormatException)
            {
                return false;
            }
            return true;
        }

        private String openOuterBrackets(String expr){
            if (expr.Substring(0, 1).Equals("(") && expr.Substring(expr.Length - 1, 1).Equals(")"))
            {
                int brackets = 0;
                for (int i = 0; i < expr.Length-1; i++)
                {
                    String a = expr.Substring(i, 1);
                    if (a.Equals("("))
                    {
                        brackets++;
                    }
                    if (a.Equals(")"))
                    {
                        brackets--;
                    }
                    if (brackets == 0)
                    {
                        return expr;
                    }
                }
                return expr.Substring(1, expr.Length - 2);
            }
            return expr;
        }


        private double add(String a, String b)
        {
            a = a == "" ? "0" : a;
            b = b == "" ? "0" : b;
            return parse(a) + parse(b);
        }

        private double deduct(String a, String b)
        {
            a = a == "" ? "0" : a;
            b = b == "" ? "0" : b;
            return parse(a) - parse(b);
        }

        private double multiply(String a, String b)
        {
            a = a == "" ? "1" : a;
            b = b == "" ? "1" : b;
            return parse(a) * parse(b);
        }

        private double devide(String a, String b)
        {
            a = a == "" ? "0" : a;
            b = b == "" ? "1" : b;
            return parse(a) / parse(b);
        }

        private double pow(String a, String b)
        {
            a = a == "" ? "0" : a;
            b = b == "" ? "1" : b;
            return Math.Pow(parse(a), parse(b));
        }

        private double sqrt(String a)
        {
            return Math.Sqrt(parse(a));
        }

        private double sin(String a)
        {
            return Math.Sin(degrees ? degreesToRadians(parse(a)) : parse(a));
        }

        private double cos(String a)
        {
            return Math.Cos(degrees ? degreesToRadians(parse(a)) : parse(a));
        }

        private double tan(String a)
        {
            return Math.Tan(degrees ? degreesToRadians(parse(a)) : parse(a));
        }

        private double asin(String a)
        {
            return degrees ? radiansToDegrees(Math.Asin(parse(a))) : Math.Asin(parse(a));
        }

        private double acos(String a)
        {
            return degrees ? radiansToDegrees(Math.Acos(parse(a))) : Math.Acos(parse(a));
        }

        private double atan(String a)
        {
            return degrees ? radiansToDegrees(Math.Atan(parse(a))) : Math.Atan(parse(a));
        }

        private double ln(String a)
        {
            return Math.Log(parse(a));
        }

        private double log(String a)
        {
            String operandA = a.Substring(0, a.IndexOf(";"));
            String operandB = a.Substring(a.IndexOf(";")+1);
            return Math.Log(parse(operandA), parse(operandB));
        }

    }
}
