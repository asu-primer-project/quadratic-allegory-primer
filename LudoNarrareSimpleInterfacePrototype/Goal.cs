using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LudoNarrareSimpleInterfacePrototype
{
    class Goal
    {
        /* Variables */
        public string name;
        public Operator goalOperator;

        /* Functions */
        public Goal(string _name)
        {
            name = _name;
            goalOperator = null;
        }
    }
}
