using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonDatabase
{
    class CardName
    {
        public int CardNum { get; set; }
        public string URL { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public int CardSet { get; set; }
        public Bitmap CardArt { get; set; }

        public CardName()
        {
            CardNum = 0;
            URL = "";
            Name = "";
            Price = 0;
            CardArt = null;
        }
        public CardName(int _CardSet, string _CardName, int _CardNum, string _URL)
        {
            CardSet = _CardSet;
            CardNum = _CardNum;
            URL = _URL;
            Name = _CardName;
            Price = 0;
            CardArt = null;
        }
        public void Print()
        {
            Console.WriteLine(string.Format("Number {0}, Name {1}, URL {2}", CardNum, Name, URL));
        }

    }
}
