using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    internal class Item : Object
    {
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        public int Defense { get; set; }
        public int Attack { get; set; }
        public string? Description { get; set; }
        public int Gold { get; set; }
        public bool Equip { get; set; }


    }

}


