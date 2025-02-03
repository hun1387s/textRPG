using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    internal class InventoryScene : Scene
    {
        // Singleton
        private InventoryScene() { }
        private static InventoryScene? instance;
        public static InventoryScene GetInst()
        {
            if (instance == null)
                instance = new InventoryScene();
            return instance;
        }

        public override void Enter()
        {
            Clear();
            nextScene = this;

            Console.WriteLine("인벤토리");
            Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.\n");
            Console.WriteLine("[아이템 목록]\n");
            printItem();

            Console.WriteLine("\n\n1. 장착 관리");
            Console.WriteLine("0. 나가기");


            string result = Console.ReadLine();

            switch (result)
            {
                case "0":
                    // StartScene
                    nextScene = StartScene.GetInst();
                    break;
                case "1":
                    // EquipmentScene
                    nextScene = EquipmentScene.GetInst();
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
                string equip = "   ";
                // 공격력 or 방어력
                string AorD = "";
                if (core.items[i].Own)
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
                    // 장비 장착 유무
                    if (core.items[i].Equip)
                    {
                        count++;
                        equip = "[E]";
                        Console.WriteLine($" -{count} {equip}{core.items[i].Name} | {AorD} | {core.items[i].Description}");
                    }

                    
                }
            }
            
        }
    }
}
