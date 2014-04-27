using System.Collections.Generic;
using System.Text;
using System.Linq;
using Terraria;
using TShockAPI;

namespace Documentation
{
	/// <summary>
	/// Description of CSVFormatter.
	/// </summary>
	public class CSVFormatter : IFormatter
	{
		public string Name {get {return "CSV";}}
		public string Extension {get {return ".csv";}}

	    public string FormatItemsSimple()
	    {
	        return FormatItems();
	    }

	    public string FormatWallsSimple()
	    {
	        return FormatWalls();
	    }

	    public string FormatTilesSimple()
	    {
	        return FormatTiles();
	    }

	    public string FormatMobsSimple()
	    {
	        return FormatMobs();
	    }

	    public string FormatProjectilesSimple()
	    {
	        return FormatProjectiles();
	    }

		public string FormatWalls()
		{
			var sb = new StringBuilder();
			var walls = new List<Item>();
			for (var i = 0; i < Main.maxItemTypes; i++) 
			{
				walls.Add(TShock.Utils.GetItemById(i));
			}
			walls = walls.Where(x => x.createWall > 0).ToList();

            sb.AppendLine("\"Name\",\"Type\"");
			
			foreach (var wall in walls)
			{
                sb.AppendFormat("\"{0}\",\"{1}\"\n", wall.name, wall.createWall);
			}
			
			return sb.ToString();
		}
		public string FormatTiles()
		{
			var sb = new StringBuilder();
			var tiles = new List<Item>();
			for (var i = 0; i < Main.maxItemTypes; i++) 
			{
				tiles.Add(TShock.Utils.GetItemById(i));
			}
			tiles = tiles.Where(x => x.createTile > 0).ToList();

            sb.AppendLine("\"Name\",\"Type\"");
			
			foreach (var tile in tiles)
			{
                sb.AppendFormat("\"{0}\",\"{1}\"\n", tile.name, tile.createTile);
			}
			
			return sb.ToString();
		}
		
		public string FormatMobs()
		{
			var sb = new StringBuilder();
			var mobs = new List<NPC>();
			for (var i = 0; i < Main.maxNPCTypes; i++) 
			{
				mobs.Add(TShock.Utils.GetNPCById(i));
			}
            sb.AppendLine("\"Name\",\"Type\",\"LifeMax\",\"LifeRegen\",\"Defense\",\"Damage\",\"TownNPC\"");
			
			foreach (var mob in mobs)
			{
                sb.AppendFormat("\"{0}\",\"{1}\",\"{2}\",\"{3}\",\"{4}\",\"{5}\",\"{6}\"\n", mob.displayName, mob.type, mob.lifeMax, mob.lifeRegen, mob.defense, mob.damage, mob.townNPC);
			}
			
			return sb.ToString();
		}

		public string FormatItems()
		{
            var sb = new StringBuilder();

            //headers are first line
            foreach (var fi in typeof(Item).GetFields())
            {
                sb.AppendFormat("\"{0}\",", fi.Name);
            }

            sb.Append("\r\n");

            for (var i = -48; i < Main.maxItemTypes; i++)
            {
                var item = TShock.Utils.GetItemById(i);
                foreach (var fi in typeof(Item).GetFields())
                {
                    sb.AppendFormat("\"{0}\",", fi.GetValue(item));
                }

                sb.Append("\r\n");
            }

            return sb.ToString();
		}
		
		public string FormatCommands()
		{
			var sb = new StringBuilder();
            sb.AppendLine("\"Name\",\"Permission\",\"HelpText\",\"AllowServer\"");
			
			foreach (var com in Commands.ChatCommands)
			{
                sb.AppendFormat("\"{0}\",\"{1}\",\"{2}\",\"{3}\"\n", com.Name, com.Permissions.Count > 0 ? com.Permissions.First() : "", com.HelpText, com.AllowServer);
			}
			
			return sb.ToString();
		}

        public string FormatProjectiles()
        {
            var sb = new StringBuilder();
            sb.AppendLine("\"Name\",\"Type\",\"AI\",\"Friendly\",\"Penetrating\",\"NotItemDropping\",\"Minion\",\"NumberOfHits\",\"Magic\",\"Range\",\"Melee\",\"Damage\"");

            var projectiles = new List<Projectile>();

            for (int i = 0; i < Main.maxProjectileTypes; i++)
            {
                var proj = new Projectile();
                proj.SetDefaults(i);
                projectiles.Add(proj);
            }

            foreach (var proj in projectiles)
            {
                sb.AppendFormat("\"{0}\"\",{1}\",\"{2}\",\"{3}\",\"{4}\",\"{5}\",\"{6}\",\"{7}\",\"{8}\",\"{9}\",\"{10}\",\"{11}\"\n",
                    proj.name, proj.type, proj.aiStyle, proj.friendly, proj.penetrate,
                    proj.noDropItem, proj.minion, proj.numHits, proj.magic, proj.ranged,
                    proj.melee, proj.damage);
            }
            return sb.ToString();
        }
	}
}
