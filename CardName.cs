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
        public int iCardNum { get; set; }
        public string sURL { get; set; }
        public string sName { get; set; }
        public double dPrice { get; set; }
        public int iCardSet { get; set; }
        public Bitmap bitmap { get; set; }

        public CardName()
        {
            iCardNum = 0;
            sURL = "";
            sName = "";
            dPrice = 0;
            bitmap = null;
        }

    }
}
