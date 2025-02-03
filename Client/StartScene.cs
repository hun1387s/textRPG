using System;
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
            //Character character = Character.GetInst();

            Console.WriteLine("RPG 마을에 오신 여러분 환영합니다.");
            Console.WriteLine("이곳에서 던전으로 들어가기전 활동을 할 수 있습니다.\n");

            Console.WriteLine("1. 상태보기");
            Console.WriteLine("2. 인벤토리");
            Console.WriteLine("3. 상점");

            int result = int.Parse(Console.ReadLine());

            if (result == 1)
            { }
        }

        public override void Exit()
        {

        }
    }
}
