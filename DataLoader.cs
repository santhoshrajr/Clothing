using System;
using System.Collections.Generic;
using System.Text;

namespace docusign2
{
    public class DataLoader
    {
        public static void injectData(string weather, Dictionary<string,Dictionary<string,string>> options, List<string> clothes)
        {
            List<string> clothesOp = ClothesOptions(weather);
            InsertOptions(weather, options, clothesOp);
        }
        public static List<string> ClothesOptions(string temperature)
        {
            List<string> clothes = new List<string>();
            if(temperature.Equals("Hot"))
            {
                clothes.Add("sandals");
                clothes.Add("sun visor");
                clothes.Add("fail");
                clothes.Add("t - shirt");
                clothes.Add("fail");
                clothes.Add("shorts");
                clothes.Add("leaving house");
                clothes.Add("Removing PJs");
            }
            else
            {
                clothes.Add("boots");
                clothes.Add("hat");
                clothes.Add("socks");
                clothes.Add("shirt");
                clothes.Add("jacket");
                clothes.Add("pants");
                clothes.Add("leaving house");
                clothes.Add("Removing PJs");
            }
            return clothes;
        }

        public static void InsertOptions(string weather, Dictionary<string, Dictionary<string, string>> options, List<string> clothes)
        {
            Dictionary<string, string> commands = new Dictionary<string, string>();
            commands.Add("1", clothes[0]);
            commands.Add("2", clothes[1]);
            commands.Add("3", clothes[2]);
            commands.Add("4", clothes[3]);
            commands.Add("5", clothes[4]);
            commands.Add("6", clothes[5]);
            commands.Add("7", clothes[6]);
            commands.Add("8", clothes[7]);
            options.Add(weather, commands);
        }

    }

}
