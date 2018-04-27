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
        }

        private void Download_Set_Names_Click(object sender, EventArgs e)
        {
            Database db = new Database();
            db.InitializeDatabase();

            string[] ttarray = new string[] { "https://www.trollandtoad.com/Pokemon/7061.html", "inline smallFont subCats", "<a onclick='openSets()' class='seeAllCats'>" };
            string[] tcgArray = new string[] { "https://shop.tcgplayer.com/pokemon", "<select id=\"SetName\" name=\"SetName\">", "<div class=\"SearchParameter Floating\">" };
            if (TandT.Checked) { getAllSets(ttarray); } else { getAllSets(tcgArray); }
        }

        private void Get_Card_Names_Click(object sender, EventArgs e)
        {

        }

        private void getAllSets(string[] URL)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(URL[0]);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            if (response.StatusCode == HttpStatusCode.OK)
            {
                Stream receiveStream = response.GetResponseStream();
                StreamReader readStream = null;
                

                if (response.CharacterSet == null) { readStream = new StreamReader(receiveStream); }
                else { readStream = new StreamReader(receiveStream, Encoding.GetEncoding(response.CharacterSet)); }

                string data = "";
                while((data = readStream.ReadLine()).Contains(URL[1]) == false) { }

                List<CardSet> lSets = new List<CardSet>();
                int countOfSets = 1;

                while ((data = readStream.ReadLine()).Contains(URL[2]) == false)
                {
                    lSets.Add(AssignInfo(TandT.Checked, countOfSets++, data));
                }
                response.Close();
                readStream.Close();
                //TODO add connection with database here
            }
        }

        private CardSet AssignInfo(Boolean b, int i, string nameString)
        {
            CardSet currentSet = new CardSet();
            try
            {
                int offset = 4;
                if (nameString.Contains("inline smallFont subCats")) { offset = offset + 48; }
                if (nameString.Contains("subCatAlphaHeader")) { offset = offset + 36; }
                if (b)
                {
                    currentSet.setName(nameString.Substring(offset).Split('>')[1].Trim().Substring(0, nameString.Substring(offset).Split('>')[1].Trim().Length - 3));
                    currentSet.setURL(nameString.Split('\'')[1]);
                }
                else
                {
                    currentSet.setName(nameString.Replace("\"", "#").Replace("&amp", "&").Split('#')[1]);
                    currentSet.URLCreation();
                }
                currentSet.setSetNum(i);
            }
            catch { }
            return currentSet;
        }


    }
}
