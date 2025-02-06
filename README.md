# 개인 프로젝트 "TextRPG"

## 프로젝트 소개
이번에는 개인 프로젝트로 TextRPG를 구현해 보는 것이다.

게임이 진행되는 과정들을 하나하나 느껴볼 수 있었던 프로젝트다.

C#과 친해질 수 있는 경험이 되었다.



# WireFrame

![image-20250203214434883](/assets/image-20250203214434883.png)

> 전체 구현할 Scene들의 화면과 작동 요소이다.



# Class 구조

![image-20250204200047141](/assets/image-20250204200047141.png)

아직 게임을 제작할 때 어떤 구조로 작성되는지는 모른다.

하지만 unity를 잠깐 해봤을때, GameObject로부터 다양하게 파생되는 느낌을 받았다.

그래서 나도 그 구조를 따라해보고자, 위와 같은 구조를 만들어봤다.


# 주요 기능 구현 방식

### Object

- 제일 상위 클래스다.
- 이름과, ID값을 가진다.
  (아직 ID의 역할은 정확히는 모르지만, 나중 관리를 위해 필요하겠지)



### Item 

- 캐릭터의 status에 영향을 미치는 장비들을 다루는 클래스다.
- 인벤토리와 장비관리, 상점의 가장 중요한 요소일 듯싶다.
- 이름, 방어력, 공격력, 설명, 가치, 장착유무, 소유여부를 다룬다.



### Character

- 캐릭터의 status를 다룬다.
- 이름, 레벨, 직업, 공격력, 방어력, 체력, 소유화폐 등을 가진다.



### Scene

- 여러 Scene에 공통적으로 사용될 메서드와 필드를 미리 정해 놓는 상위 클래스다.
  - 화면을 정리해 주는 `Clear` 메서드
  - 화면을 구성해 주는 `Enter` 메서드
  - 다음 Scene을 지정해 주는 `Exit` 메서드
  - 다음 Scene이 담길 `nextScene` 필드



### Core

[Core.cs](https://github.com/hun1387s/textRPG/blob/main/Client/Core.cs)

- 게임이 구동 되는 주축 클래스이다.

``` c#
private bool isRun = true;
public bool IsRunning
{
    get { return isRun; }
    set { isRun = value; }
}
```

- `IsRunning`을 통해 내부에서 게임을 종료 할 수단 제공.



``` c#
public void init()
{
    character = Character.GetInst();
    itemInit();
}
```

- 각종 Item들을 초기화한다.



``` c#
// 계속 반복되는 메서드
public void Update()
{
    // Scene 진입
    currentScene.Enter();

    // 다음 Scene 지정
    Scene nextScene = currentScene.Exit();
    currentScene = nextScene;
}
```

- 간단한 게임 구조이다
- `currentScene.Enter()`를 통해 Scene에 진입
- `currentScene.Exit()`을 통해 다음 Scene 반환
- `Update()` 메서드 재 실행으로 계속 반복



### StartScene

![image-20250204164904307](/assets/image-20250204164904307.png)

[StartScene.cs](https://github.com/hun1387s/textRPG/blob/main/Client/StartScene.cs)

#### Singleton

세부 Scene들은 Singleton으로 구현해 서로 땡겨다 쓸수 있도록 작성 했다.

``` c#
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
    	// 싱글톤 객체 사용
        Character character = Character.GetInst();
}
```

- Character 객체도 같은 방식으로 구성해서 위와 같은 방법으로 가져다 사용한다.
- 생성자를 `private`으로 만들어, 외부에서 생성 할 수 없도록 구현.
- `instance` 필드에는 자기 자신 객체를 가르킨다.



#### Enter()

``` c#
public override void Enter()
{
    Clear();
    nextScene = this;


    Console.WriteLine("RPG 마을에 오신 여러분 환영합니다.");
    Console.WriteLine("이곳에서 던전으로 들어가기전 활동을 할 수 있습니다.\n");

    Console.WriteLine("1. 상태보기");
    Console.WriteLine("2. 인벤토리");
    Console.WriteLine("3. 상점");
    Console.WriteLine("4. 던전 들어가기");
    Console.WriteLine("5. 휴식");


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
```

- 이번 게임은 반응형 게임으로 제작해, 위가 기본 틀이다.
- 각 Scene에 접근하기 위해 `Enter()`메서드를 활용한다.
- `switch-case`를 활용해 각각의 다른 Scene으로 이동한다.

#### Exit()

``` c#
public override Scene Exit()
        {
            return nextScene;
        }
```

- `nextScene`필드는 상위 객체인 `Scene`객체로부터 상속 받은 필드이다.
- 다음 Scene을 지정해 반환해 준다.



### StatusScene

![image-20250204172848576](/assets/image-20250204172848576.png)

[StatusScene.cs](https://github.com/hun1387s/textRPG/blob/main/Client/StatusScene.cs)

``` csharp
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
```

- 해당 Scene에서 이름을 변경 할 수 있도록 제작.
- `Thread.Sleep()`으로 화면이 초기화 되는 것에 딜레이 제공.



### InventoryScene

![image-20250204173040131](/assets/image-20250204173040131.png)



#### printItem()

``` csharp
private void printItem()
{
    Core core = Core.GetInst();
    // item 출력
    int count = 0;
    for (int i = 0; i < core.items.Count; i++)
    {
        // 장비 장착 옵션
        string equip = "   ";
        // 공격력 or 방어력
        string AorD = "";
        if (core.items[i].Own)
        {
            // 방어 장비
            if (core.items[i].Defense > 0)
            {
                AorD = $"방어력 +{core.items[i].Defense}";
            }
            // 공격 장비
            else if (core.items[i].Attack > 0)
            {
                AorD = $"공격력 +{core.items[i].Attack}";
            }
            // 장비 장착 유무
            if (core.items[i].Equip)
            {
                count++;
                equip = "[E]";
                Console.WriteLine($" -{equip}{core.items[i].Name} | {AorD} | {core.items[i].Description}");
            }


        }
    }
}
```

- 장비 장착 유무를 통해 아이템을 출력한다.



### EquipmentScene

[EquipmentScene.cs](https://github.com/hun1387s/textRPG/blob/main/Client/EquipmentScene.cs)

![image-20250204173327359](/assets/image-20250204173327359.png)

#### SetItemToggle

``` c#
private void SetItemToggle(int idx)
{
    Item item = core.items[idx];
    // 아이템 장착 토글
    bool equip = item.Equip ? false : true;

    Item prevItem;
    // 아이템 타입별 한종류만 장착
    if (item.ItemTYPE == ITEMTYPE.Weapon)
    {
        // 무기일 때
        prevItem = character.Weapon;
        if (prevItem != null)
        {
            // 이전 장비 해제
            MountItem(prevItem, false);
            character.Weapon = null;
        }

    }
    else if (item.ItemTYPE == ITEMTYPE.Armor)
    {
        // 갑옷일 때
        prevItem = character.Armor;
        if (prevItem != null)
        {
            // 이전 장비 해제
            MountItem(prevItem, false);
            character.Armor = null;
        }
    }

    MountItem(item, equip);
}
```

- 아이템을 `idx`로 받아, 장착 또는 해제 하는 메서드이다.
- `ITEMTYPE.Weapon`을 통해 무기, 방어구를 구분한다.
- `character.Weapon` 또는 `character.Armor`를 통해 중복 착용을 막는다.



#### MountItem

```c#
public void MountItem(Item item, bool equip)
{
    // 아이템 성능 적용
    if (equip)
    {
        character.Attack += item.Attack;
        character.Defense += item.Defense;
    }
    else
    {
        character.Attack -= item.Attack;
        character.Defense -= item.Defense;
    }

    item.Equip = equip;

    if (item.ItemTYPE == ITEMTYPE.Weapon)
        character.Weapon = item;
    if (item.ItemTYPE == ITEMTYPE.Armor)
        character.Armor = item;
}
```

- 장착 또는 해제의 경우에 아이템의 성능을 `characer`에 적용 시키는 메서드다.
- 장착 때는 `+`, 해제 때는`-`
- `character.Weapon`, 와 `character.Armor`에 착용 중인 무기와 방어구를 지정한다.



### Item

[Item.cs](https://github.com/hun1387s/textRPG/blob/main/Client/Item.cs)

- 아이템 관리를 위해 만든 객체
- 아이템들의 여러 요소들을 가진다.

```c#
internal class Item : Object
{
    public string Name
    {
        get { return name; }
        set { name = value; }
    }
    public int Defense { get; set; }
    public int Attack { get; set; }
    public string? Description { get; set; }
    public int Gold { get; set; }
    public bool Equip { get; set; }
    public bool Own {  get; set; }
    public ITEMTYPE ItemTYPE { get; set; }
}
```



#### ITEMTYPE

- 개인 프로젝트를 하며, 이제껏 사용해 보지 못한 기능들을 사용해보기 위해, 사용해본 enum이다.
- 무기, 방패 등 아이템 타입을 지정 할 enum 값들을 저장한다.

``` c#
enum ITEMTYPE
{
    None,
    Weapon,
    Armor,
}
```


### ShopScene

[ShopScene.cs](https://github.com/hun1387s/textRPG/blob/main/Client/ShopScene.cs)

![image-20250204192309701](/assets/image-20250204192309701.png)

- 아이템을 구매 또는 판매 할 수 있는 공간으로 넘어가기 위한 Scene
- 이미 소지한 아이템은 가격 표시 제한



### ShopBuyScene

[ShopBuyScene.cs](https://github.com/hun1387s/textRPG/blob/main/Client/ShopBuyScene.cs)

![image-20250204192435535](/assets/image-20250204192435535.png)

- 이미 가지고 있는 아이템은 중복 구매를 막는 기능 (가격 표시 제한 및 구매 제한 기능)



#### BuyItem

- 구매 시도 시 몇 가지 분기를 거쳐 구매를 진행 한다.
  - 이미 소지한 아이템인지
  - 보유 골드가 충분 한지

``` c#
private void BuyItem(int idx)
{
    // 구매 불가
    if (core.items[idx - 1].Own)
    {
        Console.WriteLine("이미 구매한 아이템입니다.");
        Thread.Sleep(1000);
    }
    // 구매 가능
    else
    {
        // Gold 충분
        if (core.items[idx - 1].Gold <= character.Gold)
        {
            core.items[idx - 1].Own = true;
            character.Gold -= core.items[idx - 1].Gold;
            Console.WriteLine("구매를 완료했습니다.");
            Thread.Sleep(1000);
        }
        // Gold 부족
        else
        {
            Console.WriteLine("Gold가 부족합니다.");
            Thread.Sleep(1000);
        }
    }
}
```





### ShopSellScene

[ShopSellScene.cs](https://github.com/hun1387s/textRPG/blob/main/Client/ShopSellScene.cs)

![image-20250204193640889](/assets/image-20250204193640889.png)

- 소지한 아이템을 판매 하는 기능
- 미소지 아이템은 미소유 표기
- 판매 가격은 구매 가격 대비 85%



#### SellItem

``` c#
private void SellItem(int idx)
{
    EquipmentScene equipmentScene = EquipmentScene.GetInst();
    Item item = core.items[idx - 1];
    // 판매 불가
    if (!item.Own)
    {
        Console.WriteLine("소유하지 않은 아이템입니다.");
        Thread.Sleep(1000);
    }
    // 판매 가능
    else
    {
        // 장착 중이라면 장착 해제
        if (item.Equip)
        {
            equipmentScene.MountItem(item, false);
        }
        item.Own = false;
        character.Gold += (int)((float)item.Gold * 0.85f);
        Console.WriteLine("판매를 완료했습니다.");
        Thread.Sleep(1000);
    }
}
```

- 장착 중인 아이템 판매 시도 시에, 장착 해제 후 판매 기능



### RestScene

[RestScene.cs](https://github.com/hun1387s/textRPG/blob/main/Client/RestScene.cs)

![image-20250204194003279](/assets/image-20250204194003279.png)

![image-20250204194014489](/assets/image-20250204194014489.png)

- 다른 Scene 들에 비해 많이 간단하지만, 새로운 Scene 구성을 안하고 하나의 Scene에서 구성 해본 Scene이다.



### DungeonScene

[DungeonScene.cs](https://github.com/hun1387s/textRPG/blob/main/Client/DungeonScene.cs)

![image-20250204194804655](/assets/image-20250204194804655.png)

- 난이도 별 구현

#### TryDungeon

``` c#
private void TryDungeon(int dLevel)
{
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
```

- 40% 확률로 실패.
- 실패시 보상 없음
- 실패시 체력 반토막
- 성공시 체력 감소 알고리듬



#### Reward

``` c#
private void Reward(int dLevel)
{
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
```

- 성공시 보수 지급을 위한 메서드
- 공격력에 따른 추가 보상 알고리듬



### Character

[Character.cs](https://github.com/hun1387s/textRPG/blob/main/Client/Character.cs)

- 캐릭터의 여러 속성을 설정 및 반환 가능한 클래스



#### LevelCheck

```csharp
// 레벨 업 구간인지 체크
public void LevelCheck()
{
    for (int i = 0; i < Level; i++)
    {
        if (level == i && dgTry == i)
        {
            level++;
            dgTry = 0;
            attack += 0.5f;
            defense += 1;
        }
    }
}
```

- `dgTry`는 던전을 탐험 한 횟수를 기록하며, 레벨업 후에 0으로 초기화 된다.
- 각 레벨과 동일한 횟수를 던전 탐험에 성공 시, 레벨 업 하는 구조
