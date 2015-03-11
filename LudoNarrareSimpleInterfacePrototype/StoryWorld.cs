using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LudoNarrareSimpleInterfacePrototype
{
    class StoryWorld
    {
        /* Variables */
        public string name;
        public string userEntity;
        public List<Entity> entities;
        public List<Condition> endConditions;
        public List<Verb> verbs;
        public List<Obligation> obligations;
        public List<Goal> goals;
        public List<Behavior> behaviors;

        /* Functions */
        public StoryWorld(string _name, string _userEntity)
        {
            name = _name;
            userEntity = _userEntity;
            entities = new List<Entity>();
            endConditions = new List<Condition>();
            verbs = new List<Verb>();
            obligations = new List<Obligation>();
            goals = new List<Goal>();
            behaviors = new List<Behavior>();
        }

        public string getPrintedWorldState()
        {
            string temp;
            temp = "Story World: " + name + "\n";
            temp += "{\n";

            temp += ("\tUser Entity: " + userEntity + "\n\n");

            if (entities.Count > 0)
            {
                temp += "\t--Entities--\n\n";
                for (int i = 0; i < entities.Count; i++)
                {
                    temp += ("\t" + entities[i].name + "\n");
                    temp += "\t{\n";
                    
                    if (entities[i].attributes.Count > 0)
                    {
                        temp += "\t\t--Attributes--\n";
                        for (int j = 0; j < entities[i].attributes.Count; j++)
                        {
                            temp += ("\t\t" + entities[i].attributes[j].name + "\n");
                        }
                        temp += "\n";
                    }

                    if (entities[i].relationships.Count > 0)
                    {
                        temp += "\t\t--Relationships--\n";
                        for (int j = 0; j < entities[i].relationships.Count; j++)
                        {
                            temp += ("\t\t" + entities[i].relationships[j].name + " | Object: " + entities[i].relationships[j].other + "\n");
                        }
                        temp += "\n";
                    }

                    if (entities[i].obligations.Count > 0)
                    {
                        temp += "\t\t--Obligations--\n";
                        for (int j = 0; j < entities[i].obligations.Count; j++)
                        {
                            temp += ("\t\t" + entities[i].obligations[j] + "\n");
                        }
                        temp += "\n";
                    }

                    if (entities[i].goals.Count > 0)
                    {
                        temp += "\t\t--Goals--\n";
                        for (int j = 0; j < entities[i].goals.Count; j++)
                        {
                            temp += ("\t\t" + entities[i].goals[j] + "\n");
                        }
                        temp += "\n";
                    }

                    if (entities[i].behaviors.Count > 0)
                    {
                        temp += "\t\t--Behaviors--\n";
                        for (int j = 0; j < entities[i].behaviors.Count; j++)
                        {
                            temp += ("\t\t" + entities[i].behaviors[j].name + " | Chance: " + entities[i].behaviors[j].chance + "\n");
                        }
                    }

                    temp += "\t}\n\n";
                }           
            }

            if (endConditions.Count > 0)
            {
                temp += "\t--End Conditions--\n\n";
                for (int i = 0; i < endConditions.Count; i++)
                {
                    temp += ("\t" + endConditions[i].name + "\n");
                    temp += ("\t(Subject: " + endConditions[i].conditionSubject);
                    if (endConditions[i].negate)
                        temp += (" | Negate: True ");
                    else
                        temp += (" | Negate: False ");

                    if (endConditions[i].type == 0)
                        temp += ("| Attribute: " + endConditions[i].attribute.name + ")\n");
                    else if (endConditions[i].type == 1)
                        temp += ("| Relationship: " + endConditions[i].relationship.name + " | Object: " + endConditions[i].relationship.other + ")\n");
                }
            }

            if (verbs.Count > 0)
            {
                temp += "\n\t--Verbs--\n\n";
                for (int i = 0; i < verbs.Count; i++)
                {
                    temp += ("\t" + verbs[i].name + "\n");
                    temp += "\t{\n";
                    
                    if (verbs[i].variables.Count > 0)
                    {
                        temp += "\t\t--Variables--\n";
                        for (int j = 0; j < verbs[i].variables.Count; j++)
                        {
                            temp += ("\t\t" + verbs[i].variables[j] + "\n");
                        }
                        temp += "\n";
                    }

                    if (verbs[i].output.Count > 0)
                    {
                        temp += "\t\t--Output--\n\t\t";
                        for (int j = 0; j < verbs[i].output.Count; j++)
                        {
                            temp += (verbs[i].output[j] + " ");
                        }
                        temp += "\n\n";
                    }

                    if (verbs[i].conditions.Count > 0)
                    {
                        temp += "\t\t--Conditions--\n";
                        for (int j = 0; j < verbs[i].conditions.Count; j++)
                        {
                            temp += ("\t\t" + verbs[i].conditions[j].name + "\n");
                            temp += ("\t\t(Subject: " + verbs[i].conditions[j].conditionSubject);
                            if (verbs[i].conditions[j].negate)
                                temp += (" | Negate: True ");
                            else
                                temp += (" | Negate: False ");

                            if (verbs[i].conditions[j].type == 0)
                                temp += ("| Attribute: " + verbs[i].conditions[j].attribute.name + ")\n");
                            else if (verbs[i].conditions[j].type == 1)
                                temp += ("| Relationship: " + verbs[i].conditions[j].relationship.name + " | Object: " + verbs[i].conditions[j].relationship.other + ")\n");
                        }
                        temp += "\n";
                    }

                    if (verbs[i].operators.Count > 0)
                    {
                        temp += "\t\t--Operators--\n";
                        for (int j = 0; j < verbs[i].operators.Count; j++)
                        {
                            temp += ("\t\t" + verbs[i].operators[j].name + "\n");
                            temp += ("\t\t(Subject: " + verbs[i].operators[j].operatorSubject);
                            if (verbs[i].operators[j].addRemove)
                                temp += (" | Type: Add ");
                            else
                                temp += (" | Type: Remove ");

                            if (verbs[i].operators[j].type == 0)
                                temp += ("| Attribute: " + verbs[i].operators[j].attribute.name + ")\n");
                            else if (verbs[i].operators[j].type == 1)
                                temp += ("| Relationship: " + verbs[i].operators[j].relationship.name + "|  Object: " + verbs[i].operators[j].relationship.other + ")\n");
                            else if (verbs[i].operators[j].type == 2)
                                temp += ("| Obligation: " + verbs[i].operators[j].obligation + ")\n");
                            else if (verbs[i].operators[j].type == 3)
                                temp += ("| Goal: " + verbs[i].operators[j].goal + " | Goal Variable: " + verbs[i].operators[j].goalVariable + ")\n");
                            else if (verbs[i].operators[j].type == 4)
                                temp += ("| Behavior: " + verbs[i].operators[j].behavior.name + " | Behavior Chance: " + verbs[i].operators[j].behavior.chance + ")\n");
                        }
                    }

                    temp += "\t}\n\n";
                }
            }

            if (obligations.Count > 0)
            {
                temp += "\t--Obligations--\n\n";
                for (int i = 0; i < obligations.Count; i++)
                {
                    temp += ("\t" + obligations[i].name + "\n");
                    temp += "\t{\n";
                    temp += ("\t\t Verb:" + obligations[i].verb + "\n");
                    if (obligations[i].conditions.Count > 0)
                    {
                        temp += "\n\t\t--Conditions--\n";
                        for (int j = 0; j < obligations[i].conditions.Count; j++)
                        {
                            temp += ("\t\t" + obligations[i].conditions[j].name + "\n");
                            temp += ("\t\t(Subject: " + obligations[i].conditions[j].conditionSubject);
                            if (obligations[i].conditions[j].negate)
                                temp += (" | Negate: True ");
                            else
                                temp += (" | Negate: False ");

                            if (obligations[i].conditions[j].type == 0)
                                temp += ("| Attribute: " + obligations[i].conditions[j].attribute.name + ")\n");
                            else if (obligations[i].conditions[j].type == 1)
                                temp += ("| Relationship: " + obligations[i].conditions[j].relationship.name + " | Object: " + obligations[i].conditions[j].relationship.other + ")\n");
                        }
                    }

                    temp += "\t}\n\n";
                }
            }

            if (goals.Count > 0)
            {
                temp += "\t--Goals--\n\n";
                for (int i = 0; i < goals.Count; i++)
                {
                    temp += ("\t" + goals[i].name + "\n");
                    temp += ("\t--Goal Operator--\n");
                    temp += ("\t" + goals[i].goalOperator.name + "\n");
                    temp += ("\t(Subject: " + goals[i].goalOperator.operatorSubject);
                    if (goals[i].goalOperator.addRemove)
                        temp += (" | Type: Add ");
                    else
                        temp += (" | Type: Remove ");

                    if (goals[i].goalOperator.type == 0)
                        temp += ("| Attribute: " + goals[i].goalOperator.attribute.name + ")\n");
                    else if (goals[i].goalOperator.type == 1)
                        temp += ("| Relationship: " + goals[i].goalOperator.relationship.name + " | Object: " + goals[i].goalOperator.relationship.other + ")\n");
                    else if (goals[i].goalOperator.type == 2)
                        temp += ("| Obligation: " + goals[i].goalOperator.obligation + ")\n");
                    else if (goals[i].goalOperator.type == 3)
                        temp += ("| Goal: " + goals[i].goalOperator.goal + " | Goal Variable: " + goals[i].goalOperator.goalVariable + ")\n");
                    else if (goals[i].goalOperator.type == 4)
                        temp += ("| Behavior: " + goals[i].goalOperator.behavior.name + " | Behavior Chance: " + goals[i].goalOperator.behavior.chance + ")\n");
                }
            }

            if (behaviors.Count > 0)
            {
                temp += "\n\t--Behaviors--\n\n";
                for (int i = 0; i < behaviors.Count; i++)
                {
                    temp += ("\t" + behaviors[i].name + " | Verb: " + behaviors[i].verb + "\n");
                }
            }

            temp += "}\n";
            return temp;
        }

        public override string ToString()
        {
            return name;
        }
    }
}
