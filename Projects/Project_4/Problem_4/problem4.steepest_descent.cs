using System;

namespace Steepest_Descent
{
    class Program
    {
        static void Main(string[] args)
        {
            //Solve starting from an initial starting vector and the specified number of iterations.
            solve(new Vector(0, 0, 0), 5);
        }

        static double sqr(double x)
        {
            return Math.Pow(x, 2);
        }

        //Function f(x,y)
        static double f(Vector v)
        {
            double x = v.x, y = v.y, z = v.z;

            return (sqr(y) * sqr(z) + sqr(z) * sqr(x) + sqr(x) * sqr(z)) + (sqr(x) + sqr(y) + sqr(z)) - 2 * x * y * z - 2 * (z + x - y) - 2 * (y * z + z * x - x * y);
        }

        //Gradient of f(x,y)
        static Vector grad(Vector v)
        {
            double x = v.x, y = v.y, z = v.z;

            return new Vector(
                    2 * x * sqr(y) + 2 * x * sqr(z) + 2 * x - 2 * y * z + 2 * y - 2 * z - 2,
                    2 * y * sqr(x) + 2 * y * sqr(z) + 2 * x - 2 * x * z + 2 * y - 2 * z + 2,
                    2 * z * sqr(x) - 2 * x * y - 2 * x + 2 * sqr(y) * z - 2 * y + 2 * z - 2
                );
        }

        static void solve(Vector initial, int iterations)
        {
            Vector x = initial;

            bool diverge = false;

            for (int i = 0; i <= iterations; i++)
            {
                //If the function value is large, assume it is diverging.
                if (Math.Abs(f(x)) > 1000000)
                {
                    diverge = true;
                    break;
                }

                Console.Write((i + "").PadRight(3) + " & ");
                Console.Write((x.round(5) + "").PadRight(20) + " & ");
                Console.Write((Math.Round(f(x), 8) + "").PadRight(12) + " & ");
                Console.Write((grad(x).round(5) + "").PadRight(20) + " & ");

                double l = findMinLambda(x);

                Console.WriteLine(("l: " + l).PadRight(11) + " \\\\");

                x = x + (l * grad(x));
            }

            if (diverge)
                Console.WriteLine("This iteration diverged");

            Console.ReadLine();

        }

        //Find the value of lambda that will minimize the function.
        static double findMinLambda(Vector v)
        {
            //Calculate the gradient at v.
            Vector gradient = grad(v);

            double bestLambda = 0;
            double bestValue = Double.MaxValue;

            //Loop from -5 to 5 incrementing by 0.001.
            for (double i = -5; i <= 5; i = Math.Round(i + 0.001, 6))
            {
                //Use an iterative method to find a minimum with a max of 1000 iterations.
                double l = iterateToMinimum(v, i, 100);

                //Is the lambda value is NaN then the iteration diverged.
                if (Double.IsNaN(l))
                    continue;

                if (f(v + (l * gradient)) < bestValue)
                {
                    bestLambda = l;
                    bestValue = f(v + (l * gradient));
                }
            }

            return Math.Round(bestLambda, 6);
        }

        static double iterateToMinimum(Vector v, double l, int iterationsRemaining)
        {
            //If we are out of iterations, assume we are diverging.
            if (iterationsRemaining <= 0)
                return Double.NaN;

            //Calculate the gradient at v.
            Vector gradient = grad(v);

            double fVal = f(v + (l * gradient));
            double delta = 0.00001;
            if (f(v + ((l + delta) * gradient)) < fVal)
                return iterateToMinimum(v, l + delta, iterationsRemaining - 1);
            else if (f(v + ((l - delta) * gradient)) < fVal)
                return iterateToMinimum(v, l - delta, iterationsRemaining - 1);
            else
                return l;
        }

        class Vector
        {
            public double x;
            public double y;
            public double z;

            public Vector(double x, double y, double z)
            {
                this.x = x;
                this.y = y;
                this.z = z;
            }

            public Vector round(int decimals)
            {
                return new Vector(Math.Round(x, decimals), Math.Round(y, decimals), Math.Round(z, decimals));
            }

            public static Vector operator +(Vector v1, Vector v2)
            {
                return new Vector(v1.x + v2.x, v1.y + v2.y, v1.z + v2.z);
            }

            public static Vector operator *(double s, Vector v)
            {
                return new Vector(s * v.x, s * v.y, s * v.z);
            }

            public override String ToString()
            {
                return "<" + x + ", " + y + ", " + z + ">";
            }
        }

    }
}

// part C
//│ 0   │ <0, 0, 0>                    │ 0            │ <-2, 2, -2>                   │ l: -0.30108 │
//│ 1   │ <0.60216, -0.60216, 0.60216> │ -2.41925189  │ <-1.60576, -0.80288, 0.80288> │ l: -0.2472  │
//│ 2   │ <0.9991, -0.40369, 0.40369>  │ -2.89866381  │ <-0.63934, 0.6393, -0.6393>   │ l: -0.12236 │
//│ 3   │ <1.07733, -0.48191, 0.48191> │ -2.97600187  │ <-0.3077, -0.15385, 0.15385>  │ l: -0.25493 │
//│ 4   │ <1.15578, -0.44269, 0.44269> │ -2.99421551  │ <-0.16124, 0.16124, -0.16124> │ l: -0.1096  │
//│ 5   │ <1.17345, -0.46037, 0.46037> │ -2.99851808  │ <-0.07591, -0.03796, 0.03796> │ l: -0.25413 │