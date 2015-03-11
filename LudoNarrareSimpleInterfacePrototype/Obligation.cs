using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LudoNarrareSimpleInterfacePrototype
{
    class Obligation
    {
        /* Variables */
        public string name;
        public string verb;
        public List<Condition> conditions;

        /* Functions */
        public Obligation(string _name, string _verb)
        {
            name = _name;
            verb = _verb;
            conditions = new List<Condition>();
        }
    }
}
