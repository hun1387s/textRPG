namespace Client
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // core 싱글톤
            Core core = Core.GetInst();
            // core 초기화
            core.Init();

            // 계속 게임 돌아가게하는 반복문
            while (true)
            {
                core.Update();

                // 게임 종료
                if (!core.IsRunning)
                {
                    Console.WriteLine("게임을 종료합니다.");
                    Thread.Sleep(2000);
                    break;
                }
            }
        }
    }
}
