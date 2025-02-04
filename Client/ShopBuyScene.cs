using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    internal class ShopBuyScene : Scene
    {
        // Singleton
        private ShopBuyScene() { }
        private static ShopBuyScene? instance;
        public static ShopBuyScene GetInst()
        {
            if (instance == null)
                instance = new ShopBuyScene();
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
            Console.WriteLine("- 구매");
            Console.WriteLine("필요한 아이템을 얻을 수 있는 상점입니다.\n");
            Console.WriteLine("[보유골드]");
            Console.WriteLine($"{character.Gold} G\n");
            Console.WriteLine("[아이템 목록]");

            printItem();


            Console.WriteLine("\n\n0. 나가기");

            string result = Console.ReadLine();

            switch (result)
            {
                case "0":
                    // InventoryScene
                    nextScene = InventoryScene.GetInst();
                    break;

                case "1":
                    // 1번 아이템 장착 토글
                    BuyItem(1);
                    break;

                case "2":
                    // 2번 아이템 장착 토글
                    BuyItem(2);
                    break;

                case "3":
                    // 3번 아이템 장착 토글
                    BuyItem(3);
                    break;

                case "4":
                    // 4번 아이템 장착 토글
                    BuyItem(4);
                    break;

                case "5":
                    // 5번 아이템 장착 토글
                    BuyItem(5);
                    break;

                case "6":
                    // 6번 아이템 장착 토글
                    BuyItem(6);
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
        private void BuyItem(int idx)
        {
            // 구매 불가
            if (core.items[idx - 1].Own)
            {
                Console.WriteLine("이미 구매한 아이템입니다.");
                Thread.Sleep(1000);
            }
            // 구매 가능
            else
            {
                // Gold 충분
                if (core.items[idx - 1].Gold >= character.Gold)
                {
                    core.items[idx - 1].Own = true;
                    Console.WriteLine("구매를 완료했습니다.");
                    Thread.Sleep(1000);
                }
                // Gold 부족
                else
                {
                    Console.WriteLine("Gold가 부족합니다.");
                    Thread.Sleep(1000);
                }
            }
        }

        private void printItem()
        {
            
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
                    // 장비 소유 유무
                    if (core.items[i].Own)
                    {
                        Console.WriteLine($" -{count} {core.items[i].Name} | {AorD} | {equip}");
                    }
                    else
                    {
                        Console.WriteLine($" -{count} {core.items[i].Name} | {AorD} | {core.items[i].Description}");
                    }


                }
            }
        }
    }
        
}
