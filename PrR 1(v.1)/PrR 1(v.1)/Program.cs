using System;

namespace PrR_1_v._1_
{
    class Program
    {
        static void Main(string[] args)
        {
            Fraction instance1 = new Fraction (49, 100);
            Fraction instance2 = new Fraction(5, 677);
            Fraction instance3 = new Fraction(23, 10);
            Fraction A = new Fraction(1, 1);

            Console.WriteLine("Sqrt {0} = {1}",instance1 ,Fraction.Sqrt(instance1));

    //        A = instance1 + instance3;
    //        Console.WriteLine("+ {0}", A);
    //        A = instance1 - instance3;
    //        Console.WriteLine("- {0}", A);
    //        A = instance1 / instance3;
    //        Console.WriteLine("/ {0}", A);


    //        Console.WriteLine(Fraction.Equals(instance1, instance1));
    //        Console.WriteLine(instance1.CompareTo(instance2));
    //        // Console.WriteLine(Fraction.Decimal(instance2, 1000));
        }
    }
}
