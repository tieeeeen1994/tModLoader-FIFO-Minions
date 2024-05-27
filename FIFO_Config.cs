using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader.Config;

namespace FIFO_Minions
{
    public class FIFO_Config : ModConfig
    {
        public override ConfigScope Mode => ConfigScope.ServerSide;

        public List<ItemDefinition> nonFIFOItems;
        public Dictionary<ProjectileDefinition, int> purgeFIFOProjectiles;

        public FIFO_Config()
        {
            nonFIFOItems = [new(ItemID.StardustDragonStaff)];
            purgeFIFOProjectiles = new()
            {
                [new(ProjectileID.StardustDragon1)] = ItemID.StardustDragonStaff,
                [new(ProjectileID.StardustDragon2)] = ItemID.StardustDragonStaff,
                [new(ProjectileID.StardustDragon3)] = ItemID.StardustDragonStaff,
                [new(ProjectileID.StardustDragon4)] = ItemID.StardustDragonStaff
            };
        }

        public bool IsItemIgnoredForFIFO(Item item)
        {
            return IsItemIgnoredForFIFO(item.type);
        }

        public bool IsItemIgnoredForFIFO(int type)
        {
            return nonFIFOItems.Exists(definition => definition.Type == type);
        }

        public int? IsProjectilePurgedForFIFO(Projectile projectile)
        {
            return IsProjectilePurgedForFIFO(projectile.type);
        }

        public int? IsProjectilePurgedForFIFO(int type)
        {
            var query = purgeFIFOProjectiles.Where(pair => pair.Key.Type == type);
            return query.Any() ? query.First().Value : null;
        }

        public List<int> GetFIFOGroupID(int groupId)
        {
            return purgeFIFOProjectiles.Where(pair => pair.Value == groupId).Select(pair => pair.Key.Type).ToList();
        }
    }
}
