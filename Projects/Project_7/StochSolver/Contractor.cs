using System.Collections.Generic;

namespace StochSolver
{
    class Contractor
    {
        public string name = "";
        public List<double> probabilities = new List<double>();

        public bool selected = false;

        public Contractor() { }

        public Contractor(string name, List<double> probabilities)
        {
            this.name = name;
            this.probabilities = probabilities;
        }
    }
}
