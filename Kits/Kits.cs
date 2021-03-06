﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using TShockAPI;
using Terraria;
using TerrariaApi.Server;

namespace Kits
{
    [ApiVersion(1, 17)]
    public class Kits : TerrariaPlugin
    {
	    public static ConfigFile config;
		public static String savepath = Path.Combine(TShockAPI.TShock.SavePath, "kits.cfg");

        public static Kit FindKit( String name )
        {
			return config.Kits.findKit(name);
        }

        public override Version Version
        {
            get { return new Version("1.1"); }
        }

        public override string Name
        {
            get { return "Kits"; }
        }

        public override string Author
        {
            get { return "Olink"; }
        }

        public override string Description
        {
            get { return "Create kits for giving packs of items!"; }
        }

        /// <summary>
        /// Called after TShock is initialized. Useful for plugins that needs hooks before tshock but also depend on tshock being loaded.
        /// </summary>
        public static event Action Initialized;

        public Kits(Main game)
            : base(game)
        {
            Order = 4;

	        config = ConfigFile.Read(savepath);
            config.Write(savepath);

            Console.WriteLine( "End of pre initialize");
            
            if (Initialized != null)
                Initialized();
        }

        public override void Initialize()
        {
            Commands.ChatCommands.Add( new Command("", HandleCommand, "kit") { AllowServer = false });
	        TShockAPI.Hooks.GeneralHooks.ReloadEvent += OnReload;
        }

	    private void OnReload(TShockAPI.Hooks.ReloadEventArgs args)
	    {
		    config = ConfigFile.Read(savepath);
			args.Player.SendSuccessMessage("Successfully reloaded the Kits config.");
	    }

	    private void HandleCommand(CommandArgs args)
	    {
		    TSPlayer ply = args.Player;
		    if (ply == null)
			    return;


		    if (args.Parameters.Count < 1)
		    {
			    ply.SendMessage("Valid commands are:", Color.Red);
			    ply.SendMessage("/kit <kit name>", Color.Red);
			    return;
		    }


		    String kitname = args.Parameters[0];

		    Kit k = Kits.FindKit(kitname);

		    if (k == null)
		    {
			    ply.SendMessage(String.Format("The {0} kit does not exist.", kitname), Color.Red);
			    return;
		    }

		    if (ply.Group.HasPermission(k.getPerm()))
		    {
			    var kitPly = ply.GetKitPlayer();
			    var cooldown = kitPly.GetKitCooldown(k);
				if (cooldown <= 0)
			    {
				    k.giveItems(ply);
				    kitPly.UseKit(k.name);
			    }
				else
				{
					ply.SendErrorMessage("The kit {0} is still on cooldown for {1} seconds", k.name, cooldown);
				}
		    }
		    else
		    {
			    ply.SendMessage(String.Format("You do not have access to the {0} kit.", kitname), Color.Red);
		    }
	    }

	    public static void GiveKit( TSPlayer ply, string kitname)
        {
			Kit k = config.Kits.findKit(kitname);
            if( k != null )
            {
                if( ply != null)
                {
                    k.giveItems(ply);
                }
                else
                {
                    Log.ConsoleError("The player specified does not exist.");
                }
            }
            else
            {
                Log.ConsoleError( string.Format("The kit {0} does not exist.", kitname));
            }
        }
    }
}
