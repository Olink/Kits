using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TShockAPI;
using Terraria;

namespace Kits
{
    public class Kit
    {
        public String name;
        public String perm;
        public List<KitItem> items;

        public Kit( String name, String perm, List<KitItem> items )
        {
            this.name = name.ToLower();
            this.perm = perm;
            this.items = items;
        }

        public String getName()
        {
            return name;
        }

        public String getPerm()
        {
            return perm;
        }

        public void giveItems( TSPlayer ply)
        {
            foreach( KitItem i in items )
            {
                Item item = TShockAPI.Tools.GetItemById(i.id);
                int amount = Math.Min(item.maxStack, i.amt);
                if( item != null )
                    ply.GiveItem(item.type, item.name, item.width, item.height, amount);
            }
        }
    }
}
