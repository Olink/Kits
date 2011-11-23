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

            internalKitList kits = new internalKitList();
            List<string> testItems = new List<string>();
            testItems.Add("This");
            testItems.Add("Is");
            testItems.Add("a");
            testItems.Add("test");
            kits.kits.Add(new internalKit("Testkit", testItems));
            tw.Write(JsonConvert.SerializeObject(kits));
            tw.Close();
            //Run this first, get the format for the json, and use that for serializing itself back
            //You should get a test file with the names of objects and stuff, you can re-read that and it'll go back into the format you like
            //Change names as needed to make it look better in the file
        }

        public internalKitList readFile(String file)
        {
            TextReader tr = new StreamReader(file);
            String raw = tr.ReadToEnd();
            tr.Close();

            internalKitList kitList = JsonConvert.DeserializeObject<internalKitList>(raw);
            return kitList;
        }
    }

    class internalKitList
    {
        public List<internalKit> kits;
    }

    class internalKit
    {
        public internalKit(String kitName, List<String> kitItems)
        {
            name = kitName;
            items = kitItems;
        }
        public string name;
        public List<String> items;
    }
}
