using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OptGui.Services
{
    public interface IDecisionRow
    {
        string Header { get; set; }

        string CellVal { get; set; }
    }
    public class DecisionRow : IDecisionRow
    {
        public string Header { get; set; }

        public string CellVal { get; set; }
    }
}
