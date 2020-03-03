using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;
using System;

namespace Newton_Raphson
{
    class Program
    {
        static void Main()
        {
            // initial values, and number of iterations.
            solve(DenseVector.OfArray(new double[] { 2, -1, 1 }), 7);
        }


        static void solve(Vector<double> initial, int iterations)
        {
            Vector<double> initVector = initial;

            Console.WriteLine("┌─────┬──────────────────────┬──────────────┬─────────────────────────┐");
            Console.WriteLine("│ k   │ X                    │ f(X)         │ grad(X)                 |");
            Console.WriteLine("├─────┼──────────────────────┼──────────────┼─────────────────────────┤");

            bool diverge = false;
            bool noInverse = false;

            for (int i = 0; i <= iterations; i++)
            {
                //If the function value is large, assume it is diverging.
                if (Math.Abs(f(initVector)) > 2000000000000)
                {
                    diverge = true;
                    break;
                }

                string print = string.Format("| {0, -3} | {1, 20} | {2,12} | {3,23} |", i, VectorString(initVector), Math.Round(f(initVector), 8), VectorString(round(initVector, 5)));

                Console.WriteLine(print);

                Matrix<double> A = hessian(initVector);

                if (A.Determinant() == 0)
                {
                    noInverse = true;
                    break;
                }

                Vector<double> gradientVector = grad(initVector);

                initVector = initVector - (MatrixTimesVector(A, gradientVector));
            }

            Console.WriteLine("└─────┴──────────────────────┴──────────────┴─────────────────────────┘");
            if (diverge)
                Console.WriteLine("This iteration diverged");
            if (noInverse)
                Console.WriteLine("The hessian matrix had no inverse");

            Console.ReadLine();

        }

        static string VectorString(Vector<double> vector)
        {
            string returnValue = "< ";

            for (int i = 0; i < vector.Count; i++)

                if (i < vector.Count - 1)
                {
                    returnValue += string.Format("{0} ,", vector[i]);
                }
                else
                {
                    returnValue += string.Format("{0} >", vector[i]);
                }

            return returnValue;
        }

        //Function f(x,y,z)
        static double f(Vector<double> v)
        {
            double x = v[0], y = v[1], z = v[2];

            return (sqr(y) * sqr(z) + sqr(z) * sqr(x) + sqr(x) * sqr(z)) + (sqr(x) + sqr(y) + sqr(z)) - 2 * x * y * z - 2 * (z + x - y) - 2 * (y * z + z * x - x * y);
        }


        //Gradient of f(x,y,z)
        static Vector<double> grad(Vector<double> v)
        {
            double x = v[0], y = v[1], z = v[2];

            return DenseVector.OfArray(new double[] {
                    2 * x * sqr(y) + 2 * x * sqr(z) + 2 * x - 2 * y * z + 2 * y - 2 * z - 2,
                    2 * y * sqr(x) + 2 * y * sqr(z) + 2 * x - 2 * x * z + 2 * y - 2 * z + 2,
                    2 * z * sqr(x) - 2 * x * y - 2 * x + 2 * sqr(y) * z - 2 * y + 2 * z - 2
            });
        }

        static Matrix<double> hessian(Vector<double> v)
        {
            double x = v[0], y = v[1], z = v[2];

            return DenseMatrix.OfArray(new double[,]
             {
                    {
                        2 * x * sqr(y) + 2 * sqr(z) + 2,
                        4 * x * y - 2 * x + 2,
                        4 * x * z - 2*y - 2
                    },
                     {
                        4 * x * y - 2 * x + 2,
                        2 * sqr(x) + 2 * sqr(z) + 2,
                        -2 * x + 4 * y * z - 2
                    },
                    {
                        4 * x * z - 2*y - 2,
                        -2 * x + 4 * y * z - 2,
                        2 * sqr(x) + 2 * sqr(y) + 2
                    },
             });
        }

        // returning the rounded values of the Vector<double>
        public static Vector<double> round(Vector<double> v, int decimals) => DenseVector.OfArray(new double[] { Math.Round(v[0], decimals), Math.Round(v[1], decimals), Math.Round(v[2], decimals) });

        // multiplies the matrix A by the Vector v.
        static Vector<double> MatrixTimesVector(Matrix<double> A, Vector<double> v) => A * v;

        // this is silly but I like the syntax of this better than math.pow()
        static double sqr(double x) => Math.Pow(x, 2);
    }
}
