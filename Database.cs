using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;
using System.IO;
using System.Data.SQLite;

namespace PokemonDatabase
{
    class Database
    {

        public Database()
        {
            if (!File.Exists("Pokemon.db"))
            {
                SQLiteConnection.CreateFile("Pokemon.db");
                using (SQLiteConnection db = new SQLiteConnection("data source=Pokemon.db"))
                {
                    db.Open();

                    string tableCommand = "CREATE TABLE IF NOT " +
                    "EXISTS CardSets (Primary_Key INTEGER PRIMARY KEY AUTOINCREMENT, " +
                    "setNumber integer, setName text, URL text )";
                    SQLiteCommand createTable = new SQLiteCommand(tableCommand, db);

                    createTable.ExecuteReader();

                    tableCommand = "CREATE TABLE IF NOT " +
                    "EXISTS PokemonCards (Primary_Key INTEGER PRIMARY KEY AUTOINCREMENT, " +
                    "setNumber integer, pokemonName text, cardURL text, cardCost REAL, pokemonNumber integer )";
                    createTable = new SQLiteCommand(tableCommand, db);

                    createTable.ExecuteReader();
                }
            }
        }
        public List<int> RetrieveAllSetNumbers()
        {
            List<int> TableSets = new List<int>();
            using (SQLiteConnection db = new SQLiteConnection("data source=Pokemon.db"))
            {
                db.Open();
                string sqlCmd = "SELECT setNumber FROM CardSets";
                SQLiteCommand cmd = new SQLiteCommand(sqlCmd, db);
                
                using (SQLiteDataReader rdr = cmd.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        TableSets.Add(Convert.ToInt32(rdr[0].ToString()));
                    }
                }
            }
            return TableSets;
        }
        public List<string> RetrieveAllSetURLs()
        {
            List<string> TableSets = new List<string>();
            using (SQLiteConnection db = new SQLiteConnection("data source=Pokemon.db"))
            {
                db.Open();
                string sqlCmd = "SELECT URL FROM CardSets";
                SQLiteCommand cmd = new SQLiteCommand(sqlCmd, db);

                using (SQLiteDataReader rdr = cmd.ExecuteReader())
                {
                    while (rdr.Read()) { TableSets.Add(rdr[0].ToString()); }
                }
            }
            return TableSets;
        }
        public void AddNames(List<CardName> CardNames)
        {
            try
            {
                List<string> TableSets = new List<string>();
                SQLiteCommand cmd;

                using (SQLiteConnection db = new SQLiteConnection("data source=Pokemon.db"))
                {
                    db.Open();

                    for (int i = 0; i < CardNames.Count; i++)
                    {
                        if (!TableSets.Contains(CardNames[i].Name))
                        {
                            cmd = new SQLiteCommand("INSERT INTO CardSets (setNumber, pokemonName, cardURL, cardCost, pokemonNumber) VALUES (@num,@name,@url,@cost,@cardnum)", db);
                            cmd.Parameters.AddWithValue("@num", CardNames[i].CardSet);
                            cmd.Parameters.AddWithValue("@name", CardNames[i].Name);
                            cmd.Parameters.AddWithValue("@url", CardNames[i].URL);
                            cmd.Parameters.AddWithValue("@cost", CardNames[i].Price);
                            cmd.Parameters.AddWithValue("@cardnum", CardNames[i].CardNum);

                            cmd.ExecuteNonQuery();
                        }
                    }
                }

            }
            catch (Exception e) { Console.Write("Exception information: {0}", e); }
        }
        public void AddSets(List<CardSet> CardSets)
        {
            try
            {
                List<string> TableSets = new List<string>();
                SQLiteCommand cmd;
                string sqlCmd;
                
                using (SQLiteConnection db = new SQLiteConnection("data source=Pokemon.db"))
                {
                    db.Open();
                    sqlCmd = "SELECT setName FROM CardSets";
                    cmd = new SQLiteCommand(sqlCmd, db);
                    #region This is going to grab a list of all Card Sets in the table currently
                    using (SQLiteDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            TableSets.Add(rdr[0].ToString());
                        }
                    }
                    #endregion
                    for (int i = 0; i < CardSets.Count; i++)
                    {
                        if (!TableSets.Contains(CardSets[i].Name))
                        {
                            cmd = new SQLiteCommand("INSERT INTO CardSets (setNumber, setName, URL) VALUES (@name,@url)", db);
                            
                            cmd.Parameters.AddWithValue("@name", CardSets[i].Name);
                            cmd.Parameters.AddWithValue("@url", CardSets[i].URL);
                            cmd.ExecuteNonQuery();
                        }
                    }
                }

            }
            catch(Exception e) { Console.Write("Exception information: {0}", e); }
        }

    }
    
}
