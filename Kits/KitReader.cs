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

            KitList kits = new KitList();
            List<KitItem> testItems = new List<KitItem>();

            testItems.Add( new KitItem( 76,1 ) );
            testItems.Add( new KitItem( 80,1 ) );
            testItems.Add( new KitItem( 89,1 ) );
            kits.kits.Add(new Kit("basics", null , testItems ));

            tw.Write(JsonConvert.SerializeObject(kits, Formatting.Indented));
            tw.Close();

            return kits;
        }

        public KitList readFile(String file)
        {
            KitList kitList = null;
            using (var tr = new StreamReader(file))
            {
                String raw = tr.ReadToEnd();
                kitList = JsonConvert.DeserializeObject<KitList>(raw);
            }
            return kitList;
        }
    }
}
