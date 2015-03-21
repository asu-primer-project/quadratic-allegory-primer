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
        enum LoadState { loadFile, loadStoryWorld, loadEntity, loadEndCondition, loadVerb, loadGoal };

        private StoryWorld sw;
        private Engine engine;
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
            bool success = false;
            
            if (result == DialogResult.OK)
            {
                swFileLoc = OpenWorldDataDialog.FileName;
                XmlTextReader reader = new XmlTextReader(swFileLoc);
                //Variables for tracking data added
                LoadState ls = LoadState.loadFile;
                Entity currentEntity = null;
                Verb currentVerb = null;
                Goal currentGoal = null;
                
                while (reader.Read())
                {
                    switch (reader.NodeType)
                    {
                        case XmlNodeType.Element:
                            //Read story world header data
                            if (reader.Name == "StoryWorld" && ls == LoadState.loadFile)
                            {
                                ls = LoadState.loadStoryWorld;
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
                            if (reader.Name == "Entity" && ls == LoadState.loadStoryWorld)
                            {
                                ls = LoadState.loadEntity;
                                currentEntity = new Entity("Entity");
                                sw.entities.Add(currentEntity);

                                while (reader.MoveToNextAttribute())
                                {
                                    if (reader.Name == "name")
                                        currentEntity.name = reader.Value;
                                }
                            }

                            if (reader.Name == "Attribute" && ls == LoadState.loadEntity)
                            {
                                Attribute temp = new Attribute("Attribute");
                                
                                while (reader.MoveToNextAttribute())
                                {
                                    if (reader.Name == "name")
                                        temp.name = reader.Value;
                                }

                                currentEntity.attributes.Add(temp);
                            }

                            if (reader.Name == "Relationship" && ls == LoadState.loadEntity)
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

                            if (reader.Name == "Obligation" && ls == LoadState.loadEntity)
                            {
                                string temp = "Obligation";

                                while (reader.MoveToNextAttribute())
                                {
                                    if (reader.Name == "verb")
                                        temp = reader.Value;
                                }

                                currentEntity.obligations.Add(temp);
                            }

                            if (reader.Name == "Goal" && ls == LoadState.loadEntity)
                            {
                                string temp = "Goal";

                                while (reader.MoveToNextAttribute())
                                {
                                    if (reader.Name == "name")
                                        temp = reader.Value;
                                }

                                currentEntity.goals.Add(temp);
                            }

                            if (reader.Name == "Behavior" && ls == LoadState.loadEntity)
                            {
                                BehaviorReference temp = new BehaviorReference("Behavior", 0);

                                while (reader.MoveToNextAttribute())
                                {
                                    if (reader.Name == "verb")
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
                            if (reader.Name == "EndConditions" && ls == LoadState.loadStoryWorld)
                                ls = LoadState.loadEndCondition;

                            if (reader.Name == "Condition" && ls == LoadState.loadEndCondition)
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
                            if (reader.Name == "Verb" && ls == LoadState.loadStoryWorld)
                            {
                                ls = LoadState.loadVerb;
                                currentVerb = new Verb("Verb");
                                sw.verbs.Insert(0, currentVerb);

                                while (reader.MoveToNextAttribute())
                                {
                                    if (reader.Name == "name" && reader.Value != "Wait")
                                        currentVerb.name = reader.Value;
                                }
                            }

                            if (reader.Name == "Variable" && ls == LoadState.loadVerb)
                            {
                                while (reader.MoveToNextAttribute())
                                {
                                    if (reader.Name == "name" && reader.Value != "?me")
                                        currentVerb.variables.Add(reader.Value);
                                }
                            }

                            if (reader.Name == "In" && ls == LoadState.loadVerb)
                            {
                                while (reader.MoveToNextAttribute())
                                {
                                    if (reader.Name == "text")
                                        currentVerb.input.Add(reader.Value);
                                }
                            }

                            if (reader.Name == "Out" && ls == LoadState.loadVerb)
                            {
                                while (reader.MoveToNextAttribute())
                                {
                                    if (reader.Name == "text")
                                        currentVerb.output.Add(reader.Value);
                                }
                            }

                            if (reader.Name == "Condition" && ls == LoadState.loadVerb)
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

                            if (reader.Name == "Operator" && ls == LoadState.loadVerb)
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
                                        if (reader.Value == "remove")
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

                            //Read goal data
                            if (reader.Name == "Goal" && ls == LoadState.loadStoryWorld)
                            {
                                ls = LoadState.loadGoal;
                                currentGoal = new Goal("Goal");
                                sw.goals.Add(currentGoal);

                                while (reader.MoveToNextAttribute())
                                {
                                    if (reader.Name == "name")
                                        currentGoal.name = reader.Value;
                                }
                            }

                            if (reader.Name == "Operator" && ls == LoadState.loadGoal)
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
                                        if (reader.Value == "remove")
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

                            break;
                        case XmlNodeType.EndElement:
                            //Back out of story world data
                            if (reader.Name == "StoryWorld" && ls == LoadState.loadStoryWorld)
                                ls = LoadState.loadFile;
                            
                            //Back out of entity data
                            if (reader.Name == "Entity" && ls == LoadState.loadEntity)
                                ls = LoadState.loadStoryWorld;

                            //Back out of end conditions
                            if (reader.Name == "EndConditions" && ls == LoadState.loadEndCondition)
                                ls = LoadState.loadStoryWorld;

                            //Back out of verbs
                            if (reader.Name == "Verb" && ls == LoadState.loadVerb)
                                ls = LoadState.loadStoryWorld;

                            //Back out of goal data
                            if (reader.Name == "Goal" && ls == LoadState.loadGoal)
                                ls = LoadState.loadStoryWorld;

                            break;
                    }
                }

                success = (ls == LoadState.loadFile) ? true : false;                 
            }

            //Initiate the engine with the loaded story world data.
            if (sw != null && success)
            {
                WorldDataOutBox.Text = sw.getPrintedWorldState();
                engine = new Engine(sw);
                engine.init();
                comboBoxVerb.DataSource = engine.currentUserChoices;
            }
            else
                MessageBox.Show("Story World data could not be loaded correctly.", "Error", MessageBoxButtons.OK);
        }

        //Update output boxes on tab change
        private void Tabs_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (sw != null)
                WorldDataOutBox.Text = sw.getPrintedWorldState();
            if (engine != null)
                OutputBox.Text = engine.output;
        }

        //Process verb chosen and then update output boxes
        private void buttonInput_Click(object sender, EventArgs e)
        {
            //Process verb
            engine.takeInputAndProcess(engine.currentUserChoices[comboBoxVerb.SelectedIndex]);
            comboBoxVerb.DataSource = engine.currentUserChoices;

            //Update output
            if (sw != null)
                WorldDataOutBox.Text = sw.getPrintedWorldState();
            if (engine != null)
                OutputBox.Text = engine.output;
        }

        //Exit program
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
