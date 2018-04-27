using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;
using System.IO;

namespace PokemonDatabase
{
    class Database
    {
        public Database()
        {
            
        }


        public void InitializeDatabase()
        {
            if (File.Exists("Pokemon.db"))
            {
                SqliteConnection db = new SqliteConnection("Filename=Pokemon.db");
                db.Open();
            }
            else
            {
                using (SqliteConnection db =
                    new SqliteConnection("Filename=Pokemon.db"))
                {
                    db.Open();

                    string tableCommand = "CREATE TABLE IF NOT " +
                    "EXISTS CardSets (Primary_Key INTEGER PRIMARY KEY AUTOINCREMENT, " +
                    "setNumber integer, setName text, URL text )";
                    SqliteCommand createTable = new SqliteCommand(tableCommand, db);

                    createTable.ExecuteReader();

                    tableCommand = "CREATE TABLE IF NOT " +
                    "EXISTS PokemonCards (Primary_Key INTEGER PRIMARY KEY AUTOINCREMENT, " +
                    "setNumber integer, pokemonName text, cardCost REAL )";
                    createTable = new SqliteCommand(tableCommand, db);


                    createTable.ExecuteReader();
                }
            }
        }
    }
    
}
