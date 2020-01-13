/// PROGRAMMER : Jonas Smith
/// Purpose    : Get inputs from a user and solve a given linear equation.
/// Resources  : https://numerics.mathdotnet.com/LinearEquations.html - resource to make this more general case with Math.net
///            : 
///            


using System;
using Project_1.classes;
using System.Collections.Generic;
using MathNet.Numerics.LinearAlgebra;
using System.Linq;

namespace Project_01
{
    class ProjectOne
    {
        static void Main()
        {
            bool user_is_iterating = true;
            List<char> variable_names = new List<char>() { 'a', 'b', 'c', 'd', 'r', 's' };
            List<char> solution_names = new List<char>() { 'x', 'y' };

            PrintHeader();
            PrintPurpose();
            PrintDivider();

            while (user_is_iterating)
            {
                List<variable> user_inputs = GetUserInput(variable_names);
                Vector<double> solution    = CalculateSolutions(user_inputs);

                PrintSolutions(solution, solution_names);
                UserIterations(ref user_is_iterating);
            }
        }


        static void PrintHeader()
        {
            List<string> headers = new List<string>() { "Math 371", "Spring 2020", "Lienar System Solver", "Jonas Smith" };
            int size = 32;
            string buffer = "";

            for(int i = 0; i < headers.Count; i++) {

                buffer = GetBuffer(headers[i], size);
                Console.WriteLine("{0}{1}", buffer, headers[i]);
            }

        }

        static string GetBuffer(string value, int length)
        {
            int buffer_length = (length - Convert.ToInt32(value.Length)) / 2;

            string buffer = "";

            for(int i = 0; i < buffer_length; i++)
                buffer += " ";

            return buffer;
        }

        static void PrintPurpose()
        {
            Console.WriteLine();
            Console.WriteLine("     Take inputs from the user ");
            Console.WriteLine("{a,b,c,d,r,s} and calculate x");
            Console.WriteLine("and y from the system of linear");
            Console.WriteLine("equations");
            Console.WriteLine();
            Console.WriteLine("similar to: ax + by = r");
            Console.WriteLine("            cx + dy = s");

        }

        static void PrintDivider()
        {
            Console.WriteLine("________________________________\n");
        }

        static List<variable> GetUserInput(List<char> variables)
        {
            List<variable> user_inputs = new List<variable>();

            for(int i = 0; i < variables.Count; i++)
            {
                user_inputs.Add(GetVariableInput(variables[i], i));
            }

            return user_inputs;
        }

        static variable GetVariableInput(char var_name, int index)
        {
            bool user_input_wrong = true;

            string message = "   Enter the value for";

            string buffer = "";

            for(int i = 0; i < message.Length; i++) {
                buffer += " ";
            }

            double input = 0.0;

            while(user_input_wrong)
            {
                string prompt = string.Format("{0} {1} = ", buffer, var_name);

                if (index == 0) {
                    prompt = string.Format("{0} {1} = ", message, var_name);
                }

                Console.Write(prompt);

                try 
                {
                    string _val = "";

                    ConsoleKeyInfo key;

                    do
                    {
                        key = Console.ReadKey(true);
                        if (key.Key != ConsoleKey.Backspace)
                        {
                            double val = 0;
                            bool _x = double.TryParse(key.KeyChar.ToString(), out val);
                            if (_x) {
                                _val += key.KeyChar;
                                Console.Write(key.KeyChar);
                            }
                            
                            if(key.Key == ConsoleKey.OemPeriod){
                                _val += ".";
                                Console.Write(key.KeyChar);
                            }
                        }
                        else
                        {
                            if (key.Key == ConsoleKey.Backspace && _val.Length > 0)
                            {
                                _val = _val.Substring(0, (_val.Length - 1));
                                Console.Write("\b \b");
                            }
                        }
                    } while (key.Key != ConsoleKey.Enter);


                    input = Convert.ToDouble( _val);

                    user_input_wrong = false;
                }
                catch
                {
                    index++;
                }

                Console.WriteLine();
            }


            return new variable(var_name, input);
        }

        static Vector<double> CalculateSolutions(List<variable> user_input)
        {
            // Using some linear algebra we can user Ax = b

            // Build matrix A
            int n = 2;
            double[,] matrix_A = new double[n,n];

            for(int i = 0; i <n; i++) {
                for(int k = 0; k < n; k++)
                {
                    matrix_A[i, k] = user_input[i + k].value;
                }
            }

            var A = Matrix<double>.Build.DenseOfArray(matrix_A);

            // Build the coefficient vector b
            double[] matrix_B = new double[n];
            int index = Convert.ToInt32( Math.Pow(n, 2) );

            for (int i = 0; i < n; i++) {
                matrix_B[i] = user_input[index].value;

                index++;
            }

            var b = Vector<double>.Build.Dense(matrix_B);

            // Solve!
            Vector<double> x = A.Solve(b);

            return x;
        }

        static void PrintSolutions(Vector<double> solutions, List<char> solution_names)
        {
            Console.WriteLine("\n           Solutions           ");
            Console.WriteLine("________________________________");

            for (int i = 0; i < solutions.Count; i++)
            {
                string output = "";
                // the solutions is negative so we move the margin over one characters
                if(solutions[i] < 0)
                    output = string.Format("{0} ={1}", solution_names[i], solutions[i].ToString("N5"));
                else
                    output = string.Format("{0} = {1}", solution_names[i], solutions[i].ToString("N5"));

                string buffer = GetBuffer(output, 32);

                Console.WriteLine("{0}{1}",buffer, output);
            }

            Console.WriteLine();
        }

        static void UserIterations(ref bool user_iteration)
        {
            bool correct_input = true;

            while (correct_input)
            {

                Console.WriteLine("   Would you like to continue?");
                Console.Write("        [y]es or [n]o ) : ");

                string user_input = Console.ReadLine().ToLower();

                if (user_input == "y")
                {
                    correct_input = false;
                    Console.Clear();
                    Iterate();
                }
                else if (user_input == "n")
                {
                    user_iteration = false;
                    correct_input = false;
                }
                else
                {
                    Console.Write(new String(' ', Console.BufferWidth));
                }
            }
        }

        static void Iterate()
        {
            PrintHeader();
            PrintPurpose();
            PrintDivider();
        }
    }
}
