using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PokemonDatabase
{
    class GenerateCardNames
    {
        private string SetURL { get; set; }
        private int SetIDKey { get; set; }

        public GenerateCardNames(string _setURL, int _setIDKey)
        {
            SetURL = _setURL;
            SetIDKey = _setIDKey;
        }
        public List<CardName> GetCardsNamesPokellector()
        {
            List<CardName> lCards = new List<CardName>();
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(SetURL);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            if (response.StatusCode == HttpStatusCode.OK)
            {
                Stream receiveStream = response.GetResponseStream();
                StreamReader readStream = null;

                if (response.CharacterSet == null) { readStream = new StreamReader(receiveStream); }
                else { readStream = new StreamReader(receiveStream, Encoding.GetEncoding(response.CharacterSet)); }

                string data = readStream.ReadToEnd();
                Regex cardNamesAndNums = new Regex("(?<=<div class=\"plaque\">)[ -zé]*(?=</div>)");
                Regex cardURL = new Regex("(?<=<a href=\")[ -zé]*(?=\" name=)");
                MatchCollection cNANMatches = cardNamesAndNums.Matches(data);
                MatchCollection cURLMatches = cardURL.Matches(data);
                int countOfCards = 0;
                foreach (Match setName in cNANMatches)
                {
                    string alteredSetURL = @"https://www.pokellector.com" + cURLMatches[countOfCards].ToString();
                    string[] nameDivision = setName.ToString().Split(new[] { '-' }, 2);
                    int ACardNum = 0;
                    string ACardName = "";
                    if (nameDivision.Length == 2)
                    {
                        ACardName = nameDivision[1].Trim();
                        ACardNum = Convert.ToInt32(nameDivision[0].Substring(1).Trim());
                    }
                    else
                    {
                        ACardName = nameDivision[0];
                        ACardNum = 0;
                    }
                    lCards.Add(new CardName(SetIDKey, ACardName, ACardNum, alteredSetURL));
                    countOfCards++;
                }
                response.Close();
                readStream.Close();
                foreach (CardName cn in lCards) {   cn.Print(); }

            }
            else
            {
                Console.WriteLine("Oh no");
            }
            return lCards;
        }

        private CardName AssignInfo(string nameString)
        {
            CardName currentCard = new CardName();
            try
            {
                ///pokemon/sm-forbidden-light/mysterious-treasure?xid=i6f83e066ca6c474894c507745ed0f48c" class="product__click-target" onclick="trackProductEvent('productClick', 'click', 'articuno ex team plasma', '83656', 'Pokemon', 2, '');"></a>
                string[] stringToParse;
                stringToParse = nameString.Replace("\" class=\"product", "^").Split('^');
                currentCard.URL = "https://shop.tcgplayer.com" + stringToParse[0];
                currentCard.CardSet = SetIDKey;
            }
            catch
            {
            }
            return currentCard;
        }
    }
}
