using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

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
            GenerateCardSets GCS = new GenerateCardSets();
            List<CardSet> lSets = GCS.GetCardsSetsFromPokellector();

            if (lSets != null) { db.AddSets(lSets); }
        }

        private void Get_Card_Names_Click(object sender, EventArgs e)
        {
            
            Database db = new Database();
            List<CardSet> lSets = db.RetrieveAllSets();
            foreach (CardSet cs in lSets)
            {
                GenerateCardNames GCN = new GenerateCardNames(cs.URL, cs.SetNumber);
                List<CardName> lNames = GCN.GetCardsNamesPokellector();

                if (lNames != null) { db.AddNames(lNames); }
                Console.WriteLine("Done with " + cs.URL);
                System.Threading.Thread.Sleep(5000);
            }
        }

        private int RequestDelay()
        {
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            var request = WebRequest.Create(@"https://www.tcgplayer.com/robots.txt");

            using (var response = request.GetResponse())
            using (var content = response.GetResponseStream())
            using (var reader = new StreamReader(content))
            {
                var strContent = reader.ReadToEnd().ToLower();
                foreach (var line in strContent.Split('\n'))
                {
                    if (line.Contains("crawl-delay"))
                    {
                        return Int32.Parse(Regex.Match(line, @"\d+$").ToString());
                    }
                }
            }
            return 0;
        }
    }
}
