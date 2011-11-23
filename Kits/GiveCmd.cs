using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kits
{
    class GiveCmd : Cmd
    {
        public override void exec()
        {
            if (tokens.Length >= 2)
            {
                String kitname = "";
                for (int i = 1; i < tokens.Length; i++)
                {
                    if( kitname != "" )
                    {
                        kitname += " ";
                    }
                    kitname += tokens[i];
                }

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
    }
}
