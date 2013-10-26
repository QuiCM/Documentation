using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;

using Terraria;
using TerrariaApi;
using TerrariaApi.Server;

using TShockAPI;

namespace Documentation
{
    [ApiVersion(1, 14)]
    public class Documentation : TerrariaPlugin
    {
        public override string Author { get { return "WhiteX"; } }

        public override string Description { get { return "Creates item documentation for Terraria"; } }

        public override string Name { get { return "Documentation"; } }

        public override Version Version { get { return new Version(1, 0, 0); } }
        
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
        	
        	Commands.ChatCommands.Add(new Command("documentation", Document, "docgen") {AllowServer = true, HelpText = "Master command for the Documentation plugin."});
        	
        	handler.RegisterSubcommand("mobs", DocumentNPCs);
        	handler.RegisterSubcommand("items", DocumentItems);
        	handler.RegisterSubcommand("tiles", DocumentTiles);
        	handler.RegisterSubcommand("walls", DocumentWalls);
        	handler.RegisterSubcommand("commands", DocumentCommands);
        	handler.RegisterSubcommand("all", DocumentAll);
        	
        	handler.HelpText = "Available sub-commands: mobs, items, tiles, walls, commands\nAvailable formats: csv";
        }
        
        private IFormatter GetFormatter(string name)
        {
        	try
        	{
        		return Formats.Where(x => x.Name.ToLower() == name.ToLower()).First();
        	}
        	catch
        	{
        		return null;
        	}
        }

        public void Document(CommandArgs args)
        {        	
        	handler.RunSubcommand(args);
        }
        
        public void DocumentNPCs(CommandArgs args)
        {
        	IFormatter format = GetFormatter(args.Parameters[0]);
        	if (format == null)
        	{
        		args.Player.SendErrorMessage("Invalid format provided.");
        		return;
        	}
        	File.WriteAllText(Path.Combine(TShock.SavePath, "mobs"+format.Extension), format.FormatMobs());
        	
        	args.Player.SendSuccessMessage("Mob documentation has been written.");
        }
        
        public void DocumentItems(CommandArgs args)
        {
        	IFormatter format = GetFormatter(args.Parameters[0]);
        	if (format == null)
        	{
        		args.Player.SendErrorMessage("Invalid format provided.");
        		return;
        	}
        	File.WriteAllText(Path.Combine(TShock.SavePath, "items"+format.Extension), format.FormatItems());
        	
        	args.Player.SendSuccessMessage("Item documentation has been written.");
        }
        
        public void DocumentTiles(CommandArgs args)
        {
        	IFormatter format = GetFormatter(args.Parameters[0]);
        	if (format == null)
        	{
        		args.Player.SendErrorMessage("Invalid format provided.");
        		return;
        	}
        	File.WriteAllText(Path.Combine(TShock.SavePath, "tiles"+format.Extension), format.FormatTiles());
        	
        	args.Player.SendSuccessMessage("Tile documentation has been written.");
        }
        
        public void DocumentWalls(CommandArgs args)
        {
        	IFormatter format = GetFormatter(args.Parameters[0]);
        	if (format == null)
        	{
        		args.Player.SendErrorMessage("Invalid format provided.");
        		return;
        	}
        	File.WriteAllText(Path.Combine(TShock.SavePath, "walls"+format.Extension), format.FormatWalls());
        	
        	args.Player.SendSuccessMessage("Wall documentation has been written.");
        }
        
        public void DocumentCommands(CommandArgs args)
        {
        	IFormatter format = GetFormatter(args.Parameters[0]);
        	if (format == null)
        	{
        		args.Player.SendErrorMessage("Invalid format provided.");
        		return;
        	}
        	File.WriteAllText(Path.Combine(TShock.SavePath, "commands"+format.Extension), format.FormatCommands());
        	
        	args.Player.SendSuccessMessage("Command documentation has been written.");
        }
        
        public void DocumentAll(CommandArgs args)
        {
        	DocumentCommands(args);
        	DocumentItems(args);
        	DocumentNPCs(args);
        	DocumentTiles(args);
        	DocumentWalls(args);
        	
        	args.Player.SendSuccessMessage("All documentation has been written.");
        }

        public Documentation(Main game)
            : base(game)
        {
            Order = 1;
        }
    }
}
