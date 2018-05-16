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

            Console.Write("beginning \n");
        }

        private void Download_Set_Names_Click(object sender, EventArgs e)
        {
            Database db = new Database();
            db.InitializeDatabase();
            GenerateCardSets GCS = new GenerateCardSets(TandT.Checked);
            List<CardSet> lSets = GCS.GetCardsSets();

            if (lSets != null) { db.AddSets(lSets); }
        }

        private void Get_Card_Names_Click(object sender, EventArgs e)
        {

        }
    }
}
