using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LudoNarrareSimpleInterfacePrototype
{
    class Engine
    {
        /* Variables */
        public StoryWorld storyWorld;
        public int tick;
        public string output;
        public bool waitingForInput;
        public List<Verb> currentUserChoices; 

        /* Functions */
        public Engine(StoryWorld _storyWorld)
        {
            storyWorld = _storyWorld;
            tick = 0;
            output = "";
            waitingForInput = false;
            currentUserChoices = null;
        }

        public void takeInputAndProcess(Verb choice)
        {
            //Handle user action
            waitingForInput = false;
            
            if (choice.name != "Wait")
                output += (tick + ": " + executeVerb(choice) + "\n");

            //Handle AI
            handleAI();
            tick++;

            //Now wait for input again
            currentUserChoices = generatePossibleVerbs(storyWorld.entities.Find(x => x.name == storyWorld.userEntity));

            while (currentUserChoices.Count <= 1)
            {
                //Handle AI
                handleAI();
                tick++;

                //Now wait for input again
                currentUserChoices = generatePossibleVerbs(storyWorld.entities.Find(x => x.name == storyWorld.userEntity));
            }

            waitingForInput = true;
        }

        public void init()
        {
            currentUserChoices = generatePossibleVerbs(storyWorld.entities.Find(x => x.name == storyWorld.userEntity));
            waitingForInput = true;
        }

        public string printVerb(Verb v)
        {
            string s = "";
            if (v.output.Count > 0)
            {
                for (int i = 0; i < v.output.Count; i++)
                    s += (v.output[i] + " ");
                s = Char.ToUpper(s[0]) + s.Substring(1);
            }
            return s;
        }

        public List<Verb> generatePossibleVerbs(Entity e)
        {
            //Get all verbs
            List<Verb> list = new List<Verb>();
            for (int i = 0; i < storyWorld.verbs.Count; i++ )
            {
                Verb temp = new Verb("");
                storyWorld.verbs[i].copyTo(temp);
                temp.replaceWith("?me", e.name);

                list.Add(temp);
            }

            List<string> thoseToRemove = new List<string>();
            
            //Select verbs that match conditions
            for (int i = 0; i < list.Count; i++ )
            {             
                for (int j = 0; j < list[i].conditions.Count; j++ )
                {
                    if (!checkCondition(list[i].conditions[j]))
                    {
                        thoseToRemove.Add(list[i].name);
                        break;
                    }
                }

                //If there is more than one variable, create the variable tree and see if it has a possible route.
                if (list[i].variables.Count > 1 && thoseToRemove.Find(x => x == list[i].name) == null)
                {
                    list[i].root = new DynamicVerbTreeNode(e);
                    DynamicVerbTreeNode currentDVT = list[i].root;
                    
                    for (int j = 1; j < list[i].variables.Count; j++ )
                    {
                        for (int k = 0; k < storyWorld.entities.Count; k++ )
                        {
                            if (isEntitySolution(storyWorld.entities[k], list[i], list[i].variables[j]))
                                currentDVT.children.Add(new DynamicVerbTreeNode(storyWorld.entities[k]));
                        }
                    }

                    if (currentDVT.getHeight(0) < list[i].variables.Count)
                        thoseToRemove.Add(list[i].name);
                }
            }

            //Remove those which don't match conditions
            
            for (int i = 0; i < thoseToRemove.Count; i++ )
                list.RemoveAll(x => x.name == thoseToRemove[i]);

            return list;
        }

        public bool checkCondition(Condition c)
        {
            Entity tempE = null;

            //Ignore if it is a condition for a variable, this will be handled later.
            if (c.conditionSubject[0] == '?')
                return true;

            tempE = storyWorld.entities.Find(x => x.name == c.conditionSubject);

            //Does the subject exist
            if (tempE != null)
            {
                if (c.type == 0)
                {
                    Attribute tempA = null; 
                    tempA = tempE.attributes.Find(x => x.name == c.attribute.name);
                    
                    return (tempA != null) ? !c.negate : c.negate;
                }
                else if (c.type == 1)
                {
                    Relationship tempR = null;
                    //If the object is a variable, don't worry about it yet.
                    if (c.relationship.other[0] == '?')
                        tempR = tempE.relationships.Find(x => x.name == c.relationship.name);
                    else
                        tempR = tempE.relationships.Find(x => x.name == c.relationship.name && x.other == c.relationship.other);

                    return (tempR != null) ? !c.negate : c.negate;
                }
                else
                    return false;
            }
            else
                return false;
        }
        
        public bool isEntitySolution(Entity e, Verb v, string varName)
        {
            //Get all conditions pertaining to the variable, replace the variable with the candidate entity, and then check if the entity works as a solution.
            List<Condition> varConditions = new List<Condition>();

            for (int i = 0; i < v.conditions.Count; i++ )
            {
                if (v.conditions[i].conditionSubject == varName)
                {
                    Condition tempC = new Condition("", "", false, 0);
                    v.conditions[i].copyTo(tempC);
                    tempC.replaceWith(varName, e.name);
                    varConditions.Add(tempC);
                }
            }            

            for (int i = 0; i < varConditions.Count; i++)
            {
                if (!checkCondition(varConditions[i]))
                    return false;
            }

            return true;
        }

        public void applyOperator(Operator o)
        {
            Entity subject = storyWorld.entities.Find(x => x.name == o.operatorSubject);

            if (subject != null)
            {
                if (o.addRemove)
                {
                    //Add
                    switch (o.type)
                    {
                        case 0:
                            if (o.attribute != null)
                            {
                                Attribute tempA = new Attribute("");
                                o.attribute.copyTo(tempA);
                                if (subject.attributes.Find(x => x.name == tempA.name) == null)
                                    subject.attributes.Add(tempA);
                            }
                            break;
                        case 1:
                            if (o.relationship != null)
                            {
                                Relationship tempR = new Relationship("", "");
                                o.relationship.copyTo(tempR);
                                if (subject.relationships.Find(x => x.name == tempR.name && x.other == tempR.other) == null)
                                    subject.relationships.Add(tempR);
                            }
                            break;
                        case 2:
                            if (subject.obligations.Find(x => x == o.obligation) == null && subject.name != storyWorld.userEntity)
                                subject.obligations.Add(o.obligation);
                            break;
                        case 3:
                            if (subject.goals.Find(x => x == o.goal) == null && subject.name != storyWorld.userEntity)
                                subject.goals.Add(o.goal);
                            break;
                        case 4:
                            if (o.behavior != null && subject.name != storyWorld.userEntity)
                            {
                                BehaviorReference tempB = new BehaviorReference("", 0);
                                o.behavior.copyTo(tempB);
                                if (subject.behaviors.Find(x => x.name == tempB.name) == null)
                                    subject.behaviors.Add(tempB);
                                else
                                    subject.behaviors.Find(x => x.name == tempB.name).chance = tempB.chance;
                            }
                            break;
                    }
                }
                else
                {
                    //Remove
                    switch (o.type)
                    {
                        case 0:
                            if (o.attribute != null)
                                subject.attributes.RemoveAll(x => x.name == o.attribute.name);
                            break;
                        case 1:
                            if (o.relationship != null)
                                subject.relationships.RemoveAll(x => x.name == o.relationship.name && x.other == o.relationship.other);
                            break;
                        case 2:
                            subject.obligations.RemoveAll(x => x == o.obligation);
                            break;
                        case 3:
                            subject.goals.RemoveAll(x => x == o.goal);
                            break;
                        case 4:
                            if (o.behavior != null)
                                subject.behaviors.RemoveAll(x => x.name == o.behavior.name);
                            break;
                    }
                }
            }
        }

        //Has side-effects...
        public string executeVerb(Verb v)
        {
            if (v != null)
            {
                for (int i = 0; i < v.operators.Count; i++)
                    applyOperator(v.operators[i]);

                return printVerb(v);
            }
            else
                return "";
        }

        public List<Entity> shuffleEntities(List<Entity> EL)
        {
            List<Entity> newEL = new List<Entity>();
            
            while (EL.Count > 0)
            {
                Random r = new Random(DateTime.Now.Millisecond);
                int e = r.Next(0, EL.Count);
                newEL.Add(EL[e]);
                EL.RemoveAt(e);
            }

            return newEL;
        }

        public void handleAI()
        {
            List<Entity> actors = shuffleEntities(storyWorld.entities.FindAll(x => x.obligations.Count > 0 || x.goals.Count > 0 || x.behaviors.Count > 0));

            for (int i = 0; i < actors.Count; i++)
                AIAct(actors[i]);
        }

        public void AIAct(Entity e)
        {
            List<Verb> possibleActions = generatePossibleVerbs(e);
            Verb choice = null;

            //Any obligations?
            if (e.obligations.Count > 0)
            {
                List<Verb> possibleObligations = possibleActions.FindAll(x => e.obligations.Contains(x.name));
                if (possibleObligations.Count > 0)
                {
                    Random r = new Random(DateTime.Now.Millisecond);
                    choice = possibleObligations[r.Next(0, possibleObligations.Count)];
                }        
            }

            //Any goals?
            if (choice == null && e.goals.Count > 0)
            {
                List<Goal> eGoals = storyWorld.goals.FindAll(x => e.goals.Contains(x.name));
                List<Verb> possibleGoalVerbs = new List<Verb>();
                for (int i = 0; i < eGoals.Count; i++ )
                    possibleGoalVerbs.AddRange(possibleActions.FindAll(x => x.operators.Contains<Operator>(eGoals[i].goalOperator)));          

                if (possibleGoalVerbs.Count > 0)
                {
                    Random r = new Random(DateTime.Now.Millisecond);
                    choice = possibleGoalVerbs[r.Next(0, possibleGoalVerbs.Count)];
                }
            }

            //Any behaviors?
            if (choice == null && e.behaviors.Count > 0)
            {
                List<string> verbNames = new List<string>();
                for (int i = 0; i < possibleActions.Count; i++ )
                    verbNames.Add(possibleActions[i].name);

                List<BehaviorReference> possibleBehaviors = e.behaviors.FindAll(x => verbNames.Contains(x.name));
                if (possibleBehaviors.Count > 0)
                {
                    List<string> ballSpinner = new List<string>();
                    
                    for (int i = 0; i < possibleBehaviors.Count; i++ )
                    {
                        for (int j = 0; j < possibleBehaviors[i].chance; j++)
                            ballSpinner.Add(possibleBehaviors[i].name);
                    }

                    Random r = new Random(DateTime.Now.Millisecond);
                    string c = ballSpinner[r.Next(0, ballSpinner.Count)];
                    choice = possibleActions.Find(x => x.name == c);
                }
            }

            if (choice.name != "Wait" && choice != null)
                output += (tick + ": " + executeVerb(choice) + "\n");
        }
    }
}
