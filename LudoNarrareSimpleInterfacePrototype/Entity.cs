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
        public List<string> obligations;
        public List<string> goals;
        public List<BehaviorReference> behaviors;

        /* Functions */
        public Entity(string _name)
        {
            name = _name;
            attributes = new List<Attribute>();
            relationships = new List<Relationship>();
            obligations = new List<string>();
            goals = new List<string>();
            behaviors = new List<BehaviorReference>();
        }
    }
}
