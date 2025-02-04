using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    internal class DungeonClearSceneScene : Scene
    {
        // Singleton
        private DungeonClearSceneScene() { }
        private static DungeonClearSceneScene? instance;
        public static DungeonClearSceneScene GetInst()
        {
            if (instance == null)
                instance = new DungeonClearSceneScene();
            return instance;
        }
        Core core = Core.GetInst();
        Character character = Character.GetInst();

        public override void Enter()
        {
            Clear();
            nextScene = this;

        }

        public override Scene Exit()
        {
            return nextScene;
        }
    
    }
}
