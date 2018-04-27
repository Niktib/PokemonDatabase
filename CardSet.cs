using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonDatabase
{
    class CardSet
    {
        private string name;
        private int setNum;
        private string URL;


        public void setName(string _name) { name = _name; }
        public string getName() { return name; }
        public void setSetNum(int _Num) { setNum = _Num; }
        public int getSetNum() { return setNum; }
        public void setURL(string _URL) { URL = _URL; }
        public string getURL() { return URL; }

        public CardSet()
        {
            name = "";
            setNum = 0;
            URL = "";
        }

        //Takes the name and generates the URL
        public void URLCreation()
        {
            string URLinProgress;

            if (name.Contains('-'))
            {
                URLinProgress = name.Substring(0, 5).Replace(" ", "") + name.Substring(6).Replace(" ", "-");
            }
            else
            {
                URLinProgress = name.Replace(" ", "-");
            }
            this.URL = URLinProgress.Replace(":", "");
        }
    }
}
