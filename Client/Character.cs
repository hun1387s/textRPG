using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Client
{
    internal class Character : Object
    {
        // Singleton
        private Character() { }
        private static Character? instance;
        public static Character GetInst()
        {
            if (instance == null)
                instance = new Character();

            return instance;
        }


        private string name = "HUNimation";
        private int level = 1;
        private int dgTry = 0;
        private string job = "무직";
        private float attack = 10f;
        private int defense = 5;
        private int hp = 100;
        private int gold = 1500;
        private Item? weapon = null;
        private Item? armor = null;

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
        public float Attack
        {
            get { return attack; }
            set { attack = value; }
        }
        public int Defense
        {
            get { return defense; }
            set { defense = value; }
        }
        public int HP
        {
            get { return hp; }
            set { hp = value; }
        }
        public int Gold
        {
            get { return gold; }
            set { gold = value; }
        }
        public int DGtry
        {
            get { return dgTry; }
            set { dgTry = value; }
        }
        public Item Weapon
        {
            get { return weapon; }
            set {  weapon = value; }
        }
        public Item Armor
        {
            get { return armor; }
            set {  armor = value; }
        }

        // 레벨 업 구간인지 체크
        public void LevelCheck()
        {
            for (int i = 0; i < Level; i++)
            {
                if (level == i && dgTry == i)
                {
                    level++;
                    dgTry = 0;
                    attack += 0.5f;
                    defense += 1;
                }
            }
        }
    }
}
