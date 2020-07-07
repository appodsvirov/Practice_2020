using System;
using System.Collections.Generic;
using System.Text;
using System.Numerics;
using PrR_1_v._1_;

namespace PrR_2_v._1_
{
    class FComplex: ICloneable, IEquatable<FComplex>, IComparable, IComparable<FComplex>
    {
        private static readonly BigInteger _eps = 5;
        private Fraction _real;
        private Fraction _imaginary;

        public Fraction Real
        {
            get =>
                _real;

            set =>
                _real = value;
        }
        public Fraction Imaginary
        {
            get =>
                _imaginary;

            set =>
                _imaginary = value;
        }

        #region ->Designer
        public FComplex(BigInteger Rnum, BigInteger Rdenom,
            BigInteger Inum, BigInteger Idenom)
        {
            Real = new Fraction(Rnum, Rdenom);
            Imaginary = new Fraction(Inum, Idenom);
        }

        public FComplex(FComplex other)
        {
            Real = other.Real;
            Imaginary = other.Imaginary;
        }

        public FComplex(Fraction real, Fraction imaginary)
        {
            Real = real;
            Imaginary =  imaginary;
        }
        #endregion;

        #region ICloneable
        public object Clone()
        {
            return new FComplex(this);
        }
        #endregion

        #region IEquatable
        public override bool Equals(object obj)
        {
            if (obj is null)
            {
                return false;
            }
            if (obj is FComplex fcomplex)
            {
                return Equals(fcomplex);
            }
            return false;
        }
        public bool Equals(FComplex fcomplex)
        {
            if (fcomplex is null)
            {
                return false;
            }
            return Real == fcomplex.Real
                && Imaginary == fcomplex.Imaginary;
        }
        #endregion

        #region IComparable
        public int CompareTo(object obj)
        {
            if (obj is null)
            {
                throw new ArgumentNullException(nameof(obj));
            }
            if (obj is FComplex fcmplx)
            {
                return CompareTo(fcmplx);
            }
            throw new ArgumentException("Invalid type", nameof(obj));
        }

        public int CompareTo(FComplex fcmplx)
        {
            if (fcmplx is null)
            {
                throw new ArgumentNullException(nameof(fcmplx));
            }

            var realComparisonResult = Real.CompareTo(fcmplx.Real);
            if (realComparisonResult == 0)
            {
                var imaginaryComparisonResult = Imaginary.CompareTo(fcmplx.Imaginary);

                return imaginaryComparisonResult;
            }
            return realComparisonResult;
        }
        #endregion

        #region UsualArithmetic
        public static FComplex Add(FComplex first, FComplex second)
        {
            return new FComplex(first.Real + second.Real, first.Imaginary + second.Imaginary);
        }
        public static FComplex Sub(FComplex first, FComplex second)
        {
            return new FComplex(first.Real - second.Real, first.Imaginary - second.Imaginary);
        }
        public static FComplex Mult(FComplex first, FComplex second)
        {
            return new FComplex(first.Real * second.Real - first.Imaginary * second.Imaginary,
                first.Real * second.Imaginary + first.Imaginary * second.Real);
        }
        public static FComplex Div(FComplex first, FComplex second)
        {
            return new FComplex(
                (first.Real * second.Real + first.Imaginary * second.Imaginary) /
                (second.Real * second.Real + second.Imaginary * second.Imaginary),
                (first.Imaginary * second.Real - first.Real * second.Imaginary) /
                (second.Real * second.Real + second.Imaginary * second.Imaginary));
        }
        public static FComplex operator +(FComplex first, FComplex second)
        {
            return FComplex.Add(first, second);
        }
        public static FComplex operator -(FComplex first, FComplex second)
        {
            return FComplex.Sub(first, second);
        }
        public static FComplex operator *(FComplex first, FComplex second)
        {
            return FComplex.Mult(first, second);
        }
        public static FComplex operator /(FComplex first, FComplex second)
        {
            return FComplex.Div(first, second);
        }
        #endregion

        #region HardArithmetic
        public static Fraction Module(FComplex complex)
        {
            return new Fraction(
                Fraction.Sqrt(complex.Real * complex.Real + complex.Imaginary * complex.Imaginary));
        }
        public static Fraction PI()
        {
            var result = new Fraction(0, 1);
            for (int i = 1; i < _eps; i++)
            {
                result += new Fraction(BigInteger.Pow(-1, i - 1),  2 * i - 1);
            }
            return result;
        }
        public static Fraction Sin(Fraction frac)
        {
            var result = new Fraction(0, 1);
            for (int i = 1; i < _eps; i++)
            {
                result += new Fraction(BigInteger.Pow(-1, i - 1), Fraction.Factorial(2 * i - 1)) *
                    Fraction.Pow(frac, 2*i - 1);
               // Console.WriteLine(Fraction.Decimal(result, 5));
            }
            return result;
        }
        public static Fraction Cos(Fraction frac)
        {
            var result = new Fraction(0, 1);
            for (int i = 1; i < _eps; i++)
            {
                result += new Fraction(BigInteger.Pow(-1, i - 1), Fraction.Factorial(2 * i - 2)) *
                    Fraction.Pow(frac, 2 * i - 2);
                 //Console.WriteLine(Fraction.Decimal(result, 5));
            }
            return result;
        }
        private static Fraction Atan(Fraction frac)
        {
            var sign = new Fraction(-1, 1);
            var result = new Fraction(0, 1);
            for (int i = 1; i < _eps; i++)
            {
                result += Fraction.Pow(sign, i - 1) *
                    new Fraction(Fraction.Pow(frac, 2 * i - 1) * new Fraction(1, 2 * i - 1));
            }
            return result; 
        }
        public static Fraction Argument(FComplex complex)
        {
            var temp = new Fraction(complex.Imaginary / complex.Real);
            return FComplex.Atan(temp);
        }
        public static FComplex Pow(FComplex complex, BigInteger num)
        {
            var flag = new bool();
            flag = false;
            FComplex result;
            if (num == 0) return new FComplex(1, 1, 1, 1);
            else if (num < 0)
            {
                flag = true;
                num *= -1;
            }
            result = new FComplex(complex);

            for (int i = 0; i < num - 2; i++)
            {
                result *= result;
            }

            if (flag == true)
            {
                return new FComplex(1, 1, 1, 1)/ result;
            }
            return result;
        }
        public static FComplex[] SqrtN(FComplex complex, BigInteger num)
        {
            var flag = new bool();
            flag = false;
            if (num == 0)
            {
                FComplex[] temp = new FComplex[1];
                temp[0] = new FComplex(complex);
                return temp;
            }
            if (num < 0)
            {
                flag = true;
                num *= -1;
            }
            var arg = FComplex.Argument(complex);
            var sqrtmodule = Fraction.SqrtN(FComplex.Argument(complex), num);
            var mult = new Fraction(1, num);
            Fraction Re;
            Fraction Im;
            FComplex[] result = new FComplex[(int)num];
            for (int i = 0; i < num; i++)
            {
                var temp = new Fraction(2 * i, 1);
                Re = new Fraction(sqrtmodule *
                    FComplex.Cos((arg + temp * FComplex.PI()) * mult));
                Im = new Fraction(sqrtmodule *
                    FComplex.Sin((arg + temp * FComplex.PI()) * mult));
                result[i] = new FComplex(Re, Im);
                if (flag == true)
                {
                    result[i] = new FComplex(1, 1, 1, 1) / result[i];
                }
            }
            return result;
        }
        #endregion
        public override string ToString()
        {
            return $"[{Real} + {Imaginary}i]";
        }
    }
}
