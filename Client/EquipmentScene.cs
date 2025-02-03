using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    internal class EquipmentScene : Scene
    {
        // Singleton
        private EquipmentScene() { }
        private static EquipmentScene? instance;
        public static EquipmentScene GetInst()
        {
            if (instance == null)
                instance = new EquipmentScene();
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
