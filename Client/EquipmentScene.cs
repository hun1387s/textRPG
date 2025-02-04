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
        Character character = Character.GetInst();

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

            Console.WriteLine("\n원하시는 행동을 입력해주세요.\n");

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
            Item item = core.items[idx];
            // 아이템 장착 토글
            bool equip = item.Equip ? false : true;

            Item prevItem;
            // 아이템 타입별 한종류만 장착
            if (item.ItemTYPE == ITEMTYPE.Weapon)
            {
                // 무기일 때
                prevItem = character.Weapon;
                if (prevItem != null)
                {
                    // 이전 장비 해제
                    MountItem(prevItem, false);
                    character.Weapon = null;
                }

            }
            else if (item.ItemTYPE == ITEMTYPE.Armor)
            {
                // 갑옷일 때
                prevItem = character.Armor;
                if (prevItem != null)
                {
                    // 이전 장비 해제
                    MountItem(prevItem, false);
                    character.Armor = null;
                }
            }

            MountItem(item, equip);
        }

        private void MountItem(Item item, bool equip)
        {
            // 아이템 성능 적용

            if (equip)
            {
                character.Attack += item.Attack;
                character.Defense += item.Defense;
            }
            else
            {
                character.Attack -= item.Attack;
                character.Defense -= item.Defense;
            }

            item.Equip = equip;

            if (item.ItemTYPE == ITEMTYPE.Weapon)
                character.Weapon = item;
            if (item.ItemTYPE == ITEMTYPE.Armor)
                character.Armor = item;
        }
    }
}
