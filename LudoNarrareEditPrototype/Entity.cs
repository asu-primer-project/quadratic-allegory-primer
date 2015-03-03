using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LudoNarrareEditPrototype
{
    class Entity
    {
        /* Variables */
        public string name;

        /* Functions */
        public Entity(string _name)
        {
            name = _name;
        }

        public void setName(string _name)
        {
            name = _name;
        }

        public Entity copy(Entity entity)
        {
            entity.setName(name);
            return entity;
        }

        public override string ToString()
        {
            return name;
        }
    }
}
