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
	}
}
