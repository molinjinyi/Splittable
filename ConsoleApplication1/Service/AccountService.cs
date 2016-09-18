using ConsoleApplication1.Data;
using ConsoleApplication1.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
namespace ConsoleApplication1.Service
{
   public  class AccountService
    {
        public static void AddAccount(Account account)
        {
            if (account == null)
                throw new ArgumentNullException(nameof(account));
            var routerTable = NodeRouter.GetRouterTable(account.Id);
            
            using (var conn = Db.GetClosedConnection(string.Format("Server=168.168.1.106;Port=3306;Database=user{0};Uid=sa;Pwd=sa123456;", routerTable.DbNode)))
            {
                conn.Execute(string.Format("Insert Into account{0}(Id,Name,Email,Password,CreatedOn,Salt) values(@Id,@Name,@Email,@Password,@CreatedOn,@Salt)",routerTable.TableNode),  account );
            }
        }


        public static Account FindAccountById(int id)
        {
            if (id == 0)
                throw new ArgumentNullException(nameof(id));
            var routerTable = NodeRouter.GetRouterTable(id);

            using (var conn = Db.GetClosedConnection(string.Format("Server=168.168.1.106;Port=3306;Database=user{0};Uid=sa;Pwd=sa123456;", routerTable.DbNode)))
            {
               return conn.Query<Account>(string.Format("select * from  account{0} where Id=@Id", routerTable.TableNode), new { id}).FirstOrDefault();
            }
        }
    }
}
