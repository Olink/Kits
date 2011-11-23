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
    public class Kits : TerrariaPlugin
    {
        public static Dictionary<String, Kit> kits;

        public static Kit FindKit( String name )
        {
            if( kits.ContainsKey( name ) )
            {
                return kits[name];
            }

            return null;
        }

        public static void AddKit( String name, Kit k )
        {
            kits.Add( name, k );
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

        public Kits(Main game)
            : base(game)
        {
            kits = new Dictionary<string, Kit>();
            String file = Path.Combine(TShock.SavePath, "kits.txt");
            if (File.Exists(file))
            {
                using (var sr = new StreamReader(file))
                {
                    String line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        String[] info = line.Split();
                        if (info.Length >= 4)
                        {
                            String pName = info[0];
                            int l = 0;
                            int.TryParse(info[1], out l);
                            String per = info[2];
                           
                        }
                    }
                }
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
    
        public static void ClearKits()
        {
 	        kits.Clear();
        }

    }
}
