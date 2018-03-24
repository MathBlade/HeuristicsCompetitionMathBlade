using HeuristicsCompetitionMathBlade.Classes.Grid_Classes;
using System;
using System.Collections.Generic;
using System.Text;

namespace HeuristicsCompetitionMathBlade.Classes
{
    public static class DictionaryBuilder
    {

        public static Dictionary<string,string> BuildParameterDictionary(string[] args)
        {
            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            for (int index = 0; index < args.Length; index += 2)
            {
                dictionary.Add(args[index], args[index + 1]);
            }

            //Setting default in dictionary if it doesn't exist.
            if (!dictionary.ContainsKey("-" + Grid.DISTANCE_WORD))
            {
                dictionary.Add("-" + Grid.DISTANCE_WORD, true.ToString());
            }
           
            //-s Start -f Stop
            if (!dictionary.ContainsValue(Node.START_WORD))
            {
                if (!dictionary.ContainsKey("-s"))
                {
                    dictionary.Add("-s", Node.START_WORD);

                }
            }
            if (!dictionary.ContainsValue(Node.END_WORD))
            {
                if (!dictionary.ContainsKey("-f"))
                {
                    dictionary.Add("-f", Node.END_WORD);
                }
            }
            return dictionary;
        }

    }
}
