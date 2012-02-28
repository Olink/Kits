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
                List<Item> itemList = TShock.Utils.GetItemByIdOrName(i.id);
                if (itemList.Count == 0)
                {
                    Log.ConsoleError(String.Format("The specified item does not exist: {0}", i.id) );
                    continue;
                }
                else if( itemList.Count > 1 )
                {
                    Log.ConsoleError(String.Format("The specified item has multiple entries: {0}.\n Using the first item.", i.id));
                }

                Item item = itemList[0];

                int amount = Math.Min(item.maxStack, i.amt);
                if( item != null )
                    ply.GiveItem(item.type, item.name, item.width, item.height, amount);
            }

            ply.SendMessage( String.Format("{0} kit given. Enjoy!", name), Color.Green );
        }
    }
}
