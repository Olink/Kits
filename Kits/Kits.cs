using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using Hooks;
using TShockAPI;
using Terraria;

namespace Kits
{
    [APIVersion(1, 11)]
    public class Kits : TerrariaPlugin
    {
        public static KitList kits;
        public static String savepath = "";

        public static Kit FindKit( String name )
        {
            return kits.findKit(name);
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
            get { return "Zach Piispanen"; }
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
            savepath = Path.Combine(TShockAPI.TShock.SavePath, "kits.cfg");
            
            KitReader reader = new KitReader();
            if (File.Exists(savepath))
            {
                kits = reader.readFile(Path.Combine(TShockAPI.TShock.SavePath, "kits.cfg"));
                Console.WriteLine(KitList.kits.Count + " kits have been loaded.");
            }
            else
            {
                kits = reader.writeFile(savepath);
                Console.WriteLine( "Basic kit file being created.  1 kit containing copper armor created. ");
            }

            Console.WriteLine( "End of pre initialize");
            
            if (Initialized != null)
                Initialized();
        }

        public override void Initialize()
        {
            Commands.ChatCommands.Add( new Command("", HandleCommand, "kit"));
        }


        private void HandleCommand(CommandArgs args)
        {
            TSPlayer ply = args.Player;
            if (ply == null)
                return;

            
            if( args.Parameters.Count < 1 )
            {
                ply.SendMessage("Valid commands are:", Color.Red);
                ply.SendMessage("/kit reload", Color.Red);
                ply.SendMessage("/kit <kit name>", Color.Red);
                return;
            }

            if( args.Parameters[0] == "reload")
            {
                if (ply.Group.HasPermission("kits-reload"))
                {
                    KitReader reader = new KitReader();
                    Kits.kits = reader.readFile(Path.Combine(TShockAPI.TShock.SavePath, "kits.cfg"));
                }
                else
                {
                    ply.SendMessage("You do not have access to this command.", Color.Red);
                }
                return;
            }
            else
            {
                String kitname = args.Parameters[0];
              
                Kit k = Kits.FindKit(kitname);

                if (k == null)
                {
                    ply.SendMessage(String.Format("The {0} kit does not exist.", kitname), Color.Red);
                    return;
                }

                if (ply.Group.HasPermission(k.getPerm()))
                {
                    k.giveItems(ply);
                }
                else
                {
                    ply.SendMessage(String.Format("You do not have access to the {0} kit.", kitname), Color.Red);
                }

            }
        }

        public static void GiveKit( TSPlayer ply, string kitname)
        {
            Kit k = KitList.findKit(kitname);
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
