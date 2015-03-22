using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LudoNarrareSimpleInterfacePrototype
{
    class Entity
    {
        /* Variables */
        public string name;
        public List<Attribute> attributes;
        public List<Relationship> relationships;
        public List<Obligation> obligations;
        public List<Goal> goals;
        public List<Behavior> behaviors;

        /* Functions */
        public Entity(string _name)
        {
            name = _name;
            attributes = new List<Attribute>();
            relationships = new List<Relationship>();
            obligations = new List<Obligation>();
            goals = new List<Goal>();
            behaviors = new List<Behavior>();
        }
    }
}
