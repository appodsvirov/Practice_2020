using System;
using System.Collections.Generic;
using System.Text;
using System.Numerics;

namespace PrR_1_v._1_
{
    public class Fraction: ICloneable, IEquatable<Fraction>,
        IComparable, IComparable<Fraction>
    {
        private static readonly Fraction _eps = new Fraction(1, 10);
        private BigInteger _numerator;
        private BigInteger _denominator;

        public Fraction(BigInteger numerator, BigInteger denominator)
        {
            if (denominator == 0) throw new DivideByZeroException("Divide by zero");
            _numerator = numerator;
            _denominator = denominator;
            Reduction();
        }
        public Fraction(Fraction other)
        {
            Numerator = other.Numerator;
            Denominator = other.Denominator;
            Reduction();
        }


        private void Reduction()
        {
            var nod = new BigInteger ();
            nod = BigInteger.GreatestCommonDivisor(Numerator, Denominator);
            Numerator /= nod;
            Denominator /= nod;
            if (Denominator < 0)
            {
                Numerator *= -1;
                Denominator *= -1;
            } 
        }
        public BigInteger Numerator
        {
            get =>
                _numerator;

            private set =>
                _numerator = value;
        }
        public BigInteger Denominator
        {
            get =>
                _denominator;

            private set =>
                _denominator = value;
        }

        #region ICloneable
        public object Clone()
        {
            return new Fraction(this);
        }
        #endregion

        #region IEquatable
        public override bool Equals(object obj)
        {
            if (obj is null)
            {
                return false;
            }
            if (obj is Fraction frac)
            {
                return Equals(frac);
            }
            return false;
        }
        public bool Equals(Fraction frac)
        {
            if (frac is null)
            {
                return false;
            }
            return Numerator == frac.Numerator 
                && Denominator == frac.Denominator;
        }
        #endregion

        #region  IComparable
        public int CompareTo(object obj)
        {
            if(obj is null)
            {
                throw new ArgumentNullException(nameof(obj));
            }
            if(obj is Fraction frac)
            {
                return CompareTo(frac);
            }
            throw new ArgumentException("Invalid type", nameof(obj));
        }
        public int CompareTo(Fraction cfrac)
        {
            var frac = new Fraction(cfrac);
            var tis = new Fraction(this);

            if (frac is null)
                throw new ArgumentNullException(nameof(frac));

            tis.Numerator *= frac.Denominator;
            frac.Numerator *= tis.Denominator;

            return tis.Numerator.CompareTo(frac.Numerator);  
        }
        #endregion

        #region Arithmetic
        public static Fraction Pow(Fraction cfrac, BigInteger num)
        {
            Fraction frac;
            if (num == 0) return new Fraction(1, 1);
            else if (num < 0)
            {
                num *= -1;
                frac = new Fraction(cfrac.Denominator, cfrac.Numerator);
            }
            else if (num == 1) return new Fraction(cfrac);
            else frac = new Fraction(cfrac);

            for (int i = 0; i < num - 2; i++)
            {
                frac *= frac;
            }
            frac.Reduction();
            return frac;
        }
        public static Fraction Sqrt(Fraction cfrac)
        {
            if (cfrac.Numerator < 0)
                throw new ArithmeticException("I can't get coplex number :c");

            var frac = new Fraction(cfrac);
            var half = new Fraction(1, 2);
            var A1 = new Fraction(cfrac);
            var A2 = new Fraction(cfrac);

            A1 = half * frac;
            A2 = half * (A1 + (frac / A1));

            while (A2 - A1 > _eps || A1 - A2 > _eps)
            {
                A1 = A2;
                A2 = half * (A1 + frac / A1);
            }
            A2.Reduction();
            return A2;


        }
        public static Fraction SqrtN(Fraction cfrac, BigInteger num)
        {
            if (cfrac.Numerator < 0)
                throw new ArithmeticException("I can't get coplex number :c");

            var frac = new Fraction(cfrac);
            var half = new Fraction(1, num);
            var mult = new Fraction(num - 1, 1);
            var A1 = new Fraction(cfrac);
            var A2 = new Fraction(cfrac);

            A1 = half * frac;
            A2 = half * (mult *A1 + (frac / Pow(A1, num - 1)));

            while (A2 - A1 > _eps || A1 - A2 > _eps)
            {
                A1 = A2;
                A2 = half * (mult* A1 + frac / Pow(A1, num - 1));
            }
            A2.Reduction();
            return A2;
        }
        public static string Decimal(Fraction cfrac, BigInteger eps)
        {
            var frac = new Fraction(cfrac);
            if (eps < 0) throw new ArgumentException("Second argument must be unsigned number");
            var result = new StringBuilder();
            var @int = new BigInteger();
            var temp = new BigInteger();
            @int = 0;
            while (frac.Numerator > frac.Denominator) {
                frac.Numerator -= frac.Denominator;
                @int++;
            }
            result.Append(@int.ToString());
            result.Append(".");
            if (eps == 1) result.Append("0");
            frac.Numerator *= 10;
            for (int i = 0; i < eps - 1; i++)
            {
                if (frac.Numerator < frac.Denominator)
                {
                    frac.Numerator *= 10;
                    result.Append("0");
                }
                temp = frac.Numerator / frac.Denominator;
                result.Append(temp.ToString());
                frac.Numerator -= temp * frac.Denominator;
                frac.Numerator *= 10;
            }
            return result.ToString();
        }


        public static Fraction Mult(Fraction num1, Fraction num2)
        {
            return new Fraction(num1.Numerator * num2.Numerator, num1.Denominator * num2.Denominator);
        }
        public static Fraction Div(Fraction cnum1, Fraction cnum2)
        {
            var num1 = new Fraction(cnum1);
            var num2 = new Fraction(cnum2);
            if (num2.Numerator == 0)
                throw new DivideByZeroException("Divide by zero");
            num1.Numerator *= num2.Denominator;
            num1.Denominator *= num2.Numerator;
            num1.Reduction();
            return num1;
        }
        public static Fraction Add(Fraction cnum1, Fraction cnum2)
        {
            var num1 = new Fraction(cnum1);

            var num2 = new Fraction(cnum2);
            num1.Numerator *= num2.Denominator;
            num2.Numerator *= num1.Denominator;
            num1.Denominator *= num2.Denominator;
            num2.Denominator = num1.Denominator;

            num1.Numerator += num2.Numerator;

            num1.Reduction();

            return num1;
        }
        public static Fraction Sub(Fraction cnum1, Fraction cnum2)
        {
            var num1 = new Fraction(cnum1);
            var num2 = new Fraction(cnum2);

            num1.Numerator *= num2.Denominator;
            num2.Numerator *= num1.Denominator;
            num1.Denominator *= num2.Denominator;
            num2.Denominator = num1.Denominator;

            num1.Numerator -= num2.Numerator;

            num1.Reduction();

            return num1;
        }

        public static Fraction operator *(Fraction first, Fraction second)
        {
            return Mult(first, second);
        }
        public static Fraction operator /(Fraction first, Fraction second)
        {
            return Div(first, second);
        }
        public static Fraction operator +(Fraction first, Fraction second)
        {
            return Add(first, second);
        }
        public static Fraction operator -(Fraction first, Fraction second)
        {
            return Sub(first, second);
        }
        public static BigInteger Factorial(int num)
        {
            BigInteger res = 1;
            for (int i = num; i > 1; i--)
                res *= i;
            return res;
        }
        #endregion

        #region Logic
        public static bool operator ==(Fraction left, Fraction right)
        {
            return left.Equals(right);
        }
        public static bool operator !=(Fraction left, Fraction right)
        {
            return !left.Equals(right);
        }
        public static bool operator >(Fraction left, Fraction right)
        {
            return left.CompareTo(right) > 0;
        }
        public static bool operator <(Fraction left, Fraction right)
        {
            return left.CompareTo(right) < 0;
        }
        public static bool operator >=(Fraction left, Fraction right)
        {
            return left.CompareTo(right) >= 0;
        }
        public static bool operator <=(Fraction left, Fraction right)
        {
            return left.CompareTo(right) <= 0;
        }
        #endregion



        public override string ToString()
        {
            return $"<{Numerator}>/<{Denominator}>";
        }
    }
}
