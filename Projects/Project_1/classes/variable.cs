using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_1.classes
{
    /// <summary>
    ///     Simple object to increase readability.
    /// </summary>
    class variable
    {
        public double value { get; set; }
        public char   name  { get; set; }

        public variable(char _name, double _value)
        {
            name = _name;
            value = _value;
        }
    }
}
