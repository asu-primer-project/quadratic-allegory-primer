﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LudoNarrareSimpleInterfacePrototype
{
    class Relationship
    {
        /* Variables */
        public string name;
        public string other;

        /* Functions */
        public Relationship(string _name, string _other)
        {
            name = _name;
            other = _other;
        }
    }
}
