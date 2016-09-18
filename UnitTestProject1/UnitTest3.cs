using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ConsoleApplication1.Service;

namespace UnitTestProject1
{
    /// <summary>
    /// 查看路由表
    /// </summary>
    [TestClass]
    public class UnitTest3
    {
        [TestMethod]
        public void TestMethod1()
        {
            for (var i = 1; i < 4001; i++)
            {
                var routerTable = NodeRouter.GetRouterTable(i);

                Console.WriteLine("id:{0} DB:{1} Table:{2}", i, routerTable.DbNode, routerTable.TableNode);
            }
        }
    }
}
