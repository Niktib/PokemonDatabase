using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonDatabase
{
    class CardSet
    {
        public int iSetNum { get; set; }
        public string sURL { get; set; }
        public string sName { get; set; }

        public CardSet()
        {
            sName = "";
            iSetNum = 0;
            sURL = "";
        }

        //Takes the name and generates the URL
        public void URLCreation()
        {
            string URLinProgress;
            if (sName.Contains('-'))
            {
                URLinProgress = sName.Substring(0, 5).Replace(" ", "") + sName.Substring(6).Replace(" ", "-");
            }
            else
            {
                URLinProgress = sName.Replace(" ", "-");
            }
            this.sURL = URLinProgress.Replace(":", "");
        }
    }
}
