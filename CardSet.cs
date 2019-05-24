using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonDatabase
{
    class CardSet
    {
        public string URL { get; set; }
        public string Name { get; set; }
        public int SetNumber { get; set; }
        public bool IsSet { get; set; }

        public CardSet()
        {
            Name = "";
            URL = "";
            SetNumber = 0;
            IsSet = true;
        }
        public CardSet(string _Name, string _URL, int _SetNumber)
        {
            Name = _Name;
            URL = _URL;
            SetNumber = _SetNumber;
            IsSet = true;
            if (URL.Contains("tcg")) URLCreation();
        }

        //Takes the name and generates the URL
        private void URLCreation()
        {
            this.URL = this.URL + "?Type=Cards&orientation=grid";
        }
        
        public void Print()
        {
            Console.WriteLine(this.Name + " " + this.URL);
        }
    }
}
