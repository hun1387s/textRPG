using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    internal class StatusScene : Scene
    {
        // Singleton
        private StatusScene() { }
        private static StatusScene? instance;
        public static StatusScene GetInst()
        {
            if (instance == null)
                instance = new StatusScene();
            return instance;
        }

        public override void Enter()
        {
            Clear();
            nextScene = this;

            Character character = Character.GetInst();

            Console.WriteLine("캐릭터의 정보가 표시됩니다.\n");

            Console.WriteLine($"Lv. {character.Level.ToString()}");
            Console.WriteLine($"{character.Name} ({character.Job.ToString()})");
            Console.WriteLine($"공격력: {character.Attack.ToString()}");
            Console.WriteLine($"방어력: {character.Defense.ToString()}");
            Console.WriteLine($"체 력: {character.HP.ToString()}");
            Console.WriteLine($"Gold: {character.Gold.ToString()} \n");

            Console.WriteLine("1. 이름변경");
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
