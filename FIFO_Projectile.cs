using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace FIFO_Minions
{
    public class FIFO_Projectile : GlobalProjectile
    {
        public override void OnSpawn(Projectile projectile, IEntitySource source)
        {
            if (FIFO_Config.I.IsProjectileForcedForFIFO(projectile) || IsApplicableMinion(projectile))
            {
                Player player = Main.player[projectile.owner];
                FIFO_Player modPlayer = player.GetModPlayer<FIFO_Player>();
                modPlayer.AddProjectileToQueue(projectile);
            }
        }

        private bool IsApplicableMinion(Projectile projectile)
        {
            return projectile.minion && !projectile.sentry && projectile.DamageType == DamageClass.Summon
                   && projectile.minionSlots > 0;
        }
    }
}
