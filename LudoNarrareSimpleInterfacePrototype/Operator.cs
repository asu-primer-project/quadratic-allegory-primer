using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LudoNarrareSimpleInterfacePrototype
{
    class Operator
    {
        /* Variables */
        public string name;
        public string operatorSubject;
        public bool addRemove; // Add - true, Remove - false
        public int type; // Attribute - 0, Relationship - 1, Obligation - 2, Goal - 3, Behavior - 4
        public Attribute attribute;
        public Relationship relationship;
        public string obligation;
        public string goal;
        public string goalVariable;
        public BehaviorReference behavior;

        /* Functions */
        public Operator(string _name, string _operatorSubject, bool _addRemove, int _type, string _obligation, string _goal, string _goalVariable)
        {
            name = _name;
            operatorSubject = _operatorSubject;
            addRemove = _addRemove;
            type = _type;
            attribute = null;
            relationship = null;
            obligation = _obligation;
            goal = _goal;
            goalVariable = _goalVariable;
            behavior = null;
        }
    }
}
