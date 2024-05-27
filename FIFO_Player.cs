using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;

namespace FIFO_Minions
{
    public class FIFO_Player : ModPlayer
    {
        private readonly List<int> minionQueue = [];
        private float queueMinionSlots = 0;

        public List<int> MinionQueueClone()
        {
            return new(minionQueue);
        }

        public Projectile GetProjectileFromQueue()
        {
            return Main.projectile[minionQueue[0]];
        }

        public void RemoveProjectileFromQueue()
        {
            int whoAmI = minionQueue[0];
            Projectile projectile = Main.projectile[whoAmI];
            queueMinionSlots -= projectile.minionSlots;
            if (projectile.active)
            {
                projectile.Kill();
            }
            minionQueue.RemoveAt(0);
        }

        public void RemoveProjectileFromList(int whoAmI)
        {
            Projectile projectile = Main.projectile[whoAmI];
            queueMinionSlots -= projectile.minionSlots;
            minionQueue.Remove(whoAmI);
            if (projectile.active)
            {
                projectile.Kill();
            }
        }

        public void AddProjectileToQueue(Projectile projectile)
        {
            queueMinionSlots += projectile.minionSlots;
            minionQueue.Add(projectile.whoAmI);
        }

        public bool IsProjectileQueueEmpty()
        {
            return minionQueue.Count <= 0;
        }

        public float OnDemandTotalMinionSlots()
        {
            float total = 0f;
            for (int i = 0; i < Main.projectile.Length; i++)
            {
                if (Main.projectile[i].active && Main.projectile[i].owner == Player.whoAmI && Main.projectile[i].minion)
                {
                    total += Main.projectile[i].minionSlots;
                }
            }
            return total;
        }

        public override void PostUpdateBuffs()
        {
            if (Player.slotsMinions != queueMinionSlots)
            {
                foreach (int whoAmI in MinionQueueClone())
                {
                    Projectile projectile = Main.projectile[whoAmI];
                    if (!projectile.active)
                    {
                        queueMinionSlots -= projectile.minionSlots;
                        minionQueue.Remove(whoAmI);
                    }
                }
            }
        }
    }
}
