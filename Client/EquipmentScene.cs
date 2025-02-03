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

        public override void Enter()
        {
            Clear();
            nextScene = this;

            Console.WriteLine("인벤토리 - 장착관리");
            Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.\n");
            Console.WriteLine("[아이템 목록]\n");


            Console.WriteLine("0. 나가기");

            string result = Console.ReadLine();

            switch (result)
            {
                case "0":
                    // InventoryScene
                    nextScene = InventoryScene.GetInst();
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
    }
}
