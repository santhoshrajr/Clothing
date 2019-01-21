using System;
using System.Collections.Generic;
using System.Text;

namespace docusign2
{
    class Program
    {
        static void Main(string[] args)
        {
            // Console.WriteLine("Hello World!");
            /*Inputs: 1..Temperature Type(one of the following) •	HOT •	COLD
            2.Comma separated list of numeric commands

            Command Description HOT Response COLD Response
            1       Put on footwear     “sandals”	    “boots” 
            2       Put on headwear     “sun visor”	    “hat” 
            3       Put on socks fail	        “socks” 
            4       Put on shirt        “t - shirt”	    “shirt” 
            5       Put on jacket fail	        “jacket” 
            6       Put on pants        “shorts”	    “pants” 
            7       Leave house	        “leaving house”	“leaving house” 
            8       Take off pajamas    “Removing PJs”	“Removing PJs”

            Rules: •	Initial state is in your house with your pajamas on 
            •	Pajamas must be taken off before anything else can be put on 
            • Only 1 piece of each type of clothing may be put on 
            •	You cannot put on socks when it is hot
            •	You cannot put on a jacket when it is hot
            •	Socks must be put on before shoes
            •	Pants must be put on before shoes
            •	The shirt must be put on before the headwear or jacket
            •	You cannot leave the house until all items of clothing are on(except socks and a jacket when it’s hot)
            •	If an invalid command is issued, respond with “fail” and stop processing commands


            Examples Success Input: Hot 8,6,4,2,1,7
            Output: Removing PJs, shorts, t -shirt, sun visor, sandals, leaving house
           Input: Cold 8,6,3,4,2,5,1,7
            Output: Removing PJs, pants, socks, shirt, hat, jacket, boots, leaving house */

            Dictionary<string, Dictionary<string, string>> options = new Dictionary<string, Dictionary<string, string>>();
            List<string> tempdata = new List<string>();
            string weather = "Hot";
            DataLoader.injectData(weather, options, tempdata);
            weather = "Cold";
            DataLoader.injectData(weather, options, tempdata);
            string input = Console.ReadLine();
            List<string> inputData = new List<string>();
            if(input.Length >1)
            {
                string[] splitInput = input.Split(" ");

                inputData.Add(splitInput[0]);
                string[] cmdSplit = splitInput[1].Split(",");
                foreach(var str in cmdSplit)
                {
                    inputData.Add(str);
                }
            }
            checkRules(options, inputData);
        }

        private static void checkRules(Dictionary<string, Dictionary<string, string>> options, List<string> inputData)
        {
            /* Rules: •	Initial state is in your house with your pajamas on 
             •	Pajamas must be taken off before anything else can be put on 
             • Only 1 piece of each type of clothing may be put on 
             •	You cannot put on socks when it is hot
             •	You cannot put on a jacket when it is hot
             •	Socks must be put on before shoes
             •	Pants must be put on before shoes
             •	The shirt must be put on before the headwear or jacket
             •	You cannot leave the house until all items of clothing are on(except socks and a jacket when it’s hot)
             •	If an invalid command is issued, respond with “fail” and stop processing commands*/
            Dictionary<string, string> cmdDescription = options[inputData[0].ToString()];
            HashSet<string> seenState = new HashSet<string>();
            string previousState = "";
            if (!inputData[1].ToString().Equals("8"))
            {
                OutputErrorMessage();
                return;
            }
            StringBuilder output = new StringBuilder();
            output.Append(cmdDescription["8"] + ", ");
            seenState.Add("8");
            previousState = "8";
            for (int i = 2; i < inputData.Count; i++)
            {
                
                if (previousState.Equals(inputData[i]))
                {
                    OutputErrorMessage();
                    return;
                }
                else
                {
                    /*•	You cannot put on socks when it is hot
                    •	You cannot put on a jacket when it is hot*/
                    if (cmdDescription[inputData[i].ToString()].Equals("fail"))
                    {
                        OutputErrorMessage();
                        return;
                    }
                    /* •	Socks must be put on before shoes*/
                    if (inputData[i].ToString().Equals("3") && seenState.Contains("1"))
                    {
                        OutputErrorMessage();
                        return;
                    }
                    /*•	Pants must be put on before shoes*/
                    if (inputData[i].ToString().Equals("6") && seenState.Contains("1"))
                    {
                        OutputErrorMessage();
                        return;
                    }
                    /* •The shirt must be put on before the headwear or jacket*/
                    if (inputData[i].ToString().Equals("4") && (seenState.Contains("2") || seenState.Contains("5")))
                    {
                        OutputErrorMessage();
                        return;
                    }
                    if (inputData[i].ToString().Equals("7") && !CheckIfAllClothesWorn(inputData[0].ToString(), seenState))
                    {
                        OutputErrorMessage();
                        return;
                    }
                    previousState = inputData[i].ToString();
                    seenState.Add(inputData[i].ToString());
                    output.Append(cmdDescription[inputData[i].ToString()] + ", ");
                }
            }
            string finalResponse = output.ToString();
            Console.WriteLine(finalResponse.Substring(0,finalResponse.Length-2));
            Console.ReadLine();
        }

        private static bool CheckIfAllClothesWorn(string weather, HashSet<string> seenState)
        {
            if(weather.Equals("Cold"))
            {
                for(int i=1;i<7;i++)
                {
                    if (!seenState.Contains(i.ToString())) return false;
                }
            }
            else
            {
                for(int i =1; i<7; i++)
                {
                    if(!seenState.Contains(i.ToString()))
                    {
                        if( i ==3 || i==5)
                        {
                            continue;
                        }
                        else
                        {
                            return false;
                        }
                    }
                }
            }
            return true;
        }

        private static void OutputErrorMessage()
        {
            Console.WriteLine("fail");
        }
    }
}
