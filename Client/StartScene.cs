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
        // Singleton
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
            Character character = Character.GetInst();

            Console.WriteLine("RPG 마을에 오신 여러분 환영합니다.");
            Console.WriteLine("이곳에서 던전으로 들어가기전 활동을 할 수 있습니다.\n");

            Console.WriteLine("1. 상태보기");
            Console.WriteLine("2. 인벤토리");
            Console.WriteLine("3. 상점");
            Console.WriteLine("4. 던전 들어가기");

            Console.WriteLine("5. 이름변경");


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
                case "5":
                    // 이름 변경
                    Clear();
                    Console.WriteLine("변경할 이름을 입력하세요.");
                    character.Name = Console.ReadLine();

                    Console.WriteLine($"이름이 '{character.Name}'로 변경되었습니다.");
                    Thread.Sleep(1000);
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
