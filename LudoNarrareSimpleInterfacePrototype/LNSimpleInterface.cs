using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Xml;

namespace LudoNarrareSimpleInterfacePrototype
{
    public partial class LNSimpleInterface : Form
    {
        private StoryWorld sw;
        private string swFileLoc;
        
        //Constructer/Setup
        public LNSimpleInterface()
        {
            InitializeComponent();
        }

        //Load story world file
        private void loadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result = OpenWorldDataDialog.ShowDialog();
            
            if (result == DialogResult.OK)
            {
                swFileLoc = OpenWorldDataDialog.FileName;
                XmlTextReader reader = new XmlTextReader(swFileLoc);
                //Variables for tracking data added
                int storyWorldLevel = 0;
                int entityLevel = 0;
                int endConditionLevel = 0;
                int verbLevel = 0;
                int obligationLevel = 0;
                int goalLevel = 0;
                int behaviorLevel = 0;
                Entity currentEntity = null;
                Verb currentVerb = null;
                Obligation currentObligation = null;
                Goal currentGoal = null;
                
                while (reader.Read())
                {
                    switch (reader.NodeType)
                    {
                        case XmlNodeType.Element:
                            //Read story world header data
                            if (reader.Name == "StoryWorld")
                            {
                                storyWorldLevel = 1;
                                sw = new StoryWorld("StoryWorld", "UserEntity");

                                while (reader.MoveToNextAttribute())
                                {
                                    if (reader.Name == "name")
                                        sw.name = reader.Value;
                                    if (reader.Name == "userEntity")
                                        sw.userEntity = reader.Value;
                                }   
                            }
                            
                            //Read entity data
                            if (reader.Name == "Entities" && storyWorldLevel == 1)
                                entityLevel = 1;

                            if (reader.Name == "Entity" && entityLevel == 1)
                            {
                                entityLevel = 2;
                                currentEntity = new Entity("Entity");
                                sw.entities.Add(currentEntity);

                                while (reader.MoveToNextAttribute())
                                {
                                    if (reader.Name == "name")
                                        currentEntity.name = reader.Value;
                                }
                            }

                            if (reader.Name == "Attributes" && entityLevel == 2)
                                entityLevel = 3;

                            if (reader.Name == "Relationships" && entityLevel == 2)
                                entityLevel = 3;

                            if (reader.Name == "Obligations" && entityLevel == 2)
                                entityLevel = 3;

                            if (reader.Name == "Goals" && entityLevel == 2)
                                entityLevel = 3;

                            if (reader.Name == "Behaviors" && entityLevel == 2)
                                entityLevel = 3;

                            if (reader.Name == "Attribute" && entityLevel == 3)
                            {
                                Attribute temp = new Attribute("Attribute");
                                
                                while (reader.MoveToNextAttribute())
                                {
                                    if (reader.Name == "name")
                                        temp.name = reader.Value;
                                }

                                currentEntity.attributes.Add(temp);
                            }

                            if (reader.Name == "Relationship" && entityLevel == 3)
                            {
                                Relationship temp = new Relationship("Relationship","Other");

                                while (reader.MoveToNextAttribute())
                                {
                                    if (reader.Name == "name")
                                        temp.name = reader.Value;
                                    if (reader.Name == "object")
                                        temp.other = reader.Value;
                                }

                                currentEntity.relationships.Add(temp);
                            }

                            if (reader.Name == "Obligation" && entityLevel == 3)
                            {
                                string temp = "Obligation";

                                while (reader.MoveToNextAttribute())
                                {
                                    if (reader.Name == "name")
                                        temp = reader.Value;
                                }

                                currentEntity.obligations.Add(temp);
                            }

                            if (reader.Name == "Goal" && entityLevel == 3)
                            {
                                string temp = "Goal";

                                while (reader.MoveToNextAttribute())
                                {
                                    if (reader.Name == "name")
                                        temp = reader.Value;
                                }

                                currentEntity.goals.Add(temp);
                            }

                            if (reader.Name == "Behavior" && entityLevel == 3)
                            {
                                BehaviorReference temp = new BehaviorReference("Behavior", 0);

                                while (reader.MoveToNextAttribute())
                                {
                                    if (reader.Name == "name")
                                        temp.name = reader.Value;
                                    if (reader.Name == "chance")
                                    {
                                        if (!Int32.TryParse(reader.Value, out temp.chance))
                                            temp.chance = 0;
                                    }
                                }

                                currentEntity.behaviors.Add(temp);
                            }
                            
                            //Read end condition data
                            if (reader.Name == "EndConditions" && storyWorldLevel == 1)
                                endConditionLevel = 1;

                            if (reader.Name == "Condition" && endConditionLevel == 1)
                            {
                                Condition temp = new Condition("Condition", "Subject", false, 0);

                                while (reader.MoveToNextAttribute())
                                {
                                    if (reader.Name == "name")
                                        temp.name = reader.Value;
                                    if (reader.Name == "subject")
                                        temp.conditionSubject = reader.Value;
                                    if (reader.Name == "negate")
                                    {
                                        if (reader.Value == "true")
                                            temp.negate = true;
                                        else
                                            temp.negate = false;
                                    }
                                    if (reader.Name == "attribute")
                                    {
                                        temp.type = 0;
                                        temp.attribute = new Attribute(reader.Value);
                                    }
                                    else if (reader.Name == "relationship")
                                    {
                                        temp.type = 1;
                                        temp.relationship = new Relationship(reader.Value, "Object");
                                    }
                                    if (reader.Name == "object" && temp.type == 1)
                                        temp.relationship.other = reader.Value;
                                }

                                sw.endConditions.Add(temp);
                            }

                            //Read verb data
                            if (reader.Name == "Verbs" && storyWorldLevel == 1)
                                verbLevel = 1;

                            if (reader.Name == "Verb" && verbLevel == 1)
                            {
                                verbLevel = 2;
                                currentVerb = new Verb("Verb");
                                sw.verbs.Add(currentVerb);

                                while (reader.MoveToNextAttribute())
                                {
                                    if (reader.Name == "name")
                                        currentVerb.name = reader.Value;
                                }
                            }

                            if (reader.Name == "Variables" && verbLevel == 2)
                                verbLevel = 3;

                            if (reader.Name == "Variable" && verbLevel == 3)
                            {
                                while (reader.MoveToNextAttribute())
                                {
                                    if (reader.Name == "name")
                                        currentVerb.variables.Add(reader.Value);
                                }
                            }

                            if (reader.Name == "Output" && verbLevel == 2)
                                verbLevel = 3;

                            if (reader.Name == "Out" && verbLevel == 3)
                            {
                                while (reader.MoveToNextAttribute())
                                {
                                    if (reader.Name == "name")
                                        currentVerb.output.Add(reader.Value);
                                }
                            }

                            if (reader.Name == "Conditions" && verbLevel == 2)
                                verbLevel = 3;

                            if (reader.Name == "Condition" && verbLevel == 3)
                            {
                                Condition temp = new Condition("Condition", "Subject", false, 0);

                                while (reader.MoveToNextAttribute())
                                {
                                    if (reader.Name == "name")
                                        temp.name = reader.Value;
                                    if (reader.Name == "subject")
                                        temp.conditionSubject = reader.Value;
                                    if (reader.Name == "negate")
                                    {
                                        if (reader.Value == "true")
                                            temp.negate = true;
                                        else
                                            temp.negate = false;
                                    }
                                    if (reader.Name == "attribute")
                                    {
                                        temp.type = 0;
                                        temp.attribute = new Attribute(reader.Value);
                                    }
                                    else if (reader.Name == "relationship")
                                    {
                                        temp.type = 1;
                                        temp.relationship = new Relationship(reader.Value, "Object");
                                    }
                                    if (reader.Name == "object" && temp.type == 1)
                                        temp.relationship.other = reader.Value;
                                }

                                currentVerb.conditions.Add(temp);
                            }

                            if (reader.Name == "Operators" && verbLevel == 2)
                                verbLevel = 3;

                            if (reader.Name == "Operator" && verbLevel == 3)
                            {
                                Operator temp = new Operator("Operator", "Subject", true, 0, "Obligation", "Goal", "?Var");

                                while (reader.MoveToNextAttribute())
                                {
                                    if (reader.Name == "name")
                                        temp.name = reader.Value;
                                    if (reader.Name == "subject")
                                        temp.operatorSubject = reader.Value;
                                    if (reader.Name == "type")
                                    {
                                        if (reader.Value == "Remove")
                                            temp.addRemove = false;
                                        else
                                            temp.addRemove = true;
                                    }
                                    if (reader.Name == "attribute")
                                    {
                                        temp.type = 0;
                                        temp.attribute = new Attribute(reader.Value);
                                    }
                                    else if (reader.Name == "relationship")
                                    {
                                        temp.type = 1;
                                        temp.relationship = new Relationship(reader.Value, "Object");
                                    }
                                    else if (reader.Name == "obligation")
                                    {
                                        temp.type = 2;
                                        temp.obligation = reader.Value;
                                    }
                                    else if (reader.Name == "goal")
                                    {
                                        temp.type = 3;
                                        temp.goal = reader.Value;
                                    }
                                    else if (reader.Name == "behavior")
                                    {
                                        temp.type = 4;
                                        temp.behavior = new BehaviorReference(reader.Value, 0);
                                    }
                                    if (reader.Name == "object" && temp.type == 1)
                                        temp.relationship.other = reader.Value;
                                    if (reader.Name == "goalVariable" && temp.type == 3)
                                        temp.goalVariable = reader.Value;
                                    if (reader.Name == "chance" && temp.type == 4)
                                    {
                                        if (!Int32.TryParse(reader.Value, out temp.behavior.chance))
                                            temp.behavior.chance = 0;
                                    }
                                }
                                
                                currentVerb.operators.Add(temp);
                            }

                            //Read obligation data
                            if (reader.Name == "Obligations" && storyWorldLevel == 1)
                                obligationLevel = 1;

                            if (reader.Name == "Obligation" && obligationLevel == 1)
                            {
                                obligationLevel = 2;
                                currentObligation = new Obligation("Obligation", "Verb");
                                sw.obligations.Add(currentObligation);

                                while (reader.MoveToNextAttribute())
                                {
                                    if (reader.Name == "name")
                                        currentObligation.name = reader.Value;
                                    if (reader.Name == "verb")
                                        currentObligation.verb = reader.Value;
                                }
                            }

                            if (reader.Name == "Conditions" && obligationLevel == 2)
                                obligationLevel = 3;

                            if (reader.Name == "Condition" && obligationLevel == 3)
                            {
                                Condition temp = new Condition("Condition", "Subject", false, 0);

                                while (reader.MoveToNextAttribute())
                                {
                                    if (reader.Name == "name")
                                        temp.name = reader.Value;
                                    if (reader.Name == "subject")
                                        temp.conditionSubject = reader.Value;
                                    if (reader.Name == "negate")
                                    {
                                        if (reader.Value == "true")
                                            temp.negate = true;
                                        else
                                            temp.negate = false;
                                    }
                                    if (reader.Name == "attribute")
                                    {
                                        temp.type = 0;
                                        temp.attribute = new Attribute(reader.Value);
                                    }
                                    else if (reader.Name == "relationship")
                                    {
                                        temp.type = 1;
                                        temp.relationship = new Relationship(reader.Value, "Object");
                                    }
                                    if (reader.Name == "object" && temp.type == 1)
                                        temp.relationship.other = reader.Value;
                                }

                                currentObligation.conditions.Add(temp);
                            }

                            //Read goal data
                            if (reader.Name == "Goals" && storyWorldLevel == 1)
                                goalLevel = 1;

                            if (reader.Name == "Goal" && goalLevel == 1)
                            {
                                goalLevel = 2;
                                currentGoal = new Goal("Goal");
                                sw.goals.Add(currentGoal);

                                while (reader.MoveToNextAttribute())
                                {
                                    if (reader.Name == "name")
                                        currentGoal.name = reader.Value;
                                }
                            }

                            if (reader.Name == "Operator" && goalLevel == 2)
                            {
                                Operator temp = new Operator("Operator", "Subject", true, 0, "Obligation", "Goal", "?Var");

                                while (reader.MoveToNextAttribute())
                                {
                                    if (reader.Name == "name")
                                        temp.name = reader.Value;
                                    if (reader.Name == "subject")
                                        temp.operatorSubject = reader.Value;
                                    if (reader.Name == "type")
                                    {
                                        if (reader.Value == "Remove")
                                            temp.addRemove = false;
                                        else
                                            temp.addRemove = true;
                                    }
                                    if (reader.Name == "attribute")
                                    {
                                        temp.type = 0;
                                        temp.attribute = new Attribute(reader.Value);
                                    }
                                    else if (reader.Name == "relationship")
                                    {
                                        temp.type = 1;
                                        temp.relationship = new Relationship(reader.Value, "Object");
                                    }
                                    else if (reader.Name == "obligation")
                                    {
                                        temp.type = 2;
                                        temp.obligation = reader.Value;
                                    }
                                    else if (reader.Name == "goal")
                                    {
                                        temp.type = 3;
                                        temp.goal = reader.Value;
                                    }
                                    else if (reader.Name == "behavior")
                                    {
                                        temp.type = 4;
                                        temp.behavior = new BehaviorReference(reader.Value, 0);
                                    }
                                    if (reader.Name == "object" && temp.type == 1)
                                        temp.relationship.other = reader.Value;
                                    if (reader.Name == "goalVariable" && temp.type == 3)
                                        temp.goalVariable = reader.Value;
                                    if (reader.Name == "chance" && temp.type == 4)
                                    {
                                        if (!Int32.TryParse(reader.Value, out temp.behavior.chance))
                                            temp.behavior.chance = 0;
                                    }
                                }

                                currentGoal.goalOperator = temp;
                            }

                            //Read behavior data
                            if (reader.Name == "Behaviors" && storyWorldLevel == 1)
                                behaviorLevel = 1;

                            if (reader.Name == "Behavior" && behaviorLevel == 1)
                            {
                                Behavior temp = new Behavior("Behavior", "Verb");

                                while (reader.MoveToNextAttribute())
                                {
                                    if (reader.Name == "name")
                                        temp.name = reader.Value;
                                    if (reader.Name == "verb")
                                        temp.verb = reader.Value;
                                }

                                sw.behaviors.Add(temp);
                            }

                            break;
                        case XmlNodeType.EndElement:
                            //Back out of story world data
                            if (reader.Name == "StoryWorld" && storyWorldLevel == 1)
                                storyWorldLevel = 0;
                            
                            //Back out of entity data
                            if (reader.Name == "Entities" && entityLevel == 1)
                                entityLevel = 0;
                            if (reader.Name == "Entity" && entityLevel == 2)
                                entityLevel = 1;
                            if (reader.Name == "Attributes" && entityLevel == 3)
                                entityLevel = 2;
                            if (reader.Name == "Relationships" && entityLevel == 3)
                                entityLevel = 2;
                            if (reader.Name == "Obligations" && entityLevel == 3)
                                entityLevel = 2;
                            if (reader.Name == "Goals" && entityLevel == 3)
                                entityLevel = 2;
                            if (reader.Name == "Behaviors" && entityLevel == 3)
                                entityLevel = 2;

                            //Back out of end conditions
                            if (reader.Name == "EndConditions" && endConditionLevel == 1)
                                endConditionLevel = 0;

                            //Back out of verbs
                            if (reader.Name == "Verbs" && verbLevel == 1)
                                verbLevel = 0;
                            if (reader.Name == "Verb" && verbLevel == 2)
                                verbLevel = 1;
                            if (reader.Name == "Variables" && verbLevel == 3)
                                verbLevel = 2;
                            if (reader.Name == "Output" && verbLevel == 3)
                                verbLevel = 2;
                            if (reader.Name == "Conditions" && verbLevel == 3)
                                verbLevel = 2;
                            if (reader.Name == "Operators" && verbLevel == 3)
                                verbLevel = 2;

                            //Back out of obligation data
                            if (reader.Name == "Obligations" && obligationLevel == 1)
                                obligationLevel = 0;
                            if (reader.Name == "Obligation" && obligationLevel == 2)
                                obligationLevel = 1;
                            if (reader.Name == "Conditions" && obligationLevel == 3)
                                obligationLevel = 2;

                            //Back out of goal data
                            if (reader.Name == "Goals" && goalLevel == 1)
                                goalLevel = 0;
                            if (reader.Name == "Goal" && goalLevel == 2)
                                goalLevel = 1;

                            //Back out of behavior data
                            if (reader.Name == "Behaviors" && behaviorLevel == 1)
                                behaviorLevel = 0;

                            break;
                    }
                }
                    
                //MessageBox.Show("Story World data missing. Could not load", "Error", MessageBoxButtons.OK);
            }

            if (sw != null)
                WorldDataOutBox.Text = sw.getPrintedWorldState();
        }

        //Exit program
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
