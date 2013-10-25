using System;
using System.Collections.Generic;
using TShockAPI;
using Terraria;

namespace Documentation
{
	/// <summary>
	/// Formats the output for writing docs to files.
	/// </summary>
	public interface IFormatter
	{
		string FormatWalls();
		string FormatTiles();
		string FormatItems();
		string FormatMobs();
		string FormatCommands();
	}
}
