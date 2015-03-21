using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LudoNarrareSimpleInterfacePrototype
{
    class BehaviorReference
    {
        /* Variables */
        public string name;
        public int chance;

        /* Functions */
        public BehaviorReference(string _name, int _chance)
        {
            name = _name;
            chance = _chance;
        }

        public void copyTo(BehaviorReference br)
        {
            if (br != null)
            {
                br.name = name;
                br.chance = chance;
            }
        }
    }
}
