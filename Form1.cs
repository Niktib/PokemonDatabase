using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
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
            List<CardSet> lSets = GCS.GetCardsSets();

            if (lSets != null) { db.AddSets(lSets); }
        }

        private void Get_Card_Names_Click(object sender, EventArgs e)
        {
            
            Database db = new Database();
            List<string> setURLs = db.RetrieveAllSetURLs();
            List<int> setNums = db.RetrieveAllSetNumbers();
            for (int i = 0; i < 1; i++)
            {
                GenerateCardNames GCN = new GenerateCardNames(setURLs[i], setNums[i]);
                List<CardName> lNames = GCN.GetCardsNames();

                if (lNames != null) { db.AddNames(lNames); }
            }
        }
    }
}
