using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LudoNarrareEditPrototype
{
    class Verb
    {
        /* Variables */
        public string name;

        /* Functions */
        public Verb(string _name)
        {
            name = _name;
        }

        public void setName(string _name)
        {
            name = _name;
        }

        public Verb copy(Verb verb)
        {
            verb.setName(name);
            return verb;
        }

        public override string ToString()
        {
            return name;
        }
    }
}
