using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Data.Sqlite;
using System.Net;
using System.IO;

namespace PokemonDatabase
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            Console.Write("beginning \n");
        }

        private void Download_Set_Names_Click(object sender, EventArgs e)
        {
            Database db = new Database();
            db.InitializeDatabase();
            List<CardSet> lSets;
            Console.Write("created database \n");

            string[] ttarray = new string[] { "https://www.trollandtoad.com/Pokemon/7061.html", "inline smallFont subCats", "<a onclick='openSets()' class='seeAllCats'>" };
            string[] tcgArray = new string[] { "https://shop.tcgplayer.com/pokemon", "<div class=\"magicSets\" style=\"font-family:Arial;\">", "<BR clear=all>" };
            if (TandT.Checked) {  lSets = getAllSets(ttarray); } else { lSets = getAllSets(tcgArray); }

            Console.Write("Lset size is " + lSets.Count +"\n");
            if (lSets != null) { db.AddSets(lSets); }
        }

        private void Get_Card_Names_Click(object sender, EventArgs e)
        {

        }

        private List<CardSet> getAllSets(string[] URL)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(URL[0]);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            if (response.StatusCode == HttpStatusCode.OK)
            {
                Stream receiveStream = response.GetResponseStream();
                StreamReader readStream = null;
                

                if (response.CharacterSet == null) { readStream = new StreamReader(receiveStream); }
                else { readStream = new StreamReader(receiveStream, Encoding.GetEncoding(response.CharacterSet)); }

                string data;
                while((data = readStream.ReadLine()).Contains(URL[1]) == false) { }

                List<CardSet> lSets = new List<CardSet>();
                int countOfSets = 1;

                while ((data = readStream.ReadLine()).Contains(URL[2]) == false)
                {
                    if (!TandT.Checked && data.Contains("<a href=\"https://shop.tcgplayer.com/pokemon"))
                    {
                        lSets.Add(AssignInfo(TandT.Checked, countOfSets++, data.Trim()));
                    }
                    else if(TandT.Checked)
                    {
                        lSets.Add(AssignInfo(TandT.Checked, countOfSets++, data.Trim()));
                    }

         
                }
                response.Close();
                readStream.Close();

                Console.Write("Got here \n");
                return lSets;
            }
            return null;
        }

        private CardSet AssignInfo(bool b, int i, string nameString)
        {
            CardSet currentSet = new CardSet();
            try
            {
                int offset = 4;
                string[] stringToParse;
                if (nameString.Contains("inline smallFont subCats")) { offset = offset + 48; }
                if (nameString.Contains("subCatAlphaHeader")) { offset = offset + 36; }
                if (b)
                {
                    currentSet.setName(nameString.Substring(offset).Split('>')[1].Trim().Substring(0, nameString.Substring(offset).Split('>')[1].Trim().Length - 3));
                    currentSet.setURL(nameString.Split('\'')[1]);
                }
                else
                {
                    stringToParse = nameString.Replace("&amp;", "&").Replace("</a>", "^").Replace(">", "^").Split('^');
                    currentSet.setName(stringToParse[1]);
                    stringToParse = nameString.Replace("<a href=\"", "^").Replace("?", "^").Split('^');
                    if (stringToParse[1].Contains("\" style")) { stringToParse = nameString.Replace("<a href=\"", "^").Replace("\" style", "^").Split('^'); }
                    currentSet.setURL(stringToParse[1]);
                }
                currentSet.setSetNum(i);
            }
            catch
            {
                Console.Write("Is this the stupid error spot?\n");
            }
            return currentSet;
        }


    }
}
