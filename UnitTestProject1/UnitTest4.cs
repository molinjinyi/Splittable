using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using ConsoleApplication1.Service;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest4
    {
        /// <summary>
        /// 从文件读取sql
        /// </summary>
        [TestMethod]
        public void TestMethod1()
        {
            using (StreamReader sr = new StreamReader("user.sql"))
            {
                var sql = sr.ReadToEnd();
                Utils.Execute(sql);
                sr.Close();
                sr.Dispose();
            }

            //Utils.Execute("Create Database TestJinyi");
        }
    }
}
