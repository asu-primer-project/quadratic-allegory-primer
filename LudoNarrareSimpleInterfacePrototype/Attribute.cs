using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LudoNarrareSimpleInterfacePrototype
{
    class Attribute
    {
        /* Variables */
        public string name;

        /* Functions */
        public Attribute(string _name)
        {
            name = _name;
        }

        public void copyTo(Attribute a)
        {
            if (a != null)
                a.name = name;
        }
    }
}
