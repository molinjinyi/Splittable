using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ConsoleApplication1.Data;
using Dapper;
namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        /// <summary>
        /// 生成数字主键 分布式
        /// </summary>
        [TestMethod]
        public void TestMethod1()
        {
            for (var i = 0; i < 2147483647; i++)
            {
         
                using (var conn = Db.Server2Connection())
                {
                    var id = conn.ExecuteScalar<int>("REPLACE INTO Tickets32 (stub) VALUES ('a');SELECT LAST_INSERT_ID();");
                    Console.WriteLine(id);

                }
            }
        }
        [TestMethod]
        public void TestMethod2()
        {
            for (var i = 0; i < 2; i++)
            {
                Console.WriteLine(Guid.NewGuid());
            }
        }
    }

}
