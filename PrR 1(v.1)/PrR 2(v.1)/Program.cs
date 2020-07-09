using System;
using System.Numerics;
using PrR_1_v._1_;
namespace PrR_2_v._1_
{
    class Program
    {
        static void Main(string[] args)
        {
            FComplex GetRndFC()
            {
                Random rnd = new Random();
                return new FComplex(rnd.Next(1, 10), rnd.Next(1, 10),
                    rnd.Next(1, 10), rnd.Next(1, 10));
            }
            Random Rnd = new Random();
            var frac = new Fraction(1, 40);
            var ince1 = new FComplex(1, 2, 1, 2);
            var ince2 = new FComplex(1, 15, 20, 4);
            var copyince = new FComplex(ince1);
            var temp = new FComplex(frac, frac);
            
            temp = (FComplex)ince2.Clone();
            Console.WriteLine("ICloneable:\n\ttemp = {0}\n", temp);
            Console.WriteLine("IEquatable :\n\t{0} == {1} ? - {2}\n\t{1} == {3}? - {4}\n",
                nameof(ince1), nameof(ince2),ince1.Equals(ince2),
                nameof(temp), ince2.Equals(temp));
            Console.WriteLine(
                "IComparable:\n\t{0} > {1} ? = {2}\n\t{1} > {0} ? = {3}\n\t{1} > {1} ? = {4}\n",
                nameof(ince1), nameof(ince2),
                ince1.CompareTo(ince2), ince2.CompareTo(ince1), ince1.CompareTo(ince1));
            Console.WriteLine("{0} + {1} = {2}", ince1, ince2, ince1 + ince2);
            Console.WriteLine("{0} - {1} = {2}", ince1, ince2, ince1 - ince2);
            Console.WriteLine("{0} * {1} = {2}", ince1, ince2, ince1 * ince2);
            Console.WriteLine("{0} / {1} = {2}\n", ince1, ince2, ince1 / ince2);

            FComplex temp2 = GetRndFC();
            Console.WriteLine("Module ({0}) = {1}", temp2, FComplex.Module(temp2));
            FComplex temp3 = GetRndFC();
            Console.WriteLine("Argument ({0}) = {1}", temp3,FComplex.Argument(temp3));
            FComplex temp4 = new FComplex(1, 44, 1, 217);
            Console.WriteLine("Pow : ({0})^3 = {1}", temp4, FComplex.Pow(temp4, 3));
            FComplex[] arr = new FComplex[6];

            arr = FComplex.SqrtN(temp4, 3);
            Console.WriteLine("\n\n\n_____________________\n");
        }
    }
}
