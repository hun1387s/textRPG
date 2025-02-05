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
        
        int[] levelDefense = new int[] { 5, 11, 17 };
        int[] levelReward = new int[] { 1000, 1700, 2500 };
        string[] levelName = new string[] { "쉬운", "일반", "어려운" };

        int prevHP;
        int prevGold;
        public override void Enter()
        {
            Character character = Character.GetInst();
            Clear();
            nextScene = this;

            prevGold = character.Gold;
            prevHP = character.HP;

            Console.WriteLine("던전입장\n");
            Console.WriteLine("이곳에서 던전으로 들어가기전 활동을 할 수 있습니다.");

            Console.WriteLine($"1. 쉬운 던전     | 방어력 {levelDefense[0]} 이상 권장");
            Console.WriteLine($"2. 일반 던전     | 방어력 {levelDefense[1]} 이상 권장");
            Console.WriteLine($"3. 어려운 던전    | 방어력 {levelDefense[2]} 이상 권장");

            Console.WriteLine("\n0. 나가기");

            Console.WriteLine("\n원하시는 행동을 입력해주세요.\n");

            string result = Console.ReadLine();
            switch (result)
            {
                case "0":
                    // StartScene
                    nextScene = StartScene.GetInst();
                    break;
                case "1":
                    // 쉬운 던전
                    TryDungeon(1);
                    break;
                case "2":
                    // 일반 던전
                    TryDungeon(2);
                    break;
                case "3":
                    // 어려운 던전
                    TryDungeon(3);
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

        private void TryDungeon(int dLevel)
        {
            Character character = Character.GetInst();
            int defese = character.Defense;
            Random random = new Random();
            // 캐릭터 방어력이 권장 방어력 보다 낮을 때
            if (defese < levelDefense[dLevel - 1])
            {
                int ranValue = random.Next(1, 10);

                // 40% 확률로 실패
                if (ranValue <= 4)
                {
                    // 체력 반토막 + 보상 없음
                    character.HP /= 2;
                    Clear();
                    Console.WriteLine("던전 클리어 실패.");

                    Console.WriteLine("[탐험 결과]");
                    Console.WriteLine($"체력 {prevHP} -> {character.HP}");

                    Thread.Sleep(2000);
                    return;
                }
            }

            // 캐릭터 방어력이 권장 방어력 보다 높을 때
            // 기본 20~35 감소, (내 방어력 - 권장 방어력) 만큼 랜덤 값에 설정
            int subtractHP = random.Next(20, 36) + (levelDefense[dLevel - 1] - defese);
            character.HP -= subtractHP;

            Reward(dLevel);
        }

        private void Reward(int dLevel)
        {
            Character character = Character.GetInst();
            Random random = new Random();
            //공격력  ~ 공격력 * 2 의 % 만큼 추가 보상 획득 가능
            int ranValue = random.Next((int)character.Attack, (int)character.Attack * 2);

            int resultReward = levelReward[dLevel - 1];
            resultReward += (int)(((float)ranValue / 100) * levelReward[dLevel - 1]);

            character.Gold += resultReward;
            character.DGtry++;
            character.LevelCheck();

            Clear();

            Console.WriteLine("던전 클리어\n");
            Console.WriteLine($"축하합니다!!\n{levelName[dLevel - 1]} 던전을 클리어 하였습니다.\n");

            Console.WriteLine("[탐험 결과]");
            Console.WriteLine($"체력 {prevHP} -> {character.HP}");
            Console.WriteLine($"Gold {prevGold} G-> {character.Gold}");

            Console.WriteLine("\n0. 나가기");

            Console.WriteLine("\n원하시는 행동을 입력해주세요.\n");

            string result = Console.ReadLine();
            switch (result)
            {
                case "0":
                    // 나가기
                    break;
                default:
                    // 지금 Scene 재반환 및 화면 정리
                    Console.WriteLine("올바른 값을 입력하세요.");
                    Thread.Sleep(1000);
                    break;
            }
        }
    }
}
