using System;
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
		
		public string FormatWalls()
		{
			StringBuilder sb = new StringBuilder();
			List<Item> walls = new List<Item>();
			for (int i = 0; i < Main.maxItemTypes; i++) 
			{
				walls.Add(TShock.Utils.GetItemById(i));
			}
			walls = walls.Where(x => x.createWall > 0).ToList();
			
			sb.AppendLine("Name,Type");
			
			foreach (Item wall in walls)
			{
				sb.AppendFormat("{0},{1}\n", wall.name, wall.createWall);
			}
			
			return sb.ToString();
		}
		public string FormatTiles()
		{
			StringBuilder sb = new StringBuilder();
			List<Item> tiles = new List<Item>();
			for (int i = 0; i < Main.maxItemTypes; i++) 
			{
				tiles.Add(TShock.Utils.GetItemById(i));
			}
			tiles = tiles.Where(x => x.createTile > 0).ToList();
			
			sb.AppendLine("Name,Type");
			
			foreach (Item tile in tiles)
			{
				sb.AppendFormat("{0},{1}\n", tile.name, tile.createTile);
			}
			
			return sb.ToString();
		}
		
		public string FormatMobs()
		{
			StringBuilder sb = new StringBuilder();
			List<NPC> mobs = new List<NPC>();
			for (int i = 0; i < Main.maxItemTypes; i++) 
			{
				mobs.Add(TShock.Utils.GetNPCById(i));
			}			
			sb.AppendLine("Name,Type,LifeMax,LifeRegen,Defense,Damage,TownNPC");
			
			foreach (NPC mob in mobs)
			{
				sb.AppendFormat("{0},{1},{2},{3},{4},{5},{6}\n", mob.displayName, mob.type, mob.lifeMax, mob.lifeRegen, mob.defense, mob.damage, mob.townNPC);
			}
			
			return sb.ToString();
		}
		public string FormatItems()
		{
			StringBuilder sb = new StringBuilder();
			List<Item> items = new List<Item>();
			for (int i = 0; i < Main.maxItemTypes; i++) 
			{
				items.Add(TShock.Utils.GetItemById(i));
			}
			sb.AppendLine("Name,Type,Accessory,HeadSlot,BodySlot,LegSlot,Melee,Magic,Ranged,Mana,ManaIncrease,LifeRegen,Potion,Damage,Defense,Tooltip,Tooltip2");
			
			foreach (Item item in items)
			{
				sb.AppendFormat("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13},{14},{15},{16},{17}\n", 
				                 item.name, item.type, item.accessory,
	                			 item.headSlot, item.bodySlot, item.legSlot, 
	                			 item.melee, item.magic, item.ranged, 
	                			 item.mana, item.manaIncrease, item.lifeRegen, 
	                			 item.potion, item.damage, item.defense, 
	                			 item.toolTip, item.toolTip2, item.value);
			}
			
			return sb.ToString();
		}		
		public string FormatCommands()
		{
			StringBuilder sb = new StringBuilder();
			sb.AppendLine("Name,Permission,HelpText,AllowServer");
			
			foreach (Command com in Commands.ChatCommands)
			{
				sb.AppendFormat("{0},{1},{2},{3}\n",com.Name, com.Permissions.Count > 0 ? com.Permissions.First() : "",com.HelpText,com.AllowServer);
			}
			
			return sb.ToString();
		}

        public string FormatProjectiles()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Name,Type,AI,Friendly,Penetrating,NotItemDropping,Minion,NumberOfHits,Magic,Range,Melee,Damage");

            List<Projectile> projectiles = new List<Projectile>();
            Projectile projectile = new Projectile();

            for (int i = 0; i < Main.maxProjectileTypes; i++)
            {
                projectile.SetDefaults(i);
                projectiles.Add(projectile);
            }

            foreach (Projectile proj in projectiles)
            {
                sb.AppendFormat("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10}, {11}\n",
                    proj.name, proj.type, proj.aiStyle, proj.friendly, proj.penetrate,
                    proj.noDropItem, proj.minion, proj.numHits, proj.magic, proj.ranged,
                    proj.melee, proj.damage);
            }
            return sb.ToString();
        }
	}
}
