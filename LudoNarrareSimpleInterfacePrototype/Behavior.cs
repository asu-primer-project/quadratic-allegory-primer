using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LudoNarrareSimpleInterfacePrototype
{
    class Behavior
    {
        /* Variables */
        public string name;
        public string verb;

        /* Functions */
        public Behavior(string _name, string _verb)
        {
            name = _name;
            verb = _verb;
        }
    }
}
