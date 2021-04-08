using System;
using Dapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Data.SqlClient;
using System.IO;
using System.Data;
using System.Collections.Generic;
using System.Linq;

namespace Bank
{
    public interface ITrade
    {
        double Value { get; }
        string ClientSector { get; }
    }

    public class Trade : ITrade
    {
        public double Value { get; set;}
        public string ClientSector { get; set;}
    }

    class Program
    {
        public static IConfigurationRoot Configuration { get; set; }

        private static String SqlConnection()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            Configuration = builder.Build();
            return Configuration["ConnectionStrings:DefaultConnection"];
        }

        private List<string> GetRisk(List<ITrade> trades) 
        {
            List<string> tradeCategories = new List<string>();    
            string _con = SqlConnection();
            string risk = "";

            using (IDbConnection db = new SqlConnection(_con))
            {
                db.Open();
                foreach (var t in trades)
                {
                    string query = " SELECT RISK FROM CATEGORIES"; 
                        query += " WHERE SECTOR = UPPER('" +t.ClientSector +"')";
                        query += " AND VALUE_FROM < " +t.Value;
                        query += " AND VALUE_UNTIL > " +t.Value;  

                    var result = db.Query<string>(query);

                    if (result.Count() > 0)
                        risk = result.ToList()[0];
                    else    
                        risk = "UNDEFINED";

                    tradeCategories.Add(risk);
                }
            }
            return tradeCategories;
        }
        
        static void Main(string[] args)
        {  
            Program obj = new Program();

            List<ITrade> portfolio = new List<ITrade>();
            List<string> tradeCategories = new List<string>();

            portfolio.Add(new Trade(){Value = 2000000, ClientSector = "Private"});
            portfolio.Add(new Trade(){Value = 400000, ClientSector = "Public"});
            portfolio.Add(new Trade(){Value = 500000, ClientSector = "Public"});
            portfolio.Add(new Trade(){Value = 3000000, ClientSector = "Public"});

            tradeCategories = obj.GetRisk(portfolio);

            Console.WriteLine(String.Join(", ", tradeCategories));
        }
    }
}