using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ModLoader;

namespace ChaoGardenMod.Core
{
	[Autoload(false)]
    public class AbstractChaoProj : ModProjectile
    {
        protected string name;
        protected string type;

		public override string Name => name;
		public override string Texture => $"ChaoGardenMod/Assets/Chaos/{name.Replace($"{type} ", "")}/{(type != "" ? type : "None")}";

        public override bool CloneNewInstances => true;

        public AbstractChaoProj(string name, string type)
        {
            this.name = name;
            this.type = type;
			ChaoGardenMod.allChaos.Add(name);
			ChaoGardenMod.allChaosPairs.Add(name, false);
        }

        public override bool IsLoadingEnabled(Mod mod) => true;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault(name);
            Main.projFrames[Projectile.type] = 4;
            Main.projPet[Projectile.type] = true;
        }

        public override void SetDefaults()
        {
            Projectile.CloneDefaults(380);
            AIType = 380;
            Projectile.width = 30;
            Projectile.height = 46;
            Projectile.scale = 1.1f;
        }

        public override bool PreAI()
        {
            Main.player[Projectile.owner].zephyrfish = false;
            return true;
        }

        public override void AI()
        {
			Player player = Main.player[Projectile.owner];
			ChaoPlayer modPlayer = player.GetModPlayer<ChaoPlayer>();
			if (!Collision.CanHitLine(Projectile.Center, 1, 1, player.Center, 1, 1))
			{
				Projectile.ai[0] = 1f;
			}
			float speed = 8f;
			if (Projectile.ai[0] == 1f)
			{
				speed = 17f;
			}
			Vector2 center = Projectile.Center;
			Vector2 direction = player.Center - center;
			Projectile.ai[1] = 3600f;
			Projectile.netUpdate = true;
			int num = 1;
			for (int i = 0; i < Projectile.whoAmI; i++)
			{
				if (Main.projectile[i].active && Main.projectile[i].owner == Projectile.owner && Main.projectile[i].type == Projectile.type)
				{
					num++;
				}
				if (modPlayer.directory[name])
				{
					Projectile.timeLeft = 2;
					num++;
				}
				if (player.dead)
                {
					modPlayer.directory[name] = false;
                }
			}
			direction.X -= num * 40 * player.direction;
			direction.Y -= 30f;
			float num2 = direction.Length();
			if (num2 > 200f && speed < 12f)
			{
				speed = 12f;
			}
			if (num2 < 100f && Projectile.ai[0] == 1f && !Collision.SolidCollision(Projectile.position, Projectile.width, Projectile.height))
			{
				Projectile.ai[0] = 0f;
				Projectile.netUpdate = true;
			}
			if (num2 > 2000f)
			{
				Projectile.Center = player.Center;
			}
			if (num2 > 10f)
			{
				direction.Normalize();
				direction *= speed;
				float temp = 20f;
				Projectile.velocity = (Projectile.velocity * temp + direction) / (temp + 1f);
			}
			else
			{
				Projectile projectile = Projectile;
				projectile.velocity *= (float)Math.Pow(0.9, 1.0);
			}
			double num3 = (double)Math.Abs(Projectile.velocity.X);
		}
    }
}
