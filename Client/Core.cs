using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    internal class Core
    {
        private Core() { }
        private static Core? instance;
        public static Core GetInst()
        {
            if (instance == null)
                instance = new Core();
            return instance;
        }

        Character character = Character.GetInst();
        List<Item> items = new List<Item>();

        Scene currentScene = StartScene.GetInst();

        public int Update()
        {
            currentScene.Enter();
            
            Scene nextScene = currentScene.Exit();
            currentScene = nextScene;
            return 1;
        }

        public void init()
        {
            character = Character.GetInst();
            itemInit();
        }


        private void itemInit()
        {
            items.Add(new Item()
            {
                Name = "수련자 갑옷",
                Defense = 5,
                Description = "수련에 도움을 주는 갑옷입니다.",
                Gold = 1000
            });

            items.Add(new Item()
            {
                Name = "무쇠 갑옷",
                Defense = 9,
                Description = "무쇠로 만들어져 튼튼한 갑옷입니다.",
                Gold = 2000
            });

            items.Add(new Item()
            {
                Name = "스파르타의 갑옷",
                Defense = 15,
                Description = "스파르타의 전사들이 사용했다는 전설의 갑옷입니다.",
                Gold = 3500
            });

            items.Add(new Item()
            {
                Name = "낡은 검",
                Attack = 2,
                Description = "쉽게 볼 수 있는 낡은 검 입니다.",
                Gold = 600
            });

            items.Add(new Item()
            {
                Name = "청동 도끼",
                Attack = 5,
                Description = "어디선가 사용됐던거 같은 도끼입니다.",
                Gold = 1500
            });

            items.Add(new Item()
            {
                Name = "스파르타의 창",
                Attack = 7,
                Description = "스파르타의 전사들이 사용했다는 전설의 창입니다.",
                Gold = 3000
            });
        }
    }
}
