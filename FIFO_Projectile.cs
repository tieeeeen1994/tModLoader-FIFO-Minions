using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace FIFO_Minions
{
    public class FIFO_Projectile : GlobalProjectile
    {
        public override void OnSpawn(Projectile projectile, IEntitySource source)
        {
            if (projectile.minion && projectile.minionSlots > 0)
            {
                Player player = Main.player[projectile.owner];
                FIFO_Player modPlayer = player.GetModPlayer<FIFO_Player>();
                modPlayer.AddProjectileToQueue(projectile);
            }
        }
    }
}
