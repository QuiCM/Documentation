using System.Data;
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
			var mobs = new List<NPC>();
			for (var i = -65; i < Main.maxNPCTypes; i++) 
				mobs.Add(TShock.Utils.GetNPCById(i));

            var orderedMobs = from x in mobs orderby x.type select x;
            return JsonConvert.SerializeObject(orderedMobs, Formatting.Indented);
		}

	    public string FormatMobsSimple()
	    {
	        var mobs = new List<MobInfo>();
	        for (var i = -65; i < Main.maxNPCTypes; i++)
	        {
	            var mob = TShock.Utils.GetNPCById(i);
	            var info = new MobInfo(mob.name, mob.life,
	                mob.damage, mob.defense, mob.type, mob.friendly, mob.townNPC);
	            mobs.Add(info);
            }
            var orderedMobs = from x in mobs orderby x.type select x;
            return JsonConvert.SerializeObject(orderedMobs, Formatting.Indented);
	    }
		
		public string FormatItems()
		{
			var items = new List<Item>();
		    for (var i = -48; i < Main.maxItemTypes; i++)
		        items.Add(TShock.Utils.GetItemById(i));

            var orderedItems = from x in items orderby x.type select x;
            return JsonConvert.SerializeObject(orderedItems, Formatting.Indented);
		}

	    public string FormatItemsSimple()
	    {
	        var items = new List<ItemInfo>();
	        for (var i = -48; i < Main.maxItemTypes; i++)
	        {
	            var item = TShock.Utils.GetItemById(i);
	            var info = new ItemInfo(item.name, item.useTime, item.createTile, item.createWall,
	                item.type, item.autoReuse, item.consumable, item.maxStack);
	            items.Add(info);
            }
            var orderedItems = from x in items orderby x.type select x;
            return JsonConvert.SerializeObject(orderedItems, Formatting.Indented);
	    }
		
		public string FormatTiles()
		{
			var tiles = new List<Item>();
			for (var i = -48; i < Main.maxItemTypes; i++) 
				tiles.Add(TShock.Utils.GetItemById(i));

			tiles = tiles.Where(x => x.createTile > 0).ToList();
            var orderedTiles = from x in tiles orderby x.createTile select x;
			return JsonConvert.SerializeObject(orderedTiles, Formatting.Indented);
		}

	    public string FormatTilesSimple()
	    {
	        var tiles = new List<TileInfo>();
	        for (var i = -48; i < Main.maxItemTypes; i++)
	        {
	            var tile = TShock.Utils.GetItemById(i);
	            if (!(tile.createTile > 0))
	                continue;
	            var info = new TileInfo(tile.name, tile.type, tile.createTile);
	            tiles.Add(info);
	        }
	        var orderedTiles = from x in tiles orderby x.createtile select x;
            return JsonConvert.SerializeObject(orderedTiles, Formatting.Indented);
	    }
		
		public string FormatWalls()
		{
			var walls = new List<Item>();
			for (var i = -48; i < Main.maxItemTypes; i++) 
				walls.Add(TShock.Utils.GetItemById(i));

            walls = walls.Where(x => x.createWall > 0).ToList();
            var orderedWalls = from x in walls orderby x.createWall select x;
            return JsonConvert.SerializeObject(orderedWalls, Formatting.Indented);
		}

	    public string FormatWallsSimple()
	    {
	        var walls = new List<WallInfo>();
	        for (var i = -48; i < Main.maxItemTypes; i++)
	        {
	            var wall = TShock.Utils.GetItemById(i);
	            if (!(wall.createWall > 0))
	                continue;
	            var info = new WallInfo(wall.name, wall.type, wall.createWall);
	            walls.Add(info);
            }
            var orderedWalls = from x in walls orderby x.createwall select x;
            return JsonConvert.SerializeObject(orderedWalls, Formatting.Indented);
	    }

	    public string FormatProjectiles()
	    {
	        var proj = new Projectile();
	        var projectiles = new List<Projectile>();
	        for (var i = 1; i < Main.maxProjectileTypes; i++)
	        {
	            proj.SetDefaults(i);
	            projectiles.Add(proj);
            } 
            var orderedProjectiles = from x in projectiles orderby x.type select x;
            return JsonConvert.SerializeObject(orderedProjectiles, Formatting.Indented);
	    }

        public string FormatProjectilesSimple()
        {
            var projIs = new List<ProjectileInfo>();

            var proj = new Projectile();
            for (var i = 1; i < Main.maxProjectileTypes; i++)
            {
                proj.SetDefaults(i);
                var projInfo = new ProjectileInfo(proj.name, proj.type, proj.aiStyle, proj.friendly, proj.penetrate,
                    proj.noDropItem, proj.minion, proj.numHits, proj.magic, proj.ranged, proj.melee, proj.damage);

                projIs.Add(projInfo);
            }
            var orderedProjectiles = from x in projIs orderby x.type select x;
            return JsonConvert.SerializeObject(orderedProjectiles, Formatting.Indented);
        }
	}

    /// <summary>
    /// Represents an object that contains basic info about an NPC
    /// </summary>
    public class MobInfo
    {
        public string name;
        public int health;
        public int damage;
        public int defense;
        public int type;
        public bool friendly;
        public bool townnpc;

        /// <summary>
        /// Initializes a new instance of the MobInfo class with particular values
        /// </summary>
        /// <param name="n">Name of the NPC</param>
        /// <param name="h">Health value of the NPC</param>
        /// <param name="d">Damage value of the NPC</param>
        /// <param name="def">Defense value of the NPC</param>
        /// <param name="t">Type of the NPC</param>
        /// <param name="f">Friendly value of the NPC</param>
        /// <param name="tn">TownNPC value of the NPC</param>
        public MobInfo(string n, int h, int d,  int def, int t, bool f, bool tn)
        {
            name = n;
            health = h;
            damage = d;
            type = t;
            friendly = f;
            townnpc = tn;
            defense = def;
        }
    }

    /// <summary>
    /// Represents an object that contains basic info about an item
    /// </summary>
    public class ItemInfo
    {
        public string name;
        public int usetime;
        public int createtile;
        public int createwall;
        public int type;
        public bool autoreuse;
        public bool consumable;
        public int maxstack;

        /// <summary>
        /// Initializes a new instance of the ItemInfo class with particular values
        /// </summary>
        /// <param name="n">Name of the time</param>
        /// <param name="u">Usetime of the item</param>
        /// <param name="ct">CreateTile value of the item</param>
        /// <param name="cw">CreateWall value of the item</param>
        /// <param name="t">Type of the item</param>
        /// <param name="a">Autoreuse value of the item</param>
        /// <param name="c">Consumable value of the item</param>
        /// <param name="m">Maximum stack value of the item</param>
        public ItemInfo(string n, int u, int ct, int cw, int t, bool a, bool c, int m)
        {
            name = n;
            usetime = u;
            createtile = ct;
            createwall = cw;
            type = t;
            autoreuse = a;
            consumable = c;
            maxstack = m;
        }
    }

    /// <summary>
    /// Represents an object that contains basic info about a Tile
    /// </summary>
    public class TileInfo
    {
       public string name;
       public int type;
       public int createtile;

        /// <summary>
        /// Initializes a new instance of the TileInfo class with particular values
        /// </summary>
        /// <param name="n">Name of the tile</param>
        /// <param name="t">Type of the tile</param>
        /// <param name="ti">CreateTile value of the tile</param>
        public TileInfo(string n, int t, int ti)
        {
            name = n;
            type = t;
            createtile = ti;
        }
    }

    /// <summary>
    /// Represents an object that contains basic info about a Wall
    /// </summary>
    public class WallInfo
    {
        public string name;
        public int type;
        public int createwall;

        /// <summary>
        /// Initializes a new instance of the WallInfo class with particular values
        /// </summary>
        /// <param name="n">Name of the wall</param>
        /// <param name="t">Type of the wall</param>
        /// <param name="w">CreateWall value of the wall</param>
        public WallInfo(string n, int t, int w)
        {
            name = n;
            type = t;
            createwall = w;
        }
    }

    /// <summary>
    /// Represents an object that contains basic info about a Projectile
    /// </summary>
    public class ProjectileInfo
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

        /// <summary>
        /// Initializes a new instance of the ProjectileInfo class with particular values
        /// </summary>
        /// <param name="n">Name of the projectile</param>
        /// <param name="t">Type of the projectile</param>
        /// <param name="ai">AIStyle of the projectile</param>
        /// <param name="f">Friendly value of the projectile</param>
        /// <param name="p">Penetration value of the projectile</param>
        /// <param name="nDi">NoDropItem value of the projectile</param>
        /// <param name="m">Minion value of the projectile</param>
        /// <param name="nH">Number of hits value of the projectile</param>
        /// <param name="ma">Magic value of the projectile</param>
        /// <param name="ra">Ranged value of the projectile</param>
        /// <param name="me">Melee value of the projectile</param>
        /// <param name="d">Damage value of the projectile</param>
        public ProjectileInfo(string n, int t, int ai, bool f, int p, 
            bool nDi, bool m, int nH, bool ma, bool ra, bool me,int d)
        {
            name = n;
            type = t;
            aiStyle = ai;
            friendly = f;
            penetrate = p;
            noDropItem = nDi;
            minion = m;
            numHits = nH;
            magic = ma;
            ranged = ra;
            melee = me;
            damage = d;
        }
    }
}
