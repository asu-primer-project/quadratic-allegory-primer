using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LudoNarrareEditPrototype
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

        public void setName(string _name)
        {
            name = _name;
        }

        public Attribute copy(Attribute attribute)
        {
            attribute.setName(name);
            return attribute;
        }

        public override string ToString()
        {
            return name;
        }
    }
}
