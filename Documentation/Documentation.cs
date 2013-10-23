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
        public bool wroteAll = false;

        public override string Author { get { return "WhiteX"; } }

        public override string Description { get { return "Creates item documentation for Terraria"; } }

        public override string Name { get { return "Documentation"; } }

        public override Version Version { get { return new Version(1, 0, 0); } }

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
            Commands.ChatCommands.Add(new Command("docs.items", Items, "getitems") { AllowServer = true });
            Commands.ChatCommands.Add(new Command("docs.tiles", Tiles, "gettiles") { AllowServer = true });
            Commands.ChatCommands.Add(new Command("docs.walls", Walls, "getwalls") { AllowServer = true });
            Commands.ChatCommands.Add(new Command("docs.*", All, "getall") { AllowServer = true });
        }

        public void Items(CommandArgs args)
        {
            var sb = new StringBuilder();
            for (int i = 0; i < Main.maxItemTypes; i++)
            {
                var item = TShock.Utils.GetItemById(i);
                if (sb.Length == 0)
                    sb.AppendLine();

                sb.AppendLine(string.Format("Name: {0}  ID: {1}", item.name, i));
                sb.AppendLine();
            }
            File.WriteAllText(Path.Combine(TShock.SavePath, "Items.txt"), "=====Terraria Items==== \n" + sb.ToString());

            if (!wroteAll)
                args.Player.SendSuccessMessage("Wrote all items into a text file");
        }

        public void Tiles(CommandArgs args)
        {
            var sb = new StringBuilder();
            for (int i = 0; i < Main.maxItemTypes; i++)
            {
                if (TShock.Utils.GetItemById(i).createTile > 0)
                {
                    var item = TShock.Utils.GetItemById(i);
                    if (sb.Length == 0)
                        sb.AppendLine();

                    sb.AppendLine("Name: " + item.name + "  ID: " + i + "   Tile type: " + item.createTile);
                    sb.AppendLine();
                }
            }
            File.WriteAllText(Path.Combine(TShock.SavePath, "Tiles.txt"), "=====Terraria Tiles==== \n" + sb.ToString());

            if (!wroteAll)
                args.Player.SendSuccessMessage("Wrote all tiles into a text file");
        }

        public void Walls(CommandArgs args)
        {
            var sb = new StringBuilder();
            for (int i = 0; i < Main.maxItemTypes; i++)
            {
                if (TShock.Utils.GetItemById(i).createWall > 0)
                {
                    var item = TShock.Utils.GetItemById(i);
                    if (sb.Length == 0)
                        sb.AppendLine();

                    sb.AppendLine("Name: " + item.name + "  ID: " + i + "   Wall type: " + item.createWall);
                    sb.AppendLine();
                }
            }
            File.WriteAllText(Path.Combine(TShock.SavePath, "Walls.txt"), "=====Terraria Walls==== \n" + sb.ToString());

            if (!wroteAll)
                args.Player.SendSuccessMessage("Wrote all walls into a text file");
        }

        public void All(CommandArgs args)
        {
            wroteAll = true;

            Walls(args);
            Tiles(args);
            Items(args);

            args.Player.SendSuccessMessage("Wrote 3 files, containing items, walls and tiles");
            wroteAll = false;
        }

        public Documentation(Main game)
            : base(game)
        {
            Order = 1;
        }
    }
}
