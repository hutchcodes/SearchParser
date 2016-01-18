using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SearchParser
{
    public class LuceneParser
    {
        public static string ParseSearch(string userSearch)
        {
            var luceneSearch = new StringBuilder();
            var regEx = new Regex(@"[+-]*[""]*[\S]+""*");
            var words = regEx.Matches(userSearch);

            for (var i = 0; i < words.Count; i++)
            {
                var word = words[i].Value;

                if (word == "OR")
                {
                    continue;
                }

                if (luceneSearch.Length > 0)
                {
                    if (word.StartsWith("+"))
                    {
                        luceneSearch.Append(" ");
                    }
                    else
                    {
                        luceneSearch.Append(" OR ");
                    }
                }

                if (word.StartsWith("\"") || word.StartsWith("+\"") || word.StartsWith("-\""))
                {
                    var phrase = "";
                    for (var x = i; x < words.Count; x++)
                    {
                        phrase += cleanWord(words[x].Value) + " ";
                        if (words[x].Value.EndsWith("\""))
                        {
                            word = phrase.TrimEnd(new char[] { ' ' });
                            i = x;
                            break;
                        }

                        if (x == words.Count - 1)
                        {
                            word = word.Remove(0, 1);
                        }
                    }
                }

                word = cleanWord(word);
                
                luceneSearch.Append(word);
            }
            return luceneSearch.ToString();
        }

        private static string cleanWord(string word)
        {
            var newWord = new StringBuilder();
            var wordArray = word.ToArray();
            for (var i = 0; i < wordArray.Length; i++)
            {
                if (wordArray[i] == '"')
                {
                    if (i != 0 && i != wordArray.Length - 1)
                    {
                        continue;
                    }
                }

                newWord.Append(wordArray[i]);

            }
            return newWord.ToString();
        }
    }
}
