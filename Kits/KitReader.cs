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
        public void writeFile(String file)
        {
            TextWriter tw = new StreamWriter(file);

            KitList kits = new KitList();
            List<KitItem> testItems = new List<KitItem>();
            testItems.Add( new KitItem( 91,42 ) );
            testItems.Add( new KitItem( 41,42 ) );
            testItems.Add( new KitItem( 42,1 ) );
            testItems.Add( new KitItem( 1,10 ) );
            kits.kits.Add(new Kit("Testkit", "perm", testItems ));

            testItems = new List<KitItem>();
            testItems.Add(new KitItem(91, 42));
            testItems.Add(new KitItem(41, 42));
            testItems.Add(new KitItem(42, 1));
            testItems.Add(new KitItem(1, 10));
            kits.kits.Add(new Kit("Testkit2", "perm2", testItems));

            tw.Write(JsonConvert.SerializeObject(kits, Formatting.Indented));
            tw.Close();
            //Run this first, get the format for the json, and use that for serializing itself back
            //You should get a test file with the names of objects and stuff, you can re-read that and it'll go back into the format you like
            //Change names as needed to make it look better in the file
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
