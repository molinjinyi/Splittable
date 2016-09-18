using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ConsoleApplication1.Domain;
using System.Collections.Generic;
using ConsoleApplication1.Service;
using ConsoleApplication1.Config;

namespace UnitTestProject1
{
    /// <summary>
    /// 初始化关系表
    /// </summary>
    [TestClass]
    public class UnitTest2
    {
        [TestMethod]
        public void TestMethod1()
        {
            var options = new ConfigureOptions();
            ///按照一般的切分原则，一个单一的数据库会首先进行垂直切分，垂直切分只是将关系密切的表划分在一起，我们把这样分出的一组表称为一个Partition
            var partition = new Partition { Name = "分区1" };

            var shardGroups = new List<ShardGroup>(); 
            //先初始化二十个组
            for (var i=0;i<20;i++) {
                var shardGroup = new ShardGroup { Name = "分组" + (i + 1), WriteAble = (i == 0 ? true : false), StartId = (i* options.EachGroup+1), EndId = (i + 1) * options.EachGroup };
                shardGroups.Add(shardGroup);
            }
            var shards = new List<Shard>();
            //目前先一个库只放一个余数
            for (var i = 0; i < options.Mod; i++)
            {
                var shard = new Shard { Name = "库" + (i + 1), HashValue = i.ToString() };
                shards.Add(shard);
            }


            
            ShardCatalogService.AddPartition(partition);

            foreach (var shardGroup in shardGroups)
            {
                shardGroup.PartitionId = partition.Id;
                ShardCatalogService.AddShardGroup(shardGroup);

                var fragmentTables = new List<FragmentTable>();
                //算一下每个表要装多少数据  装载主键区间  保证 options.EachGroup/ options.Mod  能整除
                for (var i = 0; i < options.Mod; i++)
                {
                    var fragmentTable = new FragmentTable { Name = "表" + (i + 1), StartId = (i) * (options.EachGroup / options.Mod) + shardGroup.StartId, EndId = (i) * (options.EachGroup / options.Mod) + shardGroup.StartId-1 + (options.EachGroup / options.Mod) };
                    fragmentTables.Add(fragmentTable);
                }


                foreach (var shard in shards)
                {
                    shard.ShardGroupId = shardGroup.Id;
                    ShardCatalogService.AddShard(shard);
                    
                    foreach (var fragmentTable in fragmentTables)
                    {
                        fragmentTable.ShardId = shard.Id;
                        ShardCatalogService.AddFragmentTable(fragmentTable);
                    }
                }
            }
        }
    }
}
