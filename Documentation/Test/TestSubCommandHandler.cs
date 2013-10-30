//using System;
//using System.Collections.Generic;
//using NUnit.Framework;
//using Documentation;
//using TShockAPI;

//namespace Documentation.Test
//{
//    [TestFixture]
//    public class TestSubCommandHandler
//    {
//        [Test]
//        public void TestMethod()
//        {
//            SubCommandHandler handler = new SubCommandHandler(){HelpText = "The available subcommands are: help, add, sub"};
//            CommandArgs args = new CommandArgs("/math add 2 5", new TSRestPlayer("test", Group.DefaultGroup), new List<string>{"add", "2", "5"});
//            handler.RegisterSubcommand("add", Add);
//            handler.RunSubcommand(args);
//        }
		
//        private void Add(CommandArgs args)
//        {
//            foreach (string item in args.Parameters)
//                Console.WriteLine(item);
//            Console.WriteLine("Test Success");
//        }
//    }
//}
