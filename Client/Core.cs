using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    internal class Core
    {
        private Core() { }
        private static Core instance;

        public static Core GetInst()
        {
            if (instance == null)
                instance = new Core();
            return instance;
        }

        public int Update()
        {
            if (Console.ReadLine() == "Exit")
            {
                Console.WriteLine("Exit 명령어로 종료합니다.");
                return -1;
            }
            return 0;
        }
    }
}
