using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    internal class Character : Object
    {
        private Character() { }
        private static Character instance;

        public static Character GetInst()
        {
            if (instance == null)
                instance = new Character();

            return instance;
        }

        private int level = 0;
        private string job = "None";
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
