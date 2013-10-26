using System;
using System.Collections.Generic;
using TShockAPI;

namespace Documentation
{
	/// <summary>
	/// Allows for easy creation of sub-commands.
	/// </summary>
	public class SubCommandHandler
	{
		private Dictionary<string, Action<CommandArgs>> subCommands = new Dictionary<string, Action<CommandArgs>>();
		/// <summary>
		/// Help text that will be sent to the player should they activate the "help" subcommand
		/// or use an invalid sub-command. Newlines will be split up into separate messages in-game and
		/// so are safe to include.
		/// </summary>
		public string HelpText;
		
		public SubCommandHandler()
		{
			RegisterSubcommand("help", DisplayHelpText);
		}
		
		private void DisplayHelpText(CommandArgs args)
		{
			foreach (string item in HelpText.Split('\n'))
				args.Player.SendInfoMessage(item);
		}
		
		public void RegisterSubcommand(string command, Action<CommandArgs> func)
		{
			subCommands.Add(command, func);
		}
		
		/// <summary>
		/// Takes in the command arguments and finds and runs the sub-command.
		/// The sub-command is defined as the first parameter to the command.
		/// </summary>
		/// <param name="args">Arguments sent to the super-command.</param>
		/// <remarks>
		/// When the sub-command is invoked the function assigned to it is sent new
		/// command args. These args are identical to the old args except the first
		/// parameter from args.Parameters has been removed.
		/// </remarks>
		public void RunSubcommand(CommandArgs args)
		{
			
			if (args.Parameters.Count > 0)
			{
				CommandArgs newargs = new CommandArgs(args.Message, args.Player, args.Parameters.GetRange(1, args.Parameters.Count-1));
				try
				{
					subCommands[args.Parameters[0]].Invoke(newargs);
				}
				catch (Exception e)
				{
					args.Player.SendErrorMessage("The command has failed, check logs for info.");
					Log.Error(e.ToString());
					subCommands["help"].Invoke(newargs);
				}
			}
			else
				subCommands["help"].Invoke(args);
		}
	}
}
