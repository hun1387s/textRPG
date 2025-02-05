using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Threading.Tasks;

namespace Client
{
    internal class Core
    {
        // Singleton
        private Core() { }
        private static Core? instance;
        public static Core GetInst()
        {
            if (instance == null)
                instance = new Core();
            return instance;
        }

        private bool isRun = true;
        public bool IsRunning
        {
            get { return isRun; }
            set { isRun = value; }
        }


        // Item을 담을 리스트
        public List<Item> items = new List<Item>();

        // character 싱글톤
        Character character = Character.GetInst();

        // currentScene - 시작화면 싱글톤
        Scene currentScene = StartScene.GetInst();

        // 계속 반복되는 메서드
        public void Update()
        {
            // Scene 진입
            currentScene.Enter();
            
            // 다음 Scene 지정
            Scene nextScene = currentScene.Exit();
            currentScene = nextScene;

            // 게임 종료시 저장
            if (!isRun)
            {
                SaveGame();
            }
        }

        public void Init()
        {
            character = Character.GetInst();
            itemInit();
        }

        // 프로젝트 경로
        static string projectDir = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;

        // 게임 저장 메서드
        public void SaveGame()
        {
            try { SaveCharacter(character); }
            catch { Failed("캐릭터 저장"); }

            try { SaveItems(items); }
            catch { Failed("아이템 저장"); }
        }
        void SaveCharacter(Character _char)
        {
            // 캐릭터 저장
            string filePath = Path.Combine(projectDir, "data", "character.json");
            string jsn = JsonSerializer.Serialize(_char
                        , new JsonSerializerOptions { WriteIndented = true, Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping });
            File.WriteAllText(filePath, jsn);
            Console.WriteLine("캐릭터 저장 완료!");
            Console.WriteLine(filePath);
            // Thread.Sleep(2000);
        }
        void SaveItems(List<Item> _items)
        {
            // 아이템 저장
            string filePath = Path.Combine(projectDir, "data", "items.json");
            string json = JsonSerializer.Serialize(_items
                , new JsonSerializerOptions { WriteIndented = true, Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping });
            File.WriteAllText(filePath, json);
            Console.WriteLine("아이템 저장 완료!");
            Console.WriteLine(filePath);
        }
        void Failed(string str)
        {
            Console.WriteLine($"{str} 실패");
        }

        // 게임 불러오기
        public void LoadGame()
        {
            try
            {
                LoadCharacter(character);
            }
            catch { Failed("캐릭터 불러오기"); }

            try
            {
                LoadItems(items);
            }
            catch { Failed("아이템 불러오기"); }
        }
        void LoadCharacter(Character _char)
        {   
            // 캐릭터 로드
            string filePath = Path.Combine(projectDir, "data", "character.json");
            string jsn = File.ReadAllText(filePath);

            Dictionary<string, JsonElement> jsonData = JsonSerializer.Deserialize<Dictionary<string, JsonElement>>(jsn);

            _char.Name = jsonData["Name"].GetString();
            _char.Level = jsonData["Level"].GetInt32();
            _char.DGtry = jsonData["DGtry"].GetInt32();
            _char.Job = jsonData["Job"].GetString();
            _char.Attack = jsonData["Attack"].GetSingle();
            _char.Defense = jsonData["Defense"].GetInt32();
            _char.HP = jsonData["HP"].GetInt32();
            _char.Gold = jsonData["Gold"].GetInt32();
            _char.Armor = JsonSerializer.Deserialize<Item>(jsonData["Armor"].GetRawText());
            _char.Weapon = JsonSerializer.Deserialize<Item>(jsonData["Weapon"].GetRawText());

            Console.WriteLine("캐릭터 불러오기 완료!");
            Console.WriteLine(filePath);
        }
        void LoadItems(List<Item> _items)
        {
            // 아이템 로드
            string filePath = Path.Combine(projectDir, "data", "items.json");
            string jsn = File.ReadAllText(filePath);

            List<Item> loadItems = new List<Item>();
            loadItems =  JsonSerializer.Deserialize<List<Item>>(jsn);

            _items = loadItems;
            Console.WriteLine("아이템 불러오기 완료!");
            Console.WriteLine(filePath);
        }


        // 아이템 초기화
        private void itemInit()
        {
            items.Add(new Item()
            {
                Name = "수련자 갑옷",
                Defense = 5,
                Description = "수련에 도움을 주는 갑옷입니다.",
                Gold = 1000,
                Equip = false,
                Own = false,
                ItemTYPE = ITEMTYPE.Armor
            });

            items.Add(new Item()
            {
                Name = "무쇠 갑옷",
                Defense = 9,
                Description = "무쇠로 만들어져 튼튼한 갑옷입니다.",
                Gold = 2000,
                Equip = false,
                Own = true,
                ItemTYPE = ITEMTYPE.Armor
            });

            items.Add(new Item()
            {
                Name = "스파르타의 갑옷",
                Defense = 15,
                Description = "스파르타의 전사들이 사용했다는 전설의 갑옷입니다.",
                Gold = 3500,
                Equip = false,
                Own = false,
                ItemTYPE = ITEMTYPE.Armor
            });

            items.Add(new Item()
            {
                Name = "낡은 검",
                Attack = 2,
                Description = "쉽게 볼 수 있는 낡은 검 입니다.",
                Gold = 600,
                Equip = false,
                Own = true,
                ItemTYPE = ITEMTYPE.Weapon
            });

            items.Add(new Item()
            {
                Name = "청동 도끼",
                Attack = 5,
                Description = "어디선가 사용됐던거 같은 도끼입니다.",
                Gold = 1500,
                Equip = false,
                Own = false,
                ItemTYPE = ITEMTYPE.Weapon
            });

            items.Add(new Item()
            {
                Name = "스파르타의 창",
                Attack = 7,
                Description = "스파르타의 전사들이 사용했다는 전설의 창입니다.",
                Gold = 3000,
                Equip = false,
                Own = true,
                ItemTYPE = ITEMTYPE.Weapon
            });

            items.Add(new Item()
            {
                Name = "운영자의 스테인리스 빨대",
                Attack = 3000,
                Description = "운영자가 사용하다가 던져놓은 빨대입니다.",
                Gold = 50000,
                Equip = false,
                Own = false,
                ItemTYPE = ITEMTYPE.Weapon
            });

            items.Add(new Item()
            {
                Name = "운영자의 목베게",
                Defense = 3000,
                Description = "운영자가 애용하는 목베게입니다.",
                Gold = 50000,
                Equip = false,
                Own = false,
                ItemTYPE = ITEMTYPE.Armor
            });
        }
    }
}
