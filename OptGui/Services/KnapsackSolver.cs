using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OptGui.Services
{
    public interface IProblemType1918
    {
        List<string> Names { get; set; }

        List<double> Weights { get; set; }

        List<double> Values { get; set; }

        int StageCount { get; }

        List<List<double>> DecisionLists { get; set; }

        List<List<double>> RecursiveReturnsLists { get; set; }

        List<List<double>> StageTable { get; }

        double OptimalPolicy { get; }

    }
    public class KnapsackSolver : IProblemType1918
    {
        private List<string> _names;
        private List<double> _weights;
        private List<double> _values;

        public List<string> Names { get => _names; set => _names = value; }

        public List<double> Weights { get => _weights; set => _weights = value; }

        public List<double> Values { get => _values; set => _values = value; }

        public int StageCount => this.Values.Count;

        public List<List<double>> DecisionLists { get; set; }

        public List<List<double>> RecursiveReturnsLists { get; set; }

        public List<List<double>> StageTable
        {
            get
            {
                List<List<double>> table = new List<List<double>>();

                for (int i = this.StageCount; i >= 0;  i--)
                {
                    table.Add(this.RecursiveReturnsLists[i]);
                    table.Add(this.DecisionLists[i]);
                }

                return table;
            }
        }

        public double OptimalPolicy { get; }

    }
}
