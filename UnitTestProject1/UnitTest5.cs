using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ConsoleApplication1.Service;
using System.IO;
using ConsoleApplication1.Data;
using Dapper;
namespace UnitTestProject1
{

    /// <summary>
    /// 
    /// </summary>
    [TestClass]
    public class UnitTest5
    {
        /// <summary>
        /// 生成一组数据库
        /// </summary>
        [TestMethod]
        public void TestMethod1()
        {
            //create db table
            var partitions = ShardCatalogService.GetAllPartitions();
            foreach (var partition in partitions)
            {
                var shardGroups = ShardCatalogService.GetShardGroupsByPartitionId(partition.Id);
                foreach (var shardGroup in shardGroups)
                {
                    var shards = ShardCatalogService.GetShardsByShardGroupId(shardGroup.Id);

                    foreach (var shard in shards)
                    {
                        var dbId = shard.Id;
                        string dbName = String.Format("user{0}", dbId);
                        Utils.Execute(String.Format("Create Database {0}", dbName));

                        var tables = ShardCatalogService.GetFragmentTablesByShardId(shard.Id);
                        var sql = "";
                        using (StreamReader sr = new StreamReader("user.sql"))
                        {
                            sql = sr.ReadToEnd();
                            Utils.Execute(sql);
                            sr.Close();
                            sr.Dispose();
                        }
                        foreach (var table in tables)
                        {
                            using (var conn = Db.GetOpenConnection(string.Format("Server=168.168.1.106;Port=3306;Database={0};Uid=sa;Pwd=sa123456;", dbName)))
                            {
                                conn.Execute(sql.Replace("account", String.Format("account{0}", table.Id)));
                            }
                        }

                    }
                }
            }

        }



        /// <summary>
        /// 删除一组数据库
        /// </summary>
        [TestMethod]
        public void TestDeleteDatabase()
        {
            //create db table
            var partitions = ShardCatalogService.GetAllPartitions();
            foreach (var partition in partitions)
            {
                var shardGroups = ShardCatalogService.GetShardGroupsByPartitionId(partition.Id);
                foreach (var shardGroup in shardGroups)
                {
                    var shards = ShardCatalogService.GetShardsByShardGroupId(shardGroup.Id);

                    foreach (var shard in shards)
                    {
                        var dbId = shard.Id;
                        string dbName = String.Format("user{0}", dbId);
                        Utils.Execute(String.Format("drop database {0}", dbName));
                        
                    }
                }
            }

        }
    }
}
