using System;

namespace Steepest_Descent
{
    class Program
    {
        static void Main(string[] args)
        {
            //Solve starting from an initial starting vector and the specified number of iterations.
            solve(new Vector(1, 1), 2);
        }

        //Function f(x,y)
        static double f(Vector v)
        {
            //1a:
            //return 4 * Math.Pow(v.x, 2) - 4 * v.x * v.y + 2 * Math.Pow(v.y, 2);

            //1b:
            return -v.x * v.y * (v.x - 2) * (v.y + 3);
        }

        //Gradient of f(x,y)
        static Vector grad(Vector v)
        {
            //1a:
            //return new Vector(8 * v.x - 4 * v.y, 4 * v.y - 4 * v.x);

            //1b:
            return new Vector(v.y * (2 * v.y + 6) * (1 - v.x), -v.x * (v.x-2) * (2 * v.y + 3));
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
            //Keep track of our current best function value and the associated lambda value.
            double min = double.MaxValue;
            double best = 0;

            //Calculate the gradient at v.
            Vector gradient = grad(v);

            //If the gradient is 0, choose lambda = 0
            if (gradient.x == 0 && gradient.y == 0)
                return 0;

            //Loop from -50 to 50 incrementing by 0.002
            for (double l = -50; l <= 50; l = Math.Round(l + 0.002, 4))
            {
                if (f(v + (l * gradient)) < min) {
                    min = f(v + (l * gradient));
                    best = l;
                }
            }

            return best;
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
