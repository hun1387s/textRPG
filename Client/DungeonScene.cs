using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    internal class DungeonScene : Scene
    {
        // Singleton
        private DungeonScene() { }
        private static DungeonScene? instance;
        public static DungeonScene GetInst()
        {
            if (instance == null)
                instance = new DungeonScene();
            return instance;
        }
        Core core = Core.GetInst();
        Character character = Character.GetInst();

        public override void Enter()
        {
            Clear();
            nextScene = this;
            Console.WriteLine("던전입장\n");
            Console.WriteLine("이곳에서 던전으로 들어가기전 활동을 할 수 있습니다.");

            Console.WriteLine("1. 쉬운 던전     | 방어력 5 이상 권장");
            Console.WriteLine("2. 일반 던전     | 방어력 11 이상 권장");
            Console.WriteLine("3. 어려운 던전    | 방어력 17 이상 권장");

            Console.WriteLine("\n0. 나가기");

            Console.WriteLine("\n원하시는 행동을 입력해주세요.\n");

        }

        public override Scene Exit()
        {
            return nextScene;
        }
    }
}
