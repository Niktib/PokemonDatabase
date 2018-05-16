using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace PokemonDatabase
{
    class GenerateCardNames
    {
        private bool TandT;

        public GenerateCardNames(bool WhichSite)
        {
            TandT = WhichSite;
        }
        public List<CardName> GetCardsSets()
        {
            string[] ttarray = new string[] { "", "", "" };
            string[] tcgArray = new string[] { "?newSearch=false&Type=Cards&orientation=list&lu=true&PageNumber=", "gtmData.searchResults = [];", "</script> <div class=\"pageView\">" };
            if (TandT) { return getAllCards(ttarray); } else { return getAllCards(tcgArray); }

        }
        private List<CardName> getAllCards(string[] URL)
        {
            //URL[0] is not correct, need the database URL's to make it work
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

                List<CardName> lCards = new List<CardName>();
                while ((data = readStream.ReadLine()).Contains(URL[2]) == false)
                {
                    if (data.Contains("<a href=\"https://shop.tcgplayer.com/pokemon"))
                    {
                        lCards.Add(AssignInfo(TandT, data.Trim().Replace("<a href=\"https://shop.tcgplayer.com/pokemon", ""), URL[0]));
                    }
                }
                response.Close();
                readStream.Close();
                
                return lCards;
            }
            return null;
        }
        private CardName AssignInfo(bool b, string nameString, string URL)
        {
            CardName currentCard = new CardName();
            try
            {
                ///pokemon/sm-forbidden-light/mysterious-treasure?xid=i6f83e066ca6c474894c507745ed0f48c
                string[] stringToParse;
                if (!b)
                {
                    stringToParse = nameString.Split('/');
                    currentCard.sURL = URL + stringToParse[1];
                }
            }
            catch
            {
                Console.Write("Is this the stupid error spot?\n");
            }
            return currentCard;
        }
    }
}
