using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Reflection;
using Terraria;
using TerrariaApi.Server;

using TShockAPI;

namespace Documentation
{
    [ApiVersion(1, 17)]
    public class Documentation : TerrariaPlugin
    {
        public override string Author { get { return "White, Ijwu"; } }

        public override string Description { get { return "Creates documentation for Terraria things"; } }

        public override string Name { get { return "Documentation"; } }

        public override Version Version { get { return Assembly.GetExecutingAssembly().GetName().Version; } }
        
        public List<IFormatter> Formats = new List<IFormatter>();
        private SubCommandHandler handler = new SubCommandHandler();

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                ServerApi.Hooks.GameInitialize.Deregister(this, OnInitialize);
            }
            base.Dispose(disposing);
        }

        public override void Initialize()
        {
            ServerApi.Hooks.GameInitialize.Register(this, OnInitialize);
        }

        public void OnInitialize(EventArgs args)
        {
        	Formats.Add(new CSVFormatter());
        	Formats.Add(new JSONFormatter());
        	
        	Commands.ChatCommands.Add(new Command("docs.docgen", Document, "docgen") {AllowServer = true, HelpText = "Master command for the Documentation plugin."});
        	
        	handler.RegisterSubcommand("mobs", DocumentNPCs);
        	handler.RegisterSubcommand("items", DocumentItems);
        	handler.RegisterSubcommand("tiles", DocumentTiles);
        	handler.RegisterSubcommand("walls", DocumentWalls);
        	handler.RegisterSubcommand("commands", DocumentCommands);
            handler.RegisterSubcommand("projectiles", DocumentProjectiles);
        	handler.RegisterSubcommand("all", DocumentAll);
        	
        	handler.HelpText = "Syntax: /docgen [sub-command] [format]\nAvailable sub-commands: mobs, items, tiles, walls, commands, projectiles\nAvailable formats: csv, json\nAppend format with ' -simple' for simple output (json only)";
        }
        
        private IFormatter GetFormatter(string name)
        {
        	try
        	{
        		return Formats.First(x => String.Equals(x.Name, name, StringComparison.CurrentCultureIgnoreCase));
        	}
        	catch
        	{
        		return null;
        	}
        }

        private void Document(CommandArgs args)
        {        	
        	handler.RunSubcommand(args);
        }

        private void DocumentNPCs(CommandArgs args)
        {
        	var format = args.Parameters.Count > 0 ? GetFormatter(args.Parameters[0]) : null;
            var simple = args.Parameters.Count > 1 && args.Parameters[1].ToLower() == "-simple";
        	if (format == null)
        	{
        		args.Player.SendErrorMessage("Invalid format provided.");
        	    args.Player.SendWarningMessage("If using simple formatting: /docgen mobs json -simple");
        		return;
        	}
            File.WriteAllText(Path.Combine(TShock.SavePath, "mobs" + format.Extension), 
                simple ? format.FormatMobsSimple() : format.FormatMobs());
        	
        	args.Player.SendSuccessMessage("Mob documentation has been written.");
        }

        private void DocumentItems(CommandArgs args)
        {
            var format = args.Parameters.Count > 0 ? GetFormatter(args.Parameters[0]) : null;
            var simple = args.Parameters.Count > 1 && args.Parameters[1].ToLower() == "-simple";
        	if (format == null)
        	{
        		args.Player.SendErrorMessage("Invalid format provided.");
                args.Player.SendWarningMessage("If using simple formatting: /docgen items json -simple");
        		return;
        	}
        	File.WriteAllText(Path.Combine(TShock.SavePath, "items"+format.Extension),
                simple ? format.FormatItemsSimple() : format.FormatItems());
        	
        	args.Player.SendSuccessMessage("Item documentation has been written.");
        }
        
        private void DocumentTiles(CommandArgs args)
        {
            var format = args.Parameters.Count > 0 ? GetFormatter(args.Parameters[0]) : null;
            var simple = args.Parameters.Count > 1 && args.Parameters[1].ToLower() == "-simple";
        	if (format == null)
        	{
                args.Player.SendErrorMessage("Invalid format provided.");
                args.Player.SendWarningMessage("If using simple formatting: /docgen tiles json -simple");
        		return;
        	}
        	File.WriteAllText(Path.Combine(TShock.SavePath, "tiles"+format.Extension), 
                simple ? format.FormatTilesSimple() : format.FormatTiles());
        	
        	args.Player.SendSuccessMessage("Tile documentation has been written.");
        }
        
        private void DocumentWalls(CommandArgs args)
        {
            var format = args.Parameters.Count > 0 ? GetFormatter(args.Parameters[0]) : null;
            var simple = args.Parameters.Count > 1 && args.Parameters[1].ToLower() == "-simple";
        	if (format == null)
        	{
                args.Player.SendErrorMessage("Invalid format provided.");
                args.Player.SendWarningMessage("If using simple formatting: /docgen walls json -simple");
        		return;
        	}
            File.WriteAllText(Path.Combine(TShock.SavePath, "walls" + format.Extension),
                simple ? format.FormatWallsSimple() : format.FormatWalls());
        	
        	args.Player.SendSuccessMessage("Wall documentation has been written.");
        }
        
        private void DocumentCommands(CommandArgs args)
        {
            var format = args.Parameters.Count > 0 ? GetFormatter(args.Parameters[0]) : null;
        	if (format == null)
        	{
                args.Player.SendErrorMessage("Invalid format provided.");
        		return;
        	}
        	File.WriteAllText(Path.Combine(TShock.SavePath, "commands"+format.Extension), format.FormatCommands());
        	
        	args.Player.SendSuccessMessage("Command documentation has been written.");
        }

        private void DocumentProjectiles(CommandArgs args)
        {
            var format = args.Parameters.Count > 0 ? GetFormatter(args.Parameters[0]) : null;
            var simple = args.Parameters.Count > 1 && args.Parameters[1].ToLower() == "-simple";
            if (format == null)
            {
                args.Player.SendErrorMessage("Invalid format provided.");
                args.Player.SendWarningMessage("If using simple formatting: /docgen projectiles json -simple");
                return;
            }
            File.WriteAllText(Path.Combine(TShock.SavePath, "projectiles"+format.Extension), 
                simple ? format.FormatProjectilesSimple() : format.FormatProjectiles());

            args.Player.SendSuccessMessage("Projectile documentation has been written.");
        }
        
        private void DocumentAll(CommandArgs args)
        {
        	DocumentCommands(args);
        	DocumentItems(args);
        	DocumentNPCs(args);
        	DocumentTiles(args);
        	DocumentWalls(args);
            DocumentProjectiles(args);
        }

        public Documentation(Main game)
            : base(game)
        {
            Order = 1;
        }
    }
}
