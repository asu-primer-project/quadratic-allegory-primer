using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LudoNarrareSimpleInterfacePrototype
{
    class Verb
    {
        /* Variables */
        public string name;
        public List<string> variables;
        public List<string> input;
        public List<string> output;
        public List<Condition> conditions;
        public List<Operator> operators;
        public DynamicVerbTreeNode root;

        /* Functions */
        public Verb(string _name)
        {
            name = _name;
            variables = new List<string>();
            variables.Add("?me");
            input = new List<string>();
            output = new List<string>();
            conditions = new List<Condition>();
            operators = new List<Operator>();
            root = null;
        }

        public void replaceWith(string replace, string with)
        {
            for (int i = 0; i < output.Count; i++ )
            {
                if (output[i] == replace)
                    output[i] = with;
            }

            for (int i = 0; i < conditions.Count; i++ )
                conditions[i].replaceWith(replace, with);

            for (int i = 0; i < operators.Count; i++)
                operators[i].replaceWith(replace, with);
        }

        public void copyTo(Verb verb)
        {
            if (verb != null)
            {
                verb.name = name;

                //Copy variables 
                verb.variables.Clear();
                for (int i = 0; i < variables.Count; i++)
                    verb.variables.Add(variables[i]);

                //Copy input
                verb.input.Clear();
                for (int i = 0; i < input.Count; i++)
                    verb.input.Add(input[i]);

                //Copy output
                verb.output.Clear();
                for (int i = 0; i < output.Count; i++)
                    verb.output.Add(output[i]);

                //Copy conditions
                verb.conditions.Clear();
                for (int i = 0; i < conditions.Count; i++)
                {
                    Condition temp = new Condition("", "", false, 0);
                    conditions[i].copyTo(temp);
                    verb.conditions.Add(temp);
                }

                //Copy operators
                verb.operators.Clear();
                for (int i = 0; i < operators.Count; i++)
                {
                    Operator temp = new Operator("", "", false, 0, "", "", "");
                    operators[i].copyTo(temp);
                    verb.operators.Add(temp);
                }
            }
        }

        public override string ToString()
        {
            string s = "";
            for (int i = 0; i < input.Count; i++)
            {
                    s += (input[i] + " ");
            }
            return s;
        }
    }
}
