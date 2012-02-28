using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace Kits
{
    class KitReader
    {
        public KitList writeFile(String file)
        {
            TextWriter tw = new StreamWriter(file);

            KitList kitList = new KitList();

            List<KitItem> testItems = new List<KitItem>();

            testItems.Add( new KitItem( "76", 1 ) );
            testItems.Add( new KitItem( "80", 1 ) );
            testItems.Add( new KitItem( "89", 1 ) );
            KitList.AddItem(new Kit("basics", "default-kit", testItems));

            tw.Write(JsonConvert.SerializeObject(KitList.kits, Formatting.Indented));
            tw.Close();

            return kitList;
        }

        public KitList readFile(String file)
        {
            TextReader tr = new StreamReader(file);
            String raw = tr.ReadToEnd();
            tr.Close();

            KitList kitList = JsonConvert.DeserializeObject<KitList>(raw);
            return kitList;
        }
    }
}
