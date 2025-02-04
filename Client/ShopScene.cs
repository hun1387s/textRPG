using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    internal class ShopScene : Scene
    {
        // Singleton
        private ShopScene() { }
        private static ShopScene? instance;
        public static ShopScene GetInst()
        {
            if (instance == null)
                instance = new ShopScene();
            return instance;
        }

        public override void Enter()
        {
            Clear();
            nextScene = this;
            Character character = Character.GetInst();

            Console.Write("상점 ");
            Console.WriteLine("   ");
            Console.WriteLine("필요한 아이템을 얻을 수 있는 상점입니다.\n");
            Console.WriteLine("[보유골드]");
            Console.WriteLine($"{character.Gold} G\n");
            Console.WriteLine("[아이템 목록]");

            printItem();

            Console.WriteLine("\n\n1. 아이템 구매");
            Console.WriteLine("2. 아이템 판매\n");
            Console.WriteLine("0. 나가기");

            Console.WriteLine("\n원하시는 행동을 입력해주세요.\n");

            string result = Console.ReadLine();

            switch (result)
            {
                case "0":
                    // StartScene
                    nextScene = StartScene.GetInst();
                    break;
                case "1":
                    // ShopBuyScene
                    nextScene = ShopBuyScene.GetInst();
                    break;
                case "2":
                    // ShopSellScene
                    nextScene = ShopSellScene.GetInst();
                    break;

                default:
                    // 지금 Scene 재반환 및 화면 정리
                    Console.WriteLine("올바른 값을 입력하세요.");
                    Thread.Sleep(1000);
                    break;
            }


        }

        public override Scene Exit()
        {
            return nextScene;
        }

        private void printItem()
        {
            Core core = Core.GetInst();
            // item 출력
            int count = 0;
            for (int i = 0; i < core.items.Count; i++)
            {
                // 장비 장착 옵션
                string equip = "구매완료";
                // 공격력 or 방어력
                string AorD = "";
                
                {
                    // 방어 장비
                    if (core.items[i].Defense > 0)
                    {
                        AorD = $"방어력 +{core.items[i].Defense}";
                    }
                    // 공격 장비
                    else if (core.items[i].Attack > 0)
                    {
                        AorD = $"공격력 +{core.items[i].Attack}";
                    }
                    count++;
                    if (core.items[i].Own)
                    {
                        Console.WriteLine($" -{count} {core.items[i].Name} | {AorD} | {core.items[i].Description} | {equip}");
                    }
                    else
                    {
                        Console.WriteLine($" -{count} {core.items[i].Name} | {AorD} | {core.items[i].Description} | {core.items[i].Gold} G");
                    }


                }
            }
        }
    }
}
