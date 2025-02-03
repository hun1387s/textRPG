using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    internal class EquipmentScene : Scene
    {
        // Singleton
        private EquipmentScene() { }
        private static EquipmentScene? instance;
        public static EquipmentScene GetInst()
        {
            if (instance == null)
                instance = new EquipmentScene();
            return instance;
        }
        List<int> equipIdx;
        Core core = Core.GetInst();

        public override void Enter()
        {
            Clear();
            
            nextScene = this;
            
            equipIdx = new List<int>();

            Console.WriteLine("인벤토리 - 장착관리");
            Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.\n");
            Console.WriteLine("[아이템 목록]\n");
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
                    SetItemToggle(equipIdx[0]);
                    break;

                case "2":
                    // 2번 아이템 장착 토글
                    SetItemToggle(equipIdx[1]);
                    break;

                case "3":
                    // 3번 아이템 장착 토글
                    SetItemToggle(equipIdx[2]);
                    break;

                case "4":
                    // 4번 아이템 장착 토글
                    SetItemToggle(equipIdx[3]);
                    break;

                case "5":
                    // 5번 아이템 장착 토글
                    SetItemToggle(equipIdx[4]);
                    break;

                case "6":
                    // 6번 아이템 장착 토글
                    SetItemToggle(equipIdx[5]);
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
                // 가지고 있는 장비
                if (core.items[i].Own)
                {
                    equipIdx.Add(i);
                    count++;
                    // 장비 장착 유무
                    if (core.items[i].Equip)
                    {
                        equip = "[E]";
                    }

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

                    Console.WriteLine($" -{count} {equip}{core.items[i].Name} | {AorD} | {core.items[i].Description}");
                }
            }
        }

        private void SetItemToggle(int idx)
        {
            bool equip = core.items[idx].Equip ? false : true;

            Character character = Character.GetInst();
            if (equip)
            {
                character.Attack += core.items[idx].Attack;
                character.Defense += core.items[idx].Defense;
            }
            else
            {
                character.Attack -= core.items[idx].Attack;
                character.Defense -= core.items[idx].Defense;
            }

            core.items[idx].Equip = equip;
        }
    }
}
