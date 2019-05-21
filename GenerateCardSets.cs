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
        public List<CardSet> GetCardsSets()
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
                foreach (string setName in setNamesMatches)
                {
                    string alteredSetName = setName.Replace("&amp;", "&").Replace("&nbsp;", "").Replace("<br>", " ");
                    lSets.Add(new CardSet(alteredSetName, setLinksMatches[countOfSets].ToString()));
                    countOfSets++;
                }
                response.Close();
                readStream.Close();
                return lSets;
            }
            return null;

        }

        private int RequestDelay()
        {
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            var request = WebRequest.Create(@"https://www.tcgplayer.com/robots.txt");

            using (var response = request.GetResponse())
            using (var content = response.GetResponseStream())
            using (var reader = new StreamReader(content))
            {
                var strContent = reader.ReadToEnd().ToLower();
                foreach (var line in strContent.Split('\n'))
                {
                    if (line.Contains("crawl-delay"))
                    {
                        return Int32.Parse(Regex.Match(line, @"\d+$").ToString());
                    }
                }
            }
            return 0;
        }
    }
}
