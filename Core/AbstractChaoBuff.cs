using System;
using Terraria;
using Terraria.ModLoader;

namespace ChaoGardenMod.Core
{
    [Autoload(false)]
    public class AbstractChaoBuff : ModBuff
    {
        protected string name;
        protected string type;
        protected string tooltip;
        protected Action<string, Player, int> action;

        public override string Name => name;
        public override string Texture => $"ChaoGardenMod/Assets/Eggs/{name.Replace($"{type} ", "")}/{(type != "" ? type : "None")}";

        public AbstractChaoBuff(string name, string type, string tooltip, Action<string, Player, int> action)
        {
            this.name = name;
            this.type = type;
            this.tooltip = tooltip;
            this.action = action;
        }

        public override bool IsLoadingEnabled(Mod mod) => true;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault(name);
            Description.SetDefault(tooltip);
            Main.buffNoTimeDisplay[Type] = true;
            Main.vanityPet[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            action.Invoke(name, player, buffIndex);
        }
    }
}
