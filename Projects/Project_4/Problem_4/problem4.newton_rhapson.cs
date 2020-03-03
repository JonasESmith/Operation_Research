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
                // If the function value is large, assume it is diverging.
                if (Math.Abs(f(initVector)) > 2000000000000)
                {
                    diverge = true;
                    break;
                }

                // the bellow formatting allows me to copy and paste this easily into a latex file! :smile:
                string print = string.Format(" {0, -3} & ${1, 20}$ & ${2,12}$ & ${3,23}$ \\\\", i, VectorString(initVector), Math.Round(f(initVector), 8), VectorString(round(initVector, 5)));

                Console.WriteLine(print);

                Matrix<double> A = hessian(initVector);

                if (A.Determinant() == 0)
                {
                    noInverse = true;
                    break;
                }

                initVector = initVector - (A.Inverse() * grad(initVector));
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
                    returnValue += string.Format("{0} ,", Math.Round(vector[i], 6));
                else
                    returnValue += string.Format("{0} >", Math.Round(vector[i], 6));

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
                    2*x*sqr(y) + 2*x*sqr(z) + 2*x - 2*y*z + 2*y - 2*z - 2,
                    2*y*sqr(x) + 2*y*sqr(z) + 2*x - 2*x*z + 2*y - 2*z + 2,
                    2*z*sqr(x) - 2*x*y - 2*x + 2*sqr(y)*z - 2*y + 2*z - 2
            });
        }

        static Matrix<double> hessian(Vector<double> v)
        {
            double x = v[0], y = v[1], z = v[2];

            return DenseMatrix.OfArray(new double[,]
             {
                    {
                        2*x*sqr(y) + 2*sqr(z) + 2,
                        4*x*y - 2*z + 2,
                        4*x*z - 2*y - 2
                    },
                     {
                        4*x*y - 2*z + 2,
                        2*sqr(x) + 2*sqr(z) + 2,
                        -2*x + 4*y*z - 2
                    },
                    {
                        4*x*z - 2*y - 2,
                        -2*x + 4*y*z - 2,
                        2*sqr(x) + 2*sqr(y) + 2
                    },
             });
        }

        // returning the rounded values of the Vector<double>
        public static Vector<double> round(Vector<double> v, int decimals) => DenseVector.OfArray(new double[] { Math.Round(v[0], decimals), Math.Round(v[1], decimals), Math.Round(v[2], decimals) });

        // this is silly but I like the syntax of this better than math.pow()
        static double sqr(double x) => Math.Pow(x, 2);
    }
}


/// ┌─────┬──────────────────────┬──────────────┬─────────────────────────┐
/// │ k   │ X                    │ f(X)         │ grad(X)                 |
/// ├─────┼──────────────────────┼──────────────┼─────────────────────────┤
/// | 0   |         < 2 ,-1 ,1 > |            5 |            < 2 ,-1 ,1 > |
/// | 1   |        < -3 ,-5 ,0 > |           60 |           < -3 ,-5 ,0 > |
/// | 2   | < -2.51011 ,-1.459683 ,-0.02972 > |  17.91526554 | < -2.51011 ,-1.45968 ,-0.02972 > |
/// | 3   | < -2.119119 ,-0.343131 ,-0.140689 > |   9.60769874 | < -2.11912 ,-0.34313 ,-0.14069 > |
/// | 4   | < -2.397976 ,0.502059 ,-0.317289 > |   9.34589421 | < -2.39798 ,0.50206 ,-0.31729 > |
/// | 5   | < 7.600513 ,-1.21627 ,-0.06305 > |  23.35958861 | < 7.60051 ,-1.21627 ,-0.06305 > |
/// | 6   | < 16.450713 ,3.742723 ,0.77344 > | 587.09322836 | < 16.45071 ,3.74272 ,0.77344 > |
/// | 7   | < 17.332004 ,-0.393847 ,0.056996 > | 252.13824625 | < 17.332 ,-0.39385 ,0.057 > |
/// └─────┴──────────────────────┴──────────────┴─────────────────────────┘