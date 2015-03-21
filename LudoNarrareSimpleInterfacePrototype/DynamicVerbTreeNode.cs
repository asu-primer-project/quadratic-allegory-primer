using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LudoNarrareSimpleInterfacePrototype
{
    class DynamicVerbTreeNode
    {
        /* Variables */
        public Entity data;
        public List<DynamicVerbTreeNode> children;

        /* Functions */
        public DynamicVerbTreeNode(Entity _data)
        {
            data = _data;
            children = new List<DynamicVerbTreeNode>();
        }

        public int getHeight(int currentHeight)
        {
            if (children.Count == 0)
                return currentHeight;
            else
            {
                int maxCandidate = 0;

                for (int i = 0; i < children.Count; i++)
                {
                    int temp = children[i].getHeight(currentHeight+1);
                    if (temp > maxCandidate)
                        maxCandidate = temp;
                }

                return maxCandidate;
            }
        }
    }
}
