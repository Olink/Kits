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
    [APIVersion(1, 9)]
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
            get { return new Version("1.2"); }
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

        public Kits(Main game)
            : base(game)
        {
            savepath = Path.Combine(TShockAPI.TShock.SavePath, "kits.cfg");
            
            KitReader reader = new KitReader();
            if (File.Exists(savepath))
            {
                kits = reader.readFile(savepath);
                Console.WriteLine(kits.kits.Count + " kits have been loaded.");
            }
            else
            {
                kits = reader.writeFile(savepath);
                Console.WriteLine( "Basic kit file being created.  1 kit containing copper armor created. ");
            }
        }

        public override void Initialize()
        {
            ServerHooks.Chat += HandleCommand;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                ServerHooks.Chat -= HandleCommand;
            }
        }

        private void HandleCommand(messageBuffer buff, int i, String command, HandledEventArgs args)
        {
            TSPlayer ply = TShock.Players[buff.whoAmI];
            if (ply == null)
                return;

            String commandline = command;
            String[] tokens = command.Split();

            if (commandline.Length > 0 && tokens[0] == "/kit")
            {
                args.Handled = true;
                tokens = commandline.Trim().Split();

                for (int k = 0; k < tokens.Length; k++)
                {
                    tokens[k] = tokens[k].ToLower().Trim();
                }

                if( tokens.Length == 1 )
                {
                    ply.SendMessage("Valid commands are:", Color.Red);
                    ply.SendMessage("/kit reload", Color.Red);
                    ply.SendMessage("/kit <kit name>", Color.Red);
                    return;
                }
                
                Cmd cmd = Cmd.findCmd(tokens[1]);

                if (cmd != null)
                {
                    cmd.token(tokens);
                    cmd.setPlayer(ply);
                    cmd.exec();
                }
                else
                {
                    cmd = new GiveCmd();
                    cmd.token(tokens);
                    cmd.setPlayer(ply);
                    cmd.exec();
                }
            }

        }
    }
}
