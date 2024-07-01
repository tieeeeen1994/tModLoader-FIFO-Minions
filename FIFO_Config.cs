using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Config;

namespace FIFO_Minions
{
    public class FIFO_Config : ModConfig
    {
        public static FIFO_Config I => ModContent.GetInstance<FIFO_Config>();

        public override ConfigScope Mode => ConfigScope.ServerSide;

        // Config vars go here.
        [Header("Main")]
        public List<ItemDefinition> nonFIFOItems;
        public Dictionary<ProjectileDefinition, int> purgeFIFOProjectiles;
        public List<ProjectileDefinition> forcedFIFOProjectiles;

        [Header("Advanced")]
        public bool enableActiveFIFO;

        // Initialize config vars in the constructor.
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
            forcedFIFOProjectiles = [];
            enableActiveFIFO = false;
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

        public bool IsProjectileForcedForFIFO(Projectile projectile)
        {
            return IsProjectileForcedForFIFO(projectile.type);
        }

        public bool IsProjectileForcedForFIFO(int type)
        {
            return forcedFIFOProjectiles.Exists(definition => definition.Type == type);
        }
    }
}
