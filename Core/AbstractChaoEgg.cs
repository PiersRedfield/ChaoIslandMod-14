using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ChaoGardenMod.Core
{
    [Autoload(false)]
    public class AbstractChaoEgg : ModItem
    {
        protected string name;
        protected string type;
        protected string tooltip;
        protected string buffTooltip;
        protected ChaoType chaoType;
        protected Action<string, Player, int> buffAction;

        public override string Name => name;
        public override string Texture => $"ChaoGardenMod/Assets/Eggs/{name.Replace($"{type} ", "")}/{(type != "" ? type : "None")}";

        public AbstractChaoEgg(string name, string type, string tooltip, string buffTooltip, ChaoType chaoType, Action<string, Player, int> buffAction)
        {
            this.name = name;
            this.type = type;
            this.tooltip = tooltip;
            this.buffTooltip = buffTooltip;
            this.chaoType = chaoType;
            this.buffAction = buffAction;
        }

        public override void SetStaticDefaults()
        {
            string tooltip = this.tooltip;
            if (chaoType == ChaoType.UniqueSupport)
            {
                tooltip += "\nThis is a [c/fafd20:Unique] [c/b61239:Support] Chao!";
            }

            DisplayName.SetDefault(name);
            Tooltip.SetDefault(tooltip);
        }

        public override bool IsLoadingEnabled(Mod mod)
        {
            mod.AddContent(new AbstractChaoBuff(name, type, buffTooltip, buffAction));
            mod.AddContent(new AbstractChaoProj(name, type));
            return true;
        }

        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 36;
            Item.useTime = 20;
            Item.useStyle = ItemUseStyleID.HoldUp;
            Item.useAnimation = 1;
            Item.UseSound = SoundID.Item1;
            Item.shoot = Mod.Find<ModProjectile>(name).Type;
            Item.buffType = Mod.Find<ModBuff>(name).Type;
            Item.rare = ItemRarityID.Yellow;
            Item.noMelee = true;
            Item.accessory = true;
        }
    }
}
