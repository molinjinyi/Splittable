using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Configuration;

namespace ConsoleApplication1.Data
{
    public class Db
    {
        private readonly static string connectionString1;
        private readonly static string connectionString2;
        private readonly static string shardCatalogString2;
        private readonly static string dynamicConnectionString;
        static Db()
        {
            connectionString1 = WebConfigurationManager.ConnectionStrings["TicketServer1"].ConnectionString;
            connectionString2 = WebConfigurationManager.ConnectionStrings["TicketServer2"].ConnectionString;
            shardCatalogString2 = WebConfigurationManager.ConnectionStrings["ShardCatalog"].ConnectionString;
            dynamicConnectionString = WebConfigurationManager.ConnectionStrings["Dynamic"].ConnectionString;
        }

        public static IDbConnection GetClosedConnection(string connectionString)
        {
            return new MySqlConnection(connectionString);
        }

        public static IDbConnection Server1Connection() {
            return GetClosedConnection(connectionString1);
        }
        public static IDbConnection Server2Connection()
        {
            return GetClosedConnection(connectionString2);
        }

        public static IDbConnection ShardCatalogConnection()
        {
            return GetClosedConnection(shardCatalogString2);
        }

        public static IDbConnection GetOpenConnection(string connectionString)
        {
           
            var connection = new MySqlConnection(connectionString);
            connection.Open();
            return connection;
        }
    }
}
