using System;

namespace Steepest_Descent
{
    class Program
    {
        static void Main(string[] args)
        {
            //Solve starting from an initial starting vector and the specified number of iterations.
            solve(new Vector(1.1, 1.1), 10);
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

        static void solve(Vector initial, int iterations)
        {
            Vector x = initial;

            Console.WriteLine("┌─────┬──────────────────────┬──────────────┬──────────────────────┬─────────────┐");
            Console.WriteLine("│ k   │ X                    │ f(X)         │ grad(X)              | lambda      |");
            Console.WriteLine("├─────┼──────────────────────┼──────────────┼──────────────────────┼─────────────┤");

            bool diverge = false;

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
                Console.Write((grad(x).round(5) + "").PadRight(20) + " │ ");

                double l = findMinLambda(x);

                Console.WriteLine(("l: " + l).PadRight(11) + " │");

                x = x + (l * grad(x));
            }

            Console.WriteLine("└─────┴──────────────────────┴──────────────┴──────────────────────┴─────────────┘");
            if (diverge)
                Console.WriteLine("This iteration diverged");

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

            public Vector(double x, double y)
            {
                this.x = x;
                this.y = y;
            }

            public Vector round(int decimals)
            {
                return new Vector(Math.Round(x, decimals), Math.Round(y, decimals));
            }

            public static Vector operator +(Vector v1, Vector v2)
            {
                return new Vector(v1.x + v2.x, v1.y + v2.y);
            }

            public static Vector operator *(double s, Vector v)
            {
                return new Vector(s * v.x, s * v.y);
            }

            public override String ToString()
            {
                return "<" + x + ", " + y + ">";
            }
        }

    }
}
