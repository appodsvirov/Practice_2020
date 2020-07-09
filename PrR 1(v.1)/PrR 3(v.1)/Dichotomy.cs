using System;
using PrR_1_v._1_;

namespace PrR_3_v._1_
{
 


    class Dichotomy
    {
        public delegate Fraction Func(Fraction X);
        public static Fraction Finding_the_root(Fraction x0, Fraction x1,
            Func F, Fraction eps)
        {
            if (x0 is null || x1 is null || F is null || eps is null)
                throw new ArgumentException("Bad arguments");
            var Vx0 = new Fraction(F(x0));
            var Vx1 = new Fraction(F(x1));
            var frac0 = new Fraction(0, 1);

            if (Vx0 * Vx1 >= frac0)
                throw new ArgumentException("The values must be of different characters");

            if (x0 > x1)
                throw new ArgumentException("The first argument is the left border");

            var half = (x1 + x0) * new Fraction(1, 2);
            var Vhalf = new Fraction(F(half));

            if (Vhalf < eps && Vhalf * new Fraction(-1, 1) < eps)  
                return half;

            if (Vx0 * Vhalf < frac0)
                return Finding_the_root(x0, half, F, eps);

            if (Vhalf * Vx1 < frac0)
                return Finding_the_root(half, x1, F, eps);

            throw new InvalidOperationException("It's impossible");
        }
        static void Main(string[] args)
        {
            Func display1 = x => //3x ^ 3 - 5x ^ 2 - x - 2 = 0 // x = 2
            new Fraction(3, 1) * x * x * x +
            new Fraction(-5, 1) * x * x +
            new Fraction(-1, 1) * x +
            new Fraction(-2, 1);
            Func display2 = x => x * x - new Fraction(9, 1);// x^2 - 9// x= +- 3
            Func display3 = x => Fraction.Pow(x, -1) - new Fraction(1, 1);// 1/x - 1// x = 1
            try
            {
                Console.WriteLine("f(x) = 3x ^ 3 - 5x ^ 2 - x - 2 = 0 // x = {0}",
                    Fraction.Decimal(Finding_the_root(new Fraction(-1, 1), new Fraction(72, 7),
                    display1 , new Fraction(1, 100000)), 10)
                    );
                Console.WriteLine("f(x) = x^2 - 9 // x = {0}",
                    Fraction.Decimal(Finding_the_root(new Fraction(-1, 1), new Fraction(6, 1),
                    display2, new Fraction(1, 1000000)), 10)
                    );
                Console.WriteLine("f(x) = 1/x - 1 // x = {0}", 
                    Fraction.Decimal(Finding_the_root(new Fraction(1, 2), new Fraction(3, 1),
                    display3, new Fraction(1, 1000000)), 10)
                    );   
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine("Arguments are bad : {0}", ex.Message);
            }
            catch(InvalidOperationException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (DivideByZeroException ex)
            {
                Console.WriteLine(ex.Message);
            }

        }
    }
}

