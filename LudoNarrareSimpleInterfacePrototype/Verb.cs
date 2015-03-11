using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LudoNarrareSimpleInterfacePrototype
{
    class Verb
    {
        /* Variables */
        public string name;
        public List<string> variables;
        public List<string> output;
        public List<Condition> conditions;
        public List<Operator> operators;

        /* Functions */
        public Verb(string _name)
        {
            name = _name;
            variables = new List<string>();
            output = new List<string>();
            conditions = new List<Condition>();
            operators = new List<Operator>();
        }
    }
}
