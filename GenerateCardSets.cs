using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;


namespace PokemonDatabase
{
    class GenerateCardSets
    {
        private bool TandT;
        public GenerateCardSets(bool WhichSite)
        {
            TandT = WhichSite;
        }
        public List<CardSet> GetCardsSets()
        {
            string[] ttarray = new string[] { "https://www.trollandtoad.com/Pokemon/7061.html", "inline smallFont subCats", "<a onclick='openSets()' class='seeAllCats'>" };
            string[] tcgArray = new string[] { "https://shop.tcgplayer.com/pokemon", "<div class=\"magicSets\" style=\"font-family:Arial;\">", "<BR clear=all>" };
            if (TandT) { return getAllSets(ttarray); } else { return getAllSets(tcgArray); }

        }
        private List<CardSet> getAllSets(string[] URL)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(URL[0]);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            if (response.StatusCode == HttpStatusCode.OK)
            {
                Stream receiveStream = response.GetResponseStream();
                StreamReader readStream = null;


                if (response.CharacterSet == null) { readStream = new StreamReader(receiveStream); }
                else { readStream = new StreamReader(receiveStream, Encoding.GetEncoding(response.CharacterSet)); }

                string data;
                while ((data = readStream.ReadLine()).Contains(URL[1]) == false) { }

                List<CardSet> lSets = new List<CardSet>();
                int countOfSets = 1;

                while ((data = readStream.ReadLine()).Contains(URL[2]) == false)
                {
                    if (data.Contains("<a href=\"https://shop.tcgplayer.com/pokemon"))
                    {
                        lSets.Add(AssignInfo(countOfSets++, data.Trim()));
                    }
                }
                response.Close();
                readStream.Close();
                return lSets;
            }
            return null;
        }
        private CardSet AssignInfo(int i, string nameString)
        {
            CardSet currentSet = new CardSet();
            try
            {
                string[] stringToParse;
                stringToParse = nameString.Replace("&amp;", "&").Replace("</a>", "^").Replace(">", "^").Split('^');
                currentSet.sName = stringToParse[1];
                stringToParse = nameString.Replace("<a href=\"", "^").Replace("?", "^").Split('^');
                if (stringToParse[1].Contains("\" style")) { stringToParse = nameString.Replace("<a href=\"", "^").Replace("\" style", "^").Split('^'); }
                currentSet.sURL = stringToParse[1];
                currentSet.iSetNum = i;
            }
            catch
            {
            }
            return currentSet;
        }
    }
}
