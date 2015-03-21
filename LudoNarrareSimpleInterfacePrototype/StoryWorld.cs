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
        public List<Goal> goals;

        /* Functions */
        public StoryWorld(string _name, string _userEntity)
        {
            name = _name;
            userEntity = _userEntity;
            entities = new List<Entity>();
            endConditions = new List<Condition>();
            verbs = new List<Verb>();

            Verb vTemp = new Verb("Wait");
            vTemp.input.Add("Wait.");
            verbs.Add(vTemp);

            goals = new List<Goal>();
        }

        public string getPrintedWorldState()
        {
            string temp;
            temp = "Story World: " + name + "\n";
            temp += "{\n";

            temp += ("\tUser Entity: " + userEntity + "\n");

            if (entities.Count > 0)
            {
                temp += "\n\t--Entities--\n";
                for (int i = 0; i < entities.Count; i++)
                {
                    temp += ("\t" + entities[i].name + "\n");
                    temp += "\t{\n";
                    
                    if (entities[i].attributes.Count > 0)
                    {
                        for (int j = 0; j < entities[i].attributes.Count; j++)
                            temp += ("\t\tAttribute: " + entities[i].attributes[j].name + "\n");
                    }

                    if (entities[i].relationships.Count > 0)
                    {
                        for (int j = 0; j < entities[i].relationships.Count; j++)
                            temp += ("\t\tRelationship: " + entities[i].relationships[j].name + " | Object: " + entities[i].relationships[j].other + "\n");
                    }

                    if (entities[i].obligations.Count > 0)
                    {
                        for (int j = 0; j < entities[i].obligations.Count; j++)
                            temp += ("\t\tObligation: " + entities[i].obligations[j] + "\n");
                    }

                    if (entities[i].goals.Count > 0)
                    {
                        for (int j = 0; j < entities[i].goals.Count; j++)
                            temp += ("\t\tGoal: " + entities[i].goals[j] + "\n");
                    }

                    if (entities[i].behaviors.Count > 0)
                    {
                        for (int j = 0; j < entities[i].behaviors.Count; j++)
                            temp += ("\t\tBehavior: " + entities[i].behaviors[j].name + " | Chance: " + entities[i].behaviors[j].chance + "\n");
                    }

                    temp += "\t}\n";
                }           
            }

            if (endConditions.Count > 0)
            {
                temp += "\n\t--End Conditions--\n";
                for (int i = 0; i < endConditions.Count; i++)
                {
                    temp += ("\tCondition: " + endConditions[i].name + " -> ");
                    temp += ("(Subject: " + endConditions[i].conditionSubject);
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
                temp += "\n\t--Verbs--\n";
                for (int i = 0; i < verbs.Count; i++)
                {
                    temp += ("\t" + verbs[i].name + "\n");
                    temp += "\t{\n";
                    
                    if (verbs[i].variables.Count > 0)
                    {
                        for (int j = 0; j < verbs[i].variables.Count; j++)
                            temp += ("\t\tVariable: " + verbs[i].variables[j] + "\n");
                    }

                    if (verbs[i].input.Count > 0)
                    {
                        temp += "\t\tInput: ";
                        for (int j = 0; j < verbs[i].input.Count; j++)
                            temp += (verbs[i].input[j] + " ");
                        temp += "\n";
                    }

                    if (verbs[i].output.Count > 0)
                    {
                        temp += "\t\tOutput: ";
                        for (int j = 0; j < verbs[i].output.Count; j++)
                            temp += (verbs[i].output[j] + " ");
                        temp += "\n";
                    }

                    if (verbs[i].conditions.Count > 0)
                    {
                        for (int j = 0; j < verbs[i].conditions.Count; j++)
                        {
                            temp += ("\t\tCondition: " + verbs[i].conditions[j].name + " -> ");
                            temp += ("(Subject: " + verbs[i].conditions[j].conditionSubject);
                            if (verbs[i].conditions[j].negate)
                                temp += (" | Negate: True ");
                            else
                                temp += (" | Negate: False ");

                            if (verbs[i].conditions[j].type == 0)
                                temp += ("| Attribute: " + verbs[i].conditions[j].attribute.name + ")\n");
                            else if (verbs[i].conditions[j].type == 1)
                                temp += ("| Relationship: " + verbs[i].conditions[j].relationship.name + " | Object: " + verbs[i].conditions[j].relationship.other + ")\n");
                        }
                    }

                    if (verbs[i].operators.Count > 0)
                    {
                        for (int j = 0; j < verbs[i].operators.Count; j++)
                        {
                            temp += ("\t\tOperator: " + verbs[i].operators[j].name + " -> ");
                            temp += ("(Subject: " + verbs[i].operators[j].operatorSubject);
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

                    temp += "\t}\n";
                }
            }

            if (goals.Count > 0)
            {
                for (int i = 0; i < goals.Count; i++)
                {
                    temp += ("\tGoal: " + goals[i].name + "\n");
                    temp += ("\tOperator: " + goals[i].goalOperator.name + " -> ");
                    temp += ("(Subject: " + goals[i].goalOperator.operatorSubject);
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

            temp += "}";
            return temp;
        }
    }
}
