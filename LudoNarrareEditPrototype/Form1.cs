using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LudoNarrareEditPrototype
{
    public partial class Form1 : Form
    {
        private StoryWorld storyWorld;
        
        public Form1()
        {
            storyWorld = new StoryWorld("Temp");

            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            listBox1.DataSource = storyWorld.attributes;
            listBox2.DataSource = storyWorld.relationships;
        }

        //Exit program
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        //Add new attribute
        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "")
            {
                if (!storyWorld.attributes.Exists(x => x.name == textBox1.Text))
                {
                    storyWorld.addNewAttribute(textBox1.Text);
                    listBox1.DataSource = null;
                    listBox1.DataSource = storyWorld.attributes;
                    textBox1.Clear();
                }
                else
                {
                    MessageBox.Show("You can't add an attribute that already exists.", "Error", MessageBoxButtons.OK);
                    textBox1.Clear();
                }
            }
            else
                MessageBox.Show("You can't add an attribute without a name.", "Error", MessageBoxButtons.OK);
        }

        //Remove a selected attribute
        private void button2_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex != -1)
            {
                DialogResult result = MessageBox.Show("Are you sure you want to delete this attribute?", "Warning!", MessageBoxButtons.YesNo);

                if (result == DialogResult.Yes)
                {
                    try
                    {
                        // Remove the item in the List.
                        storyWorld.attributes.RemoveAt(listBox1.SelectedIndex);
                    }
                    catch
                    {
                    }

                    listBox1.DataSource = null;
                    listBox1.DataSource = storyWorld.attributes;
                }
            }
            else
                MessageBox.Show("No attribute selected.", "Error", MessageBoxButtons.OK);
        }

        //Change the name of an attribute
        private void button3_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex != -1)
            {
                if (textBox1.Text != "")
                {
                    storyWorld.attributes[listBox1.SelectedIndex].name = textBox1.Text;
                    listBox1.DataSource = null;
                    listBox1.DataSource = storyWorld.attributes;
                    textBox1.Clear();
                }
                else
                    MessageBox.Show("You can't change an attribute to have no name.", "Error", MessageBoxButtons.OK);
            }
            else
                MessageBox.Show("No attribute selected.", "Error", MessageBoxButtons.OK);
        }

        //Add new relationship
        private void button5_Click(object sender, EventArgs e)
        {
            if (textBox2.Text != "")
            {
                if (!storyWorld.relationships.Exists(x => x.name == textBox2.Text))
                {
                    storyWorld.addNewRelationship(textBox2.Text);
                    listBox2.DataSource = null;
                    listBox2.DataSource = storyWorld.relationships;
                    textBox2.Clear();
                }
                else
                {
                    MessageBox.Show("You can't add a relationship that already exists.", "Error", MessageBoxButtons.OK);
                    textBox2.Clear();
                }
            }
            else
                MessageBox.Show("You can't add a relationship without a name.", "Error", MessageBoxButtons.OK);
        }

        //Remove a selected relationship
        private void button6_Click(object sender, EventArgs e)
        {
            if (listBox2.SelectedIndex != -1)
            {
                DialogResult result = MessageBox.Show("Are you sure you want to delete this relationship?", "Warning!", MessageBoxButtons.YesNo);

                if (result == DialogResult.Yes)
                {
                    try
                    {
                        // Remove the item in the List.
                        storyWorld.relationships.RemoveAt(listBox2.SelectedIndex);
                    }
                    catch
                    {
                    }

                    listBox2.DataSource = null;
                    listBox2.DataSource = storyWorld.relationships;
                }
            }
            else
                MessageBox.Show("No relationship selected.", "Error", MessageBoxButtons.OK);
        }

        //Change the name of an relationship
        private void button4_Click(object sender, EventArgs e)
        {
            if (listBox2.SelectedIndex != -1)
            {
                if (textBox2.Text != "")
                {
                    storyWorld.relationships[listBox2.SelectedIndex].name = textBox2.Text;
                    listBox2.DataSource = null;
                    listBox2.DataSource = storyWorld.relationships;
                    textBox2.Clear();
                }
                else
                    MessageBox.Show("You can't change a relationship to have no name.", "Error", MessageBoxButtons.OK);
            }
            else
                MessageBox.Show("No relationship selected.", "Error", MessageBoxButtons.OK);
        }
    }
}
