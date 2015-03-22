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
        public Obligation obligation;
        public Goal goal;
        public Behavior behavior;

        /* Functions */
        public Operator(string _name, string _operatorSubject, bool _addRemove, int _type)
        {
            name = _name;
            operatorSubject = _operatorSubject;
            addRemove = _addRemove;
            type = _type;
            attribute = null;
            relationship = null;
            obligation = null;
            goal = null;
            behavior = null;
        }

        public void replaceWith(string replace, string with)
        {
            if (operatorSubject == replace)
                operatorSubject = with;
            if (relationship.other == replace)
                relationship.other = with;
            if (obligation != null)
            {
                for (int i = 0; i < obligation.arguments.Count; i++ )
                {
                    if (obligation.arguments[i] == replace)
                        obligation.arguments[i] = with;
                }
            }
            if (behavior != null)
            {
                for (int i = 0; i < obligation.arguments.Count; i++)
                {
                    if (obligation.arguments[i] == replace)
                        obligation.arguments[i] = with;
                }
            }           
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
                op.obligation = new Obligation("", "");
                if (obligation != null)
                    obligation.copyTo(op.obligation);
                op.goal = new Goal("", "", false, 0);
                if (goal != null)
                    goal.copyTo(op.goal);
                op.behavior = new Behavior("", "", 0);
                if (behavior != null)
                    behavior.copyTo(op.behavior);
            }
        }
    }
}
