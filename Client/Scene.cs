using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    internal abstract class Scene : Object
    {
        public void Clear()
        {
            Console.Clear();
        }

        //public abstract void Init();
        public abstract void Enter();
        public abstract void Exit();

    }
}
