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
            
        }


        public void InitializeDatabase()
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
                    "setNumber integer, pokemonName text, cardCost REAL, energyType text )";
                    createTable = new SQLiteCommand(tableCommand, db);

                    createTable.ExecuteReader();
                }
            }

        }
        public void AddSets(List<CardSet> CardSets)
        {
            try
            {
                List<string> TableSets = new List<string>();
                SQLiteCommand cmd;
                string sqlCmd;

                Console.Write("Got into addsets \n");
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
                            Console.Write(rdr[0].ToString());
                        }
                    }
                    #endregion
                    for (int i = 0; i < CardSets.Count; i++)
                    {
                        if (!TableSets.Contains(CardSets[i].getName()))
                        {
                            cmd = new SQLiteCommand("INSERT INTO CardSets (setNumber, setName, URL) VALUES (@num,@name,@url)", db);

                            Console.Write("ready to insert! \n");
                            cmd.Parameters.AddWithValue("@num", CardSets[i].getSetNum());
                            cmd.Parameters.AddWithValue("@name", CardSets[i].getName());
                            cmd.Parameters.AddWithValue("@url", CardSets[i].getURL());
                            cmd.ExecuteNonQuery();
                        }
                    }
                }

            }
            catch(Exception e) { Console.Write("Exception information: {0}", e); }
        }
    }
    
}
