﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using TShockAPI;

namespace Kits
{
    class ReloadCmd : Cmd
    {
        public override void exec()
        {
            if (ply.Group.HasPermission("kits-reload"))
            {
                KitReader reader = new KitReader();
                //reader.writeFile( Path.Combine( TShockAPI.TShock.SavePath, "kitTest.cfg") );
                Kits.kits = reader.readFile(Path.Combine(TShockAPI.TShock.SavePath, "kitTest.cfg"));
            }
            else
            {
                ply.SendMessage("You do not have access to this command.", Color.Red);
            }
        }
    }
}

