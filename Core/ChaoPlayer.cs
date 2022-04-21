using System.Collections.Generic;
using Terraria.ModLoader;

namespace ChaoGardenMod.Core
{
    public class ChaoPlayer : ModPlayer
    {
        public Dictionary<string, bool> directory;

        public override void Load()
        {
            directory = ChaoGardenMod.allChaosPairs;
        }

        public override void ResetEffects()
        {
            foreach (string value in ChaoGardenMod.allChaos)
            {
                if (directory.ContainsKey(value))
                    directory[value] = false;
            }
        }

        public override void Unload()
        {
            directory = null;
        }
    }
}
