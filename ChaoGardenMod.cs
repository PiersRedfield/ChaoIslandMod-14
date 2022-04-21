using ChaoGardenMod.Core;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;

namespace ChaoGardenMod
{
	public class ChaoGardenMod : Mod
	{
		public static List<string> allChaos = new();
		public static Dictionary<string, bool> allChaosPairs = new();

		public override void Load()
		{
			for (int i = 0; i < 22; i++)
			{
				string[] types = new string[22]
				{
					"",
					"Black",
					"Blue",
					"Brown",
					"Cadet Blue",
					"Cyan",
					"Gray",
					"Green",
					"Lime Green",
					"Orange",
					"Pink",
					"Purple",
					"Red",
					"Sky Blue",
					"White",
					"Yellow",
					"Colorless",
					"Lunar",
					"Nebula",
					"Solar",
					"Stardust",
					"Vortex"
				};

				AddContent(new AbstractChaoEgg($"{(types[i] != "" ? types[i] + " " : "")}Axolotl",
					types[i],
					$"Summons an adorable {types[i]} Axolotl Chao to play with you and help you regenerate!",
					$"Your adorable {types[i]} Axolotl Chao is swimming around you and granting you buffs!" +
					"\nIncreases your Bait Power by 10% and grants you gills while in water" +
					"\nIncreases your Life Regen and Mana Regen by 10, doubled while in water" +
					"\nAxolotl Chao hate going into hot biomes and will start to dry out if they go there!",
					ChaoType.UniqueSupport,
					new Action<string, Player, int>((summon, player, buffIndex) =>
					{
						player.lifeRegen += 10;
						player.manaRegen += 10;
						player.fishingSkill += 10;
						if (player.wet)
						{
							player.lifeRegen += 10;
							player.manaRegen += 10;
							player.gills = true;
						}
						if (player.ZoneDesert)
						{
							player.lifeRegen -= 100;
							player.manaRegenBonus -= 30;
							player.GetDamage(DamageClass.Generic) -= 0.1f;
							player.moveSpeed -= 0.2f;
						}
						player.buffTime[buffIndex] = 999999;
						player.GetModPlayer<ChaoPlayer>().directory[summon] = true;
						bool petProjectileNotSpawned = true;
						if (player.ownedProjectileCounts[Find<ModProjectile>(summon).Type] > 0)
						{
							petProjectileNotSpawned = false;
						}
						if (petProjectileNotSpawned && player.whoAmI == Main.myPlayer)
						{
							Projectile.NewProjectile(player.GetProjectileSource_Buff(buffIndex), player.position.X + player.width / 2, player.position.Y + player.height / 2, 0f, 0f, Find<ModProjectile>(summon).Type, 0, 0f, player.whoAmI, 0f, 0f);
						}
					})));
			}
		}

        public override void Unload()
        {
			allChaos = null;
        }
    }
}