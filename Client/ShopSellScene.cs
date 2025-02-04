using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    internal class ShopSellScene : Scene
    {
        // Singleton
        private ShopSellScene() { }
        private static ShopSellScene? instance;
        public static ShopSellScene GetInst()
        {
            if (instance == null)
                instance = new ShopSellScene();
            return instance;
        }

        public override void Enter()
        {
            Clear();
            nextScene = this;
            Character character = Character.GetInst();

            Console.Write("상점 ");
            Console.WriteLine("- 판매");
            Console.WriteLine("필요한 아이템을 얻을 수 있는 상점입니다.\n");
            Console.WriteLine("[보유골드]");
            Console.WriteLine($"{character.Gold} G\n");
            Console.WriteLine("[아이템 목록]");
        }

        public override Scene Exit()
        {
            return nextScene;
        }
    }
}
