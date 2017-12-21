using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Quotes.Models;
using SqlDataAdapter = System.Data.SqlClient.SqlDataAdapter;

namespace Quotes.DB
{
    public class DB
    {
        private static SqlConnection connection = null;
        private static SqlConnection ConnectToDB(SqlConnection connection)
        {
            try
            {
                if (connection == null)
                    connection = new SqlConnection(ConfigurationManager.ConnectionStrings["QuotesDB"].ConnectionString);
                connection.Open();
            }

            catch (Exception)
            {
                //MessageBox.Show("Der Kunne ikke Oprettes Forbindelse til Databaseserveren!");
            }

            return connection;
        }

        private static SqlConnection DisconnectFromDB(SqlConnection connection)
        {
            try
            {
                if (connection != null)
                {
                    if (connection.State == ConnectionState.Open)
                        connection.Close();
                }
            }

            catch (Exception)
            {
                //MessageBox.Show("Der Kunne ikke Oprettes Forbindelse til Databaseserveren!");
            }

            return connection;
        }

        public static IndexViewModel GetIndexViewModel()
        {
            connection = ConnectToDB(connection);

            DataTable QuotesTable = new DataTable();
            List<Quote> quotes = new List<Quote>();


            SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM Quotes", connection);
            adapter.Fill(QuotesTable);

            foreach (DataRow tempquote in QuotesTable.Rows)
            {
                quotes.Add(
                    new Quote
                    {
                        ID = Convert.ToInt32(tempquote["ID"].ToString()),
                        Author = (tempquote["Author"].ToString()),
                        Text = (tempquote["Text"].ToString()),
                        Points = Convert.ToInt32(tempquote["Points"].ToString())
                    }
                  );
            }
            connection = DisconnectFromDB(connection);

            return new IndexViewModel(quotes);
        }

        public static Quote GetQuoteById(int inputID)
        {
            Quote quoteFromDb = new Quote();
            DataTable QuotesTable = new DataTable();

            connection = ConnectToDB(connection);
            SqlDataAdapter adapter = new SqlDataAdapter("SELECT* FROM dbo.Quotes WHERE ID =" + inputID, connection);
            adapter.Fill(QuotesTable);
            foreach (DataRow tempQuote in QuotesTable.Rows)
            {
                quoteFromDb.ID = Convert.ToInt32(tempQuote["ID"].ToString());
                quoteFromDb.Text = tempQuote["Text"].ToString();
                quoteFromDb.Author = tempQuote["Author"].ToString();
                quoteFromDb.Points = Convert.ToInt32((tempQuote["Points"].ToString()));
            }
            connection = DisconnectFromDB(connection);

            return quoteFromDb;
        }

        public static void ChangeQuoteById(Quote inputQuote)
        {
            connection = ConnectToDB(connection);
            SqlCommand command = new SqlCommand("UPDATE Quotes SET Points ="+inputQuote.Points+" WHERE ID ="+inputQuote.ID, connection);
            command.ExecuteNonQuery();
            connection = DisconnectFromDB(connection);
        }

        public static void CreateNewQuote(string  inputText,string inputAuthor)
        {
            connection = ConnectToDB(connection);
            SqlCommand command = new SqlCommand("INSERT INTO Quotes(Text, Author) VALUES('" + inputText + "','" + inputAuthor + "')", connection);
            command.ExecuteNonQuery();
            connection = DisconnectFromDB(connection);
        }
        public static IndexViewModel GetTop10()
        {
            connection = ConnectToDB(connection);

            DataTable QuotesTable = new DataTable();
            List<Quote> quotes = new List<Quote>();


            SqlDataAdapter adapter = new SqlDataAdapter("select top 10 ID, Text, Author, Points from Quotes order by points DESC;", connection);
            adapter.Fill(QuotesTable);

            foreach (DataRow tempquote in QuotesTable.Rows)
            {
                quotes.Add(
                    new Quote
                    {
                        ID = Convert.ToInt32(tempquote["ID"].ToString()),
                        Author = (tempquote["Author"].ToString()),
                        Text = (tempquote["Text"].ToString()),
                        Points = Convert.ToInt32(tempquote["Points"].ToString())
                    }
                );
            }
            connection = DisconnectFromDB(connection);

            return new IndexViewModel(quotes);
        }
        public static IndexViewModel Get10MostRecent()
        {
            connection = ConnectToDB(connection);

            DataTable QuotesTable = new DataTable();
            List<Quote> quotes = new List<Quote>();


            SqlDataAdapter adapter = new SqlDataAdapter("select top 10 ID, Text, Author, Points from Quotes order by ID DESC;", connection);
            adapter.Fill(QuotesTable);

            foreach (DataRow tempquote in QuotesTable.Rows)
            {
                quotes.Add(
                    new Quote
                    {
                        ID = Convert.ToInt32(tempquote["ID"].ToString()),
                        Author = (tempquote["Author"].ToString()),
                        Text = (tempquote["Text"].ToString()),
                        Points = Convert.ToInt32(tempquote["Points"].ToString())
                    }
                );
            }
            connection = DisconnectFromDB(connection);

            return new IndexViewModel(quotes);
        }

    }
}