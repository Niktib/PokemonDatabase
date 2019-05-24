using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using System.Text.RegularExpressions;

namespace PokemonDatabase
{
    class GenerateCardSets
    {
        public List<CardSet> GetCardsSetsFromPokellector()
        {
            string URL = @"https://www.pokellector.com/sets";
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(URL);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            if (response.StatusCode == HttpStatusCode.OK)
            {
                Stream receiveStream = response.GetResponseStream();
                StreamReader readStream = null;

                if (response.CharacterSet == null) { readStream = new StreamReader(receiveStream); }
                else { readStream = new StreamReader(receiveStream, Encoding.GetEncoding(response.CharacterSet)); }

                string data = readStream.ReadToEnd();
                Regex setLinks = new Regex("(?<=\" href=\"/sets)[ -z]*(?=\" title=\")");
                Regex setNames = new Regex("(?<=.png\" title=\")[ -z]*(?=\"><span>)");
                MatchCollection setLinksMatches = setLinks.Matches(data);
                MatchCollection setNamesMatches = setNames.Matches(data);


                List<CardSet> lSets = new List<CardSet>();
                int countOfSets = 0;

                Console.WriteLine(countOfSets);
                foreach (Match setName in setNamesMatches)
                {
                    string alteredSetURL = @"https://www.pokellector.com/sets" + setLinksMatches[countOfSets].ToString().Split('"')[0];
                    lSets.Add(new CardSet(setName.ToString(), alteredSetURL, countOfSets));
                    countOfSets++;
                }
                Console.WriteLine(countOfSets);
                PrintSets(lSets);
                response.Close();
                readStream.Close();
                return lSets;
            }
            return null;

        }
        public List<CardSet> GetCardsSetsFromTCG()
        {
            string URL =  @"https://shop.tcgplayer.com/pokemon";
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(URL);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            
            if (response.StatusCode == HttpStatusCode.OK)
            {
                Stream receiveStream = response.GetResponseStream();
                StreamReader readStream = null;

                if (response.CharacterSet == null) { readStream = new StreamReader(receiveStream); }
                else { readStream = new StreamReader(receiveStream, Encoding.GetEncoding(response.CharacterSet)); }

                string data = readStream.ReadToEnd();
                Regex setLinks = new Regex("(?<=<a href=\")[!-z]*pokemon[!-z]*(?=\")");
                Regex setNames = new Regex("(?<=</i>)[!-z ]*(?=</a>)");
                MatchCollection setLinksMatches = setLinks.Matches(data);
                MatchCollection setNamesMatches = setNames.Matches(data);


                List<CardSet> lSets = new List<CardSet>();
                int countOfSets = 0;
                foreach (Match setName in setNamesMatches)
                {
                    string alteredSetName = setName.ToString().Replace("&amp;", "&").Replace("&nbsp;", "").Replace("<br>", " ");
                    lSets.Add(new CardSet(alteredSetName, setLinksMatches[countOfSets].ToString(), countOfSets));
                    countOfSets++;
                }
                PrintSets(lSets);
                response.Close();
                readStream.Close();
                return lSets;
            }
            return null;

        }

        private void PrintSets(List<CardSet> lSets)
        {
            foreach (CardSet cs in lSets)
            {
                cs.Print();
            }
            Console.Read();
        }
    }
}
