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
        private string setURL { get; set; }
        private int setIDKey { get; set; }

        public GenerateCardNames(bool WhichSite, string _setURL, int _setIDKey)
        {
            TandT = WhichSite;
            setURL = _setURL;
            setIDKey = _setIDKey;
        }
        public List<CardName> GetCardsNames()
        {
            string[] ttarray = new string[] { "", "", "" };
            string[] tcgArray = new string[] { setURL + "?newSearch=false&Type=Cards&orientation=grid&lu=true&PageNumber=", "gtmData.searchResults = [];", "<script>trackProductsEvent" };
            if (TandT) { return getAllCards(ttarray); } else { return getAllCards(tcgArray); }

        }
        private List<CardName> getAllCards(string[] URL)
        {
            List<CardName> lCards = new List<CardName>();
            for (int i = 1; i < 11; i++)
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(URL[0] + i);
                Console.WriteLine("");
                Console.WriteLine(URL[0] + i);
                Console.WriteLine("");
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    Stream receiveStream = response.GetResponseStream();
                    StreamReader readStream = null;
                    string beWary = "abcdefghijklmnopqrstuvwxyz";
                    string data;

                    if (response.CharacterSet == null) { readStream = new StreamReader(receiveStream); }
                    else { readStream = new StreamReader(receiveStream, Encoding.GetEncoding(response.CharacterSet)); }

                    while ((data = readStream.ReadLine()).Contains(URL[1]) == false) { }
                    

                    while ((data = readStream.ReadLine()).Contains(URL[2]) == false)
                    {

                        if (data.Contains("<a href=\"/pokemon/") && !data.Contains(beWary))
                        {
                            lCards.Add(AssignInfo(TandT, data.Trim().Replace("<a href=\"", "")));
                            beWary = lCards[lCards.Count-1].sURL;
                        }
                    }
                    response.Close();
                    readStream.Close();
                }
                else
                {
                    Console.WriteLine("Oh no"); 
                }
            }

            return lCards;
        }
        private CardName AssignInfo(bool b, string nameString)
        {
            CardName currentCard = new CardName();
            try
            {
                ///pokemon/sm-forbidden-light/mysterious-treasure?xid=i6f83e066ca6c474894c507745ed0f48c" class="product__click-target" onclick="trackProductEvent('productClick', 'click', 'articuno ex team plasma', '83656', 'Pokemon', 2, '');"></a>
                string[] stringToParse;
                if (!b)
                {
                    stringToParse = nameString.Replace("\" class=\"product", "^").Split('^');
                    currentCard.sURL = "https://shop.tcgplayer.com" + stringToParse[0];
                    currentCard.iCardSet = setIDKey;
                }
            }
            catch
            {
            }
            return currentCard;
        }
    }
}
