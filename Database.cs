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
                    "setNumber integer, pokemonName text, cardURL text, cardCost REAL, pokemonNumber integer)";
                    createTable = new SQLiteCommand(tableCommand, db);

                    createTable.ExecuteReader();

                    tableCommand = "CREATE TABLE IF NOT " +
                    "EXISTS PokemonCardArt (Primary_Key INTEGER PRIMARY KEY AUTOINCREMENT, " +
                    "pokeNum integer, cardArt BLOB)";
                    createTable = new SQLiteCommand(tableCommand, db);

                    createTable.ExecuteReader();
                }
            }
        }

        public List<CardSet> RetrieveAllSets()
        {
            List<CardSet> list = new List<CardSet>();

            using (SQLiteConnection db = new SQLiteConnection("data source=Pokemon.db"))
            {
                db.Open();

                string stm = "SELECT * FROM CardSets";
                
                using (SQLiteCommand cmd = new SQLiteCommand(stm, db))
                {
                    using (SQLiteDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            string name = rdr.GetString(2);
                            string URL = rdr.GetString(3);
                            int setNum = rdr.GetInt32(1);
                            list.Add(new CardSet(name, URL, setNum));
                        }
                    }
                }
                db.Close();
            }

            return list;

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

                    foreach (CardName cn in CardNames)
                    {
                        if (!TableSets.Contains(cn.Name))
                        {
                            cmd = new SQLiteCommand("INSERT INTO PokemonCards (setNumber, pokemonName, cardURL, cardCost, pokemonNumber) VALUES (@num,@name,@url,@cost,@cardnum)", db);
                            cmd.Parameters.AddWithValue("@num", cn.CardSet);
                            cmd.Parameters.AddWithValue("@name", cn.Name);
                            cmd.Parameters.AddWithValue("@url", cn.URL);
                            cmd.Parameters.AddWithValue("@cost", cn.Price);
                            cmd.Parameters.AddWithValue("@cardnum", cn.CardNum);

                            cmd.ExecuteNonQuery();
                        }
                        else
                        {
                            cmd = new SQLiteCommand("UPDATE PokemonCards " +
                                "SET cardURL = @url, cardCost = @cost, " +
                                "WHERE pokemonName = @name, pokemonNumber = @cardnum, setNumber = @num", db);
                            cmd.Parameters.AddWithValue("@url", cn.URL);
                            cmd.Parameters.AddWithValue("@cost", cn.Price);
                            cmd.Parameters.AddWithValue("@name", cn.Name);
                            cmd.Parameters.AddWithValue("@cardnum", cn.CardNum);
                            cmd.Parameters.AddWithValue("@num", cn.CardSet);

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
                            cmd = new SQLiteCommand("INSERT INTO CardSets (setNumber, setName, URL) VALUES (@number, @name,@url)", db);

                            cmd.Parameters.AddWithValue("@number", CardSets[i].SetNumber);
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
