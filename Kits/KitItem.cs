using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kits
{
    public class KitItem
    {
        public string id;
        public int amt;

        public KitItem(string i, int a)
        {
            id = i;
            amt = a;
        }
    }
}
