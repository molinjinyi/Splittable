using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ConsoleApplication1.Service;
using ConsoleApplication1.Domain;
using System.Security.Cryptography;
using System.Text;

namespace UnitTestProject1
{
    /// <summary>
    /// 原因是如果一个字段定义为 CHAR(36), 则MySQL官方的连接器会将其当成 GUID 类型，有些情况下会要求你输入(xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx)形式的字符串，否则会报错。实际上，有时候 某个字段碰巧设为可CHAR(36), 但是我们的本意并非当它是GUID。（例如使用 MySqlDataAdapter 的 Fill 方法填充 DataTable时，就会抛出 Exception。可以使用连接器安装后所附带的 TableEditor 进行观察，重复出这个Bug）
    /// </summary>
    [TestClass]
    public class UnitTest6
    {
        /// <summary>
        /// 插入测试数据
        /// </summary>
        [TestMethod]
        public void TestMethod1()
        {
            for (var i=0;i< 100000001; i++)
            {
                var guid = Guid.NewGuid();
                var salt = guid;
                var password = "123456";


                var account = new Account();
                account.Id = i + 1;
                account.Name = guid.ToString();
                account.Email = string.Format("{0}{1}@qq.com",i,guid);
                account.Password = Md5(password + salt);
                account.Salt = salt;
                account.CreatedOn = DateTime.Now;
                AccountService.AddAccount(account);
            }
        }
        [TestMethod]
        public void TestFindById() {
            var id = 602;
            var account = AccountService.FindAccountById(id);
            Assert.AreEqual("jinyi", account.Name);
        }
        #region utils
        static string Md5(string source)
        {
            using (MD5 md5Hash = MD5.Create())
            {
                string hash = GetMd5Hash(md5Hash, source);

                return hash;
            }
        }
        static string GetMd5Hash(MD5 md5Hash, string input)
        {

            // Convert the input string to a byte array and compute the hash.
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

            // Create a new Stringbuilder to collect the bytes
            // and create a string.
            StringBuilder sBuilder = new StringBuilder();

            // Loop through each byte of the hashed data 
            // and format each one as a hexadecimal string.
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            // Return the hexadecimal string.
            return sBuilder.ToString();
        }

        // Verify a hash against a string.
        static bool VerifyMd5Hash(MD5 md5Hash, string input, string hash)
        {
            // Hash the input.
            string hashOfInput = GetMd5Hash(md5Hash, input);

            // Create a StringComparer an compare the hashes.
            StringComparer comparer = StringComparer.OrdinalIgnoreCase;

            if (0 == comparer.Compare(hashOfInput, hash))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion
    }
}
