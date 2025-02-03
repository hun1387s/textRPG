using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    internal class InventoryScene : Scene
    {
        private InventoryScene() { }
        private static InventoryScene? instance;
        public static InventoryScene GetInst()
        {
            if (instance == null)
                instance = new InventoryScene();
            return instance;
        }

        public override void Enter()
        {

        }

        public override Scene Exit()
        {
            return nextScene;
        }
    }
}
