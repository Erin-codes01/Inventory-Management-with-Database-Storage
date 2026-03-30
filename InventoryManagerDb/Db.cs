using System;
using System.Data;
using System.Data.SQLite;

namespace InventoryManagerDb
{
    public static class Db
    {
        private const string ConnStr = "Data Source=Inventory.db;Version=3;";

        public static void EnsureCreated()
        {
            using var conn = new SQLiteConnection(ConnStr);
            conn.Open();
			//Write a Query as a string that will create the Table if it does not exist.
			//PK should be an autoincrementing INT. Name and category are Text, not null
			//Price and Quantity are Not Null Integers. Price should be real

			
            //Use this to execute your string which should be named sql
			using var cmd = new SQLiteCommand(sql, conn);
            cmd.ExecuteNonQuery();
        }

        public static DataTable GetAll()
        {
            using var conn = new SQLiteConnection(ConnStr);
            conn.Open();
			//Make a var that takes the result of a SQLiteDataAdatper and pass in the query and conn.
			//Make a DataTable, Fill it with the contents of the var and return the table
            
        }

        public static DataTable Search(string? nameLike, string? categoryLike)
        {
            using var conn = new SQLiteConnection(ConnStr);
            conn.Open();

			//Create a string named sql for a select statement then create a var for the SQL command
            

			//Create some validation to add parameters if the name or category is unclear
            
			
			//Make the query order by name and execute. Use += on your string to concatenate more query parameters


			//Load the results into a DataTable and return the table
            
			
        }

        public static void Insert(Product p)
        {
            
			using var conn = new SQLiteConnection(ConnStr);
            conn.Open();
            
			//Create a string named sql with an insert statement. 
			
			//Then make a command and add insert parameters
			
			//Use ExecuteNonQuery() for this one because it does not return anything
			
        }

        public static void Update(Product p)
        {
            //This makes sure that you have a positive productID
			if (p.Id <= 0) throw new ArgumentException("Valid Id required to update.");
            using var conn = new SQLiteConnection(ConnStr);
            conn.Open();
            
			//Create a string named sql with an Update query
			
			//Then create a command and add the update parameters
			
			//Use ExecuteNonQuery() to run this one
            
        }

        public static void Delete(long id)
        {
            //No change needed, use this as an example for the SQL Syntax
			using var conn = new SQLiteConnection(ConnStr);
            conn.Open();
            using var cmd = new SQLiteCommand("DELETE FROM Products WHERE Id=@id;", conn);
            cmd.Parameters.AddWithValue("@id", id);
            cmd.ExecuteNonQuery();
        }
    }
}
