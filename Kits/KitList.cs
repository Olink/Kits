using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kits
{
    [Serializable]
    public class KitList
    {
        public static List<Kit> kits = new List<Kit>();

        public KitList()
        {
        }

        public static void AddItem( Kit k )
        {
            kits.Add( k );
        }

        public static Kit findKit( String name )
        {
            foreach( Kit k in kits )
            {
                if( k.getName() == name )
                {
                    return k;
                }
            }

            return null;
        }
    }
}
