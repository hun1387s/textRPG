namespace Client
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Core core = Core.GetInst();
            core.init();

            while (true)
            {
                int result = core.Update();

                if (result == -1)
                    break;
            }
        }
    }
}
