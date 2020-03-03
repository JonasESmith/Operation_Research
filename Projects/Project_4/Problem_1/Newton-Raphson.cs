using System;

namespace Newton_Raphson
{
    class Program
    {
        static void Main(string[] args)
        {
            solve(new Vector(1, 1), 2);
        }


        static void solve(Vector initial, int iterations)
        {
            Vector x = initial;

            Console.WriteLine("┌─────┬──────────────────────┬──────────────┬─────────────────────────┐");
            Console.WriteLine("│ k   │ X                    │ f(X)         │ grad(X)                 |");
            Console.WriteLine("├─────┼──────────────────────┼──────────────┼─────────────────────────┤");

            bool diverge = false;
            bool noInverse = false;

            for (int i = 0; i <= iterations; i++)
            {
                //If the function value is large, assume it is diverging.
                if (Math.Abs(f(x)) > 1000000)
                {
                    diverge = true;
                    break;
                }

                Console.Write("│ " + (i + "").PadRight(3) + " │ ");
                Console.Write((x.round(5) + "").PadRight(20) + " │ ");
                Console.Write((Math.Round(f(x), 8) + "").PadRight(12) + " │ ");
                Console.WriteLine((grad(x).round(5) + "").PadRight(23) + " │");

                Matrix hMatrix = hessian(x);
                if (hMatrix.det() == 0)
                {
                    noInverse = true;
                    break;
                }

                x = x - (hMatrix.inverse() * grad(x));
            }

            Console.WriteLine("└─────┴──────────────────────┴──────────────┴─────────────────────────┘");
            if (diverge)
                Console.WriteLine("This iteration diverged");
            if (noInverse)
                Console.WriteLine("The hessian matrix had no inverse");

        }

        //Function f(x,y)
        static double f(Vector v)
        {
            //1a:
            return 4 * Math.Pow(v.x, 2) - 4 * v.x * v.y + 2 * Math.Pow(v.y, 2);

            //1b:
            //return -v.x * v.y * (v.x - 2) * (v.y + 3);

            //1c:
            //return Math.Pow(1 - v.y, 2) + 100 * Math.Pow(v.x - Math.Pow(v.y, 2), 2);
        }

        //Gradient of f(x,y)
        static Vector grad(Vector v)
        {
            //1a:
            return new Vector(8 * v.x - 4 * v.y, 4 * v.y - 4 * v.x);

            //1b:
            //return new Vector(-v.y * (v.y + 3) * (2 * v.x - 2), -v.x * (v.x - 2) * (2 * v.y + 3));

            //1c:
            //return new Vector(200 * (v.x - Math.Pow(v.y, 2)), -2 * (1 - v.y) - 400 * v.y * (v.x - Math.Pow(v.y, 2)));
        }

        static Matrix hessian(Vector v)
        {
            //1a:
            return new Matrix(8, -4, -4, 4);

            //1b:
            //return new Matrix(-2 * v.y * (v.y + 3), -(2 * v.x - 2) * (2 * v.y + 3), -(2 * v.x - 2) * (2 * v.y + 3), -2 * v.x * (v.x - 2));

            //1c:
            //return new Matrix(200, -400 * v.y, -400 * v.y, 2 - 400 * (v.x - 3 * Math.Pow(v.y, 2)));
        }

    }


    class Matrix
    {
        double a, b, c, d;

        public Matrix(double a, double b, double c, double d)
        {
            this.a = a;
            this.b = b;
            this.c = c;
            this.d = d;
        }

        //Return the determinant of the matrix.
        public double det()
        {
            return a * d - b * c;
        }

        //Use the formula for a 2x2 matrix to find the inverse.
        public Matrix inverse()
        {
            return (1/det()) * new Matrix(d, -b, -c, a);
        }

        //Define the operation of a scalar times a matrix.
        public static Matrix operator *(double s, Matrix m)
        {
            return new Matrix(s * m.a, s * m.b, s * m.c, s * m.d);
        }

        //Define the operation of a matrix times a vector.
        public static Vector operator *(Matrix m, Vector v)
        {
            return new Vector(m.a * v.x + m.b * v.y, m.c * v.x + m.d * v.y);
        }

        //Define how the matrix should look when printed to the console.
        public override String ToString()
        {
            return "┌ " + (a + "").PadRight(5) + " " + (b + "").PadLeft(5) + " ┐" + "\n" + "└ " + (c + "").PadRight(5) + " " + (d + "").PadLeft(5) + " ┘";
        }
    }

    class Vector
    {
        public double x;
        public double y;

        public Vector(double x, double y)
        {
            this.x = x;
            this.y = y;
        }

        public Vector round(int decimals)
        {
            return new Vector(Math.Round(x, decimals), Math.Round(y, decimals));
        }

        //Define the operation of adding two vectors.
        public static Vector operator +(Vector v1, Vector v2)
        {
            return new Vector(v1.x + v2.x, v1.y + v2.y);
        }

        //Define the operation of subtracting two vectors.
        public static Vector operator -(Vector v1, Vector v2)
        {
            return v1 + (-1 * v2);
        }

        //Define the operation of multiplyin a scalar times a vector.
        public static Vector operator *(double s, Vector v)
        {
            return new Vector(s * v.x, s * v.y);
        }

        //Define how the vector should look when printed to the console.
        public override String ToString()
        {
            return "<" + x + ", " + y + ">";
        }
    }
}
