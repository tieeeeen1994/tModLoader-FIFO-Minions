using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FIFO_Minions
{
    public class FIFO_Minions : Mod
	{
        public override void Load()
        {
            On_Player.FreeUpPetsAndMinions += On_FreeUpPetsAndMinions;
        }

        public override void Unload()
        {
            On_Player.FreeUpPetsAndMinions -= On_FreeUpPetsAndMinions;
        }

        private void On_FreeUpPetsAndMinions(On_Player.orig_FreeUpPetsAndMinions orig, Player player, Item item)
        {
            if (!FIFO_Config.I.IsItemIgnoredForFIFO(item))
            {
                FIFO_Player modPlayer = player.GetModPlayer<FIFO_Player>();
                if (!modPlayer.IsProjectileQueueEmpty())
                {
                    float itemMinionSlots = ItemID.Sets.StaffMinionSlotsRequired[item.type];
                    float playerMinionSlots = modPlayer.OnDemandTotalMinionSlots();
                    if (itemMinionSlots + playerMinionSlots > player.maxMinions)
                    {
                        float removedSlots = 0;
                        float slotsToRemove = itemMinionSlots + playerMinionSlots - player.maxMinions;
                        while (removedSlots < slotsToRemove)
                        {
                            if (modPlayer.IsProjectileQueueEmpty())
                            {
                                break;
                            }
                            Projectile projectile = modPlayer.GetProjectileFromQueue();
                            int? fifoGroupId = FIFO_Config.I.IsProjectilePurgedForFIFO(projectile);
                            if (fifoGroupId != null && projectile.type != item.shoot)
                            {
                                List<int> purgableMinionTypes = FIFO_Config.I.GetFIFOGroupID((int)fifoGroupId);
                                foreach (Projectile minion in Main.projectile)
                                {
                                    if (minion.owner == player.whoAmI && minion.minion &&
                                        purgableMinionTypes.Contains(minion.type))
                                    {
                                        modPlayer.RemoveProjectileFromList(minion.whoAmI);
                                        removedSlots += minion.minionSlots;
                                    }
                                }
                            }
                            else
                            {
                                removedSlots += projectile.minionSlots;
                                modPlayer.RemoveProjectileFromQueue();
                            }
                        }
                    }
                }
            }
            orig(player, item);
        }
    }
}
