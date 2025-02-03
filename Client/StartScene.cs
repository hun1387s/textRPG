using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    internal class StartScene : Scene
    {
        private StartScene() { }
        private static StartScene? instance;
        public static StartScene GetInst()
        {
            if (instance == null)
                instance = new StartScene();
            return instance;
        }

        public override void Enter()
        {
            Clear();
            nextScene = this;
            //Character character = Character.GetInst();

            Console.WriteLine("RPG 마을에 오신 여러분 환영합니다.");
            Console.WriteLine("이곳에서 던전으로 들어가기전 활동을 할 수 있습니다.\n");

            Console.WriteLine("1. 상태보기");
            Console.WriteLine("2. 인벤토리");
            Console.WriteLine("3. 상점");

            string result = Console.ReadLine();

            switch (result)
            {
                case "1":
                    // StatusScene
                    nextScene = StatusScene.GetInst();
                    break;
                case "2":
                    // InventoryScene
                    nextScene = InventoryScene.GetInst();
                    break;
                case "3":
                    // EquipmentScene
                    nextScene = EquipmentScene.GetInst();
                    break;

                default:
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
