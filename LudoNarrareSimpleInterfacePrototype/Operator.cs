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

        public void replaceWith(string replace, string with)
        {
            if (operatorSubject == replace)
                operatorSubject = with;
            if (relationship.other == replace)
                relationship.other = with;
        }

        public void copyTo(Operator op)
        {
            if (op != null)
            {
                op.name = name;
                op.operatorSubject = operatorSubject;
                op.addRemove = addRemove;
                op.type = type;
                op.attribute = new Attribute("");
                if (attribute != null)
                    attribute.copyTo(op.attribute);
                op.relationship = new Relationship("", "");
                if (relationship != null)
                    relationship.copyTo(op.relationship);
                op.obligation = obligation;
                op.goal = goal;
                op.goalVariable = goalVariable;
                op.behavior = new BehaviorReference("", 0);
                if (behavior != null)
                    behavior.copyTo(op.behavior);
            }
        }
    }
}
