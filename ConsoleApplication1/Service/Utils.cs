using ConsoleApplication1.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
namespace ConsoleApplication1.Service
{
    public class Utils
    {
        public static void Execute(string sql)
        {
            using (var conn = Db.ShardCatalogConnection())
            {
                conn.Execute(sql);
            }
        }
    }
}
