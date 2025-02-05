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
            Core core = Core.GetInst();


            Console.WriteLine("RPG 마을에 오신 여러분 환영합니다.");
            Console.WriteLine("이곳에서 던전으로 들어가기전 활동을 할 수 있습니다.\n");

            Console.WriteLine("1. 상태보기");
            Console.WriteLine("2. 인벤토리");
            Console.WriteLine("3. 상점");
            Console.WriteLine("4. 던전 들어가기");
            Console.WriteLine("5. 휴식");

            Console.WriteLine("\n\n6. 저장하기");
            Console.WriteLine("7. 불러오기");


            Console.WriteLine("\n\n0. 게임 종료");

            Console.WriteLine("\n원하시는 행동을 입력해주세요.\n");


            string result = Console.ReadLine();

            switch (result)
            {
                case "0":
                    Clear();
                    // 게임 종료
                    Core.GetInst().IsRunning = false;
                    break;
                case "1":
                    // StatusScene
                    nextScene = StatusScene.GetInst();
                    break;
                case "2":
                    // InventoryScene
                    nextScene = InventoryScene.GetInst();
                    break;
                case "3":
                    // ShopScene
                    nextScene = ShopScene.GetInst();
                    break;
                case "4":
                    // DungeonScene
                    nextScene = DungeonScene.GetInst();
                    break;
                case "5":
                    // RestScene
                    nextScene = RestScene.GetInst();
                    break;

                case "6":
                    // 저장하기
                    core.SaveGame();
                    Thread.Sleep(1000);
                    break;

                case "7":
                    // 불러오기
                    core.LoadGame();
                    Thread.Sleep(1000);
                    break;


                case "debug":
                    // DebugMode
                    DebugMode();
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

        


        private void DebugMode()
        {
            Character character = Character.GetInst();
            bool debugRun = true;
            while (debugRun)
            {
                Clear();
                Console.WriteLine($"보유 골드 : {character.Gold} G");
                Console.WriteLine($"레벨 : lv.{character.Level}");
                string result = Console.ReadLine();

                switch (result)
                {
                    case "money":
                        character.Gold += 10000;
                        break;
                    case "level":
                        character.Level += 1;
                        break;
                    default:
                        debugRun = false;
                        break;     
                }
            }
        }


    }
}
