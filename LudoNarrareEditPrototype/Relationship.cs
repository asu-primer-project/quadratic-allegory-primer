using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LudoNarrareEditPrototype
{
    class Relationship
    {
        /* Variables */
        public string name;
        public Entity other;

        /* Functions */
        public Relationship(string _name)
        {
            name = _name;
            other = null;
        }

        public void setName(string _name)
        {
            name = _name;
        }

        public void setOther(Entity _other)
        {
            other = _other;
        }

        public Relationship copy(Relationship relationship)
        {
            relationship.setName(name);
            relationship.setOther(other);
            return relationship;
        }

        public override string ToString()
        {
            if (other == null)
            {
                return (name + "<>");
            }
            else
            {
                return (name + "<" + other.name + ">");
            }
        }
    }
}
