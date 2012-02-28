using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kits
{
    [Serializable]
    public class KitItem
    {
        public string id;
        public int amt;

        public KitItem(String i, int a)
        {
            id = i;
            amt = a;
        }
    }
}
