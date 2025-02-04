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
        Core core = Core.GetInst();
        Character character = Character.GetInst();

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

            printItem();


            Console.WriteLine("\n\n0. 나가기");

            Console.WriteLine("\n원하시는 행동을 입력해주세요.\n");

            string result = Console.ReadLine();

            switch (result)
            {
                case "0":
                    // ShopScene
                    nextScene = ShopScene.GetInst();
                    break;

                case "1":
                    // 1번 아이템 판매 시도
                    SellItem(1);
                    break;

                case "2":
                    // 2번 아이템 판매 시도
                    SellItem(2);
                    break;

                case "3":
                    // 3번 아이템 판매 시도
                    SellItem(3);
                    break;

                case "4":
                    // 4번 아이템 판매 시도
                    SellItem(4);
                    break;

                case "5":
                    // 5번 아이템 판매 시도
                    SellItem(5);
                    break;

                case "6":
                    // 6번 아이템 판매 시도
                    SellItem(6);
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
        private void SellItem(int idx)
        {
            // 판매 불가
            if (!core.items[idx - 1].Own)
            {
                Console.WriteLine("소유하지 않은 아이템입니다.");
                Thread.Sleep(1000);
            }
            // 판매 가능
            else
            {
                core.items[idx - 1].Own = false;
                core.items[idx - 1].Equip = false;
                character.Gold += (int)((float)core.items[idx - 1].Gold * 0.85f);
                Console.WriteLine("판매를 완료했습니다.");
                Thread.Sleep(1000);
            }
        }

        private void printItem()
        {

            // item 출력
            int count = 0;
            for (int i = 0; i < core.items.Count; i++)
            {
                // 장비 장착 옵션
                string equip = "미소유";
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
                    // 장비 소유 유무

                    if (!core.items[i].Own)
                    {
                        Console.WriteLine($" -{count} {core.items[i].Name} | {AorD} | {core.items[i].Description} | {equip}");
                    }
                    else
                    {
                        Console.WriteLine($" -{count} {core.items[i].Name} | {AorD} | {core.items[i].Description} | {(int)((float)core.items[i].Gold * 0.85f)} G");
                    }

                }
            }
        }
    }
}
