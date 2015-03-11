using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LudoNarrareSimpleInterfacePrototype
{
    class Condition
    {
        /* Variables */
        public string name;
        public string conditionSubject;
        public bool negate;
        public int type; // Attribute - 0, Relationship - 1
        public Attribute attribute;
        public Relationship relationship;

        /* Functions */
        public Condition(string _name, string _conditionSubject, bool _negate, int _type)
        {
            name = _name;
            conditionSubject = _conditionSubject;
            negate = _negate;
            type = _type;
            attribute = null;
            relationship = null;
        }
    }
}
