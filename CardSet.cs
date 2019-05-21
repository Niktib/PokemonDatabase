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

        public CardSet()
        {
            Name = "";
            URL = "";
            SetNumber = 0;
        }
        public CardSet(string _Name, string _URL)
        {
            Name = _Name;
            URL = _URL;
            SetNumber = 0;
            URLCreation();
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
