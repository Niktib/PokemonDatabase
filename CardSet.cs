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

        public CardSet()
        {
            Name = "";
            URL = "";
        }
        public CardSet(string _Name, string _URL)
        {
            Name = _Name;
            URL = _URL;
            URLCreation();
        }

        //Takes the name and generates the URL
        public void URLCreation()
        {
            this.URL = this.URL + "?Type=Cards&orientation=grid";
        }
    }
}
