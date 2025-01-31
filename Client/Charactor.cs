using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    internal class Charactor
    {
        private int level = 0;
        private string name = "";
        private string job = "";
        private int attack = 0;
        private int defense = 0;
        private int gold = 0;

        public int Level
        {
            get { return level; }
            set { level = value; }
        }
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        public string Job
        {
            get { return job; }
            set { job = value; }
        }
        public int Attack
        {
            get { return attack; }
            set { attack = value; }
        }
        public int Defense
        {
            get { return defense; }
            set { defense = value; }
        }
        public int Gold
        {
            get { return gold; }
            set { gold = value; }
        }

    }
}
