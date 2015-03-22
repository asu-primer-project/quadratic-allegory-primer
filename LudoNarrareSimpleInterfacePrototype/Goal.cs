using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LudoNarrareSimpleInterfacePrototype
{
    class Goal
    {
        /* Variables */
        public string name;
        public string operatorSubject;
        public bool addRemove; // Add - true, Remove - false
        public int type; // Attribute - 0, Relationship - 1, Obligation - 2, Behavior - 4
        public Attribute attribute;
        public Relationship relationship;
        public Obligation obligation;
        public Behavior behavior;

        /* Functions */
        public Goal(string _name, string _operatorSubject, bool _addRemove, int _type)
        {
            name = _name;
            operatorSubject = _operatorSubject;
            addRemove = _addRemove;
            type = _type;
            attribute = null;
            relationship = null;
            obligation = null;
            behavior = null;
        }

        public bool matchWith(Operator op)
        {
            if (op.operatorSubject == operatorSubject && op.addRemove == addRemove && op.type == type)
            {
                switch(type)
                {
                    case 0:
                        if (op.attribute.name == attribute.name)
                            return true;
                        else
                            return false;
                    case 1:
                        if (op.relationship.name == relationship.name)
                        {
                            if (op.relationship.other == relationship.other)
                                return true;
                            else
                                return false;
                        }
                        else
                            return false;
                    case 2:
                        if (op.obligation.name == obligation.name)
                            return true;
                        else
                            return false;
                    case 4:
                        if (op.behavior.name == behavior.name)
                            return true;
                        else
                            return false;
                    default:
                        return false;
                }
            }
            else
                return false;
        }

        public void replaceWith(string replace, string with)
        {
            if (operatorSubject == replace)
                operatorSubject = with;
            if (relationship.other == replace)
                relationship.other = with;
            if (obligation != null)
            {
                for (int i = 0; i < obligation.arguments.Count; i++)
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

        public void copyTo(Goal g)
        {
            if (g != null)
            {
                g.name = name;
                g.operatorSubject = operatorSubject;
                g.addRemove = addRemove;
                g.type = type;
                g.attribute = new Attribute("");
                if (attribute != null)
                    attribute.copyTo(g.attribute);
                g.relationship = new Relationship("", "");
                if (relationship != null)
                    relationship.copyTo(g.relationship);
                g.obligation = new Obligation("", "");
                if (obligation != null)
                    obligation.copyTo(g.obligation);
                g.behavior = new Behavior("", "", 0);
                if (behavior != null)
                    behavior.copyTo(g.behavior);
            }           
        }
    }
}
