using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    internal class RestScene : Scene
    {
        // Singleton
        private RestScene() { }
        private static RestScene? instance;
        public static RestScene GetInst()
        {
            if (instance == null)
                instance = new RestScene();
            return instance;
        }
        

        public override void Enter()
        {
            Clear();
            Character character = Character.GetInst();
            nextScene = this;
            Console.WriteLine("휴식하기\n");
            Console.WriteLine($"500G를 내면 체력을 회복 할 수 있습니다. (보유 골드 : {character.Gold} G)\n\n");

            Console.WriteLine("1. 휴식하기");
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
                    Rest();


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

        private void Rest()
        {
            Character character = Character.GetInst();
            if (character.Gold >= 500)
            {
                character.Gold -= 500;
                character.HP = 100;
                Clear();
                Console.Write("휴식 중");
                Thread.Sleep(500);
                Console.Write(".");
                Thread.Sleep(500);
                Console.Write(".");
                Thread.Sleep(500);
                Console.Write(".");
                Thread.Sleep(1500);
            }
            else
            {
                Console.WriteLine("골드가 부족합니다.");
                Thread.Sleep(3000);
            }
        }
    }
}
