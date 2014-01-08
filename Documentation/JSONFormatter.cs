using System;
using System.Linq;
using System.Collections.Generic;
using TShockAPI;
using Newtonsoft.Json;
using Terraria;

namespace Documentation
{	
	/// <summary>
	/// Formats all data for objects into JSON for easy parsing with other tools and languages.
	/// </summary>
	public class JSONFormatter : IFormatter
	{
		public string Name { get { return "JSON"; } }
		public string Extension { get { return ".json"; } }
		
		public string FormatCommands()
		{
			return JsonConvert.SerializeObject(Commands.ChatCommands, Formatting.Indented);
		}
		
		public string FormatMobs()
		{
			List<NPC> mobs = new List<NPC>();
			for (int i = 0; i < Main.maxItemTypes; i++) 
			{
				mobs.Add(TShock.Utils.GetNPCById(i));
			}	
			return JsonConvert.SerializeObject(mobs, Formatting.Indented);
		}
		
		public string FormatItems()
		{
			List<Item> items = new List<Item>();
			for (int i = 0; i < Main.maxItemTypes; i++) 
			{
				items.Add(TShock.Utils.GetItemById(i));
			}
			return JsonConvert.SerializeObject(items, Formatting.Indented);
		}
		
		public string FormatTiles()
		{
			List<Item> tiles = new List<Item>();
			for (int i = 0; i < Main.maxItemTypes; i++) 
			{
				tiles.Add(TShock.Utils.GetItemById(i));
			}
			tiles = tiles.Where(x => x.createTile > 0).ToList();
			return JsonConvert.SerializeObject(tiles, Formatting.Indented);
		}
		
		public string FormatWalls()
		{
			List<Item> walls = new List<Item>();
			for (int i = 0; i < Main.maxItemTypes; i++) 
			{
				walls.Add(TShock.Utils.GetItemById(i));
			}
			walls = walls.Where(x => x.createWall > 0).ToList();
			return JsonConvert.SerializeObject(walls, Formatting.Indented);
		}

        public string FormatProjectiles()
        {
            List<projectileInfo> projIs = new List<projectileInfo>();

            Projectile proj = new Projectile();
            for (int i = 1; i < Main.maxProjectileTypes; i++)
            {
                proj.SetDefaults(i);
                var projInfo = new projectileInfo(proj.name, proj.type, proj.aiStyle, proj.friendly, proj.penetrate,
                    proj.noDropItem, proj.minion, proj.numHits, proj.magic, proj.ranged, proj.melee, proj.damage);

                projIs.Add(projInfo);
            }
            return JsonConvert.SerializeObject(projIs, Formatting.Indented);
        }
	}

    public class projectileInfo
    {
        public string name;
        public int type;
        public int aiStyle;
        public bool friendly;
        public int penetrate;
        public bool noDropItem;
        public bool minion;
        public int numHits;
        public bool magic;
        public bool ranged;
        public bool melee;
        public int damage;

        public projectileInfo(string n, int t, int ai, bool f, int p, 
            bool nDI, bool m, int nH, bool ma, bool ra, bool me,int d)
        {
            this.name = n;
            this.type = t;
            this.aiStyle = ai;
            this.friendly = f;
            this.penetrate = p;
            this.noDropItem = nDI;
            this.minion = m;
            this.numHits = nH;
            this.magic = ma;
            this.ranged = ra;
            this.melee = me;
            this.damage = d;
        }
    }
}
