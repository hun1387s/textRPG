using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    internal class StatusScene : Scene
    {
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
            Console.WriteLine($"{character.Name} ({character.Level.ToString()})");
            Console.WriteLine($"공격력: {character.Attack.ToString()}");
            Console.WriteLine($"방어력: {character.Defense.ToString()}");
            Console.WriteLine($"체 력: {character.HP.ToString()}");
            Console.WriteLine($"Gold: {character.Gold.ToString()} \n");
            Console.WriteLine("0. 나가기");

            string result = Console.ReadLine();

            switch (result)
            {
                case "0":
                    // StartScene
                    nextScene = StartScene.GetInst();
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
