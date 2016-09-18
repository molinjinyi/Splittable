using ConsoleApplication1.Data;
using ConsoleApplication1.Domain;
using Dapper;
using System.Collections.Generic;

namespace ConsoleApplication1.Service
{
    public class ShardCatalogService
    {
        public static IList<Partition> GetAllPartitions()
        {
            using (var conn = Db.ShardCatalogConnection())
            {
               return conn.Query<Partition>("select * from `partition`").AsList();
            }
        }
        public static IList<ShardGroup> GetShardGroupsByPartitionId(int partitionId)
        {
            using (var conn = Db.ShardCatalogConnection())
            {
                return conn.Query<ShardGroup>("select * from shardgroup where PartitionId=@partitionId", new { partitionId }).AsList();
            }
        }

        public static IList<Shard> GetShardsByShardGroupId(int shardGroupId)
        {
            using (var conn = Db.ShardCatalogConnection())
            {
                return conn.Query<Shard>("select * from `shard` where ShardGroupId=@shardGroupId", new { shardGroupId }).AsList();
            }
        }
        public static IList<FragmentTable> GetFragmentTablesByShardId(int shardId)
        {
            using (var conn = Db.ShardCatalogConnection())
            {
                return conn.Query<FragmentTable>("select * from fragmenttable where ShardId=@shardId", new { shardId }).AsList();
            }
        }

        #region add
        public static void AddPartition(Partition partition)
        {
            using (var conn = Db.ShardCatalogConnection())
            {
                var id = conn.ExecuteScalar<int>("insert into `partition`(Name) values(@Name);SELECT LAST_INSERT_ID();", partition);
                partition.Id = id;
            }
        }


        public static void AddShardGroup(ShardGroup shardGroup)
        {
            using (var conn = Db.ShardCatalogConnection())
            {
                var id = conn.ExecuteScalar<int>("insert into shardgroup(Name,WriteAble,StartId,EndId,PartitionId) values(@Name,@WriteAble,@StartId,@EndId,@PartitionId);SELECT LAST_INSERT_ID();", shardGroup);
                shardGroup.Id = id;
            }
        }


        public static void AddShard(Shard shard)
        {
            using (var conn = Db.ShardCatalogConnection())
            {
                var id = conn.ExecuteScalar<int>("insert into `shard`(Name,HashValue,ShardGroupId) values(@Name,@HashValue,@ShardGroupId);SELECT LAST_INSERT_ID();", shard);
                shard.Id = id;
            }
        }



        public static void AddFragmentTable(FragmentTable fragmentTable)
        {
            using (var conn = Db.ShardCatalogConnection())
            {
                var id = conn.ExecuteScalar<int>("insert into fragmenttable(Name,StartId,EndId,ShardId) values(@Name,@StartId,@EndId,@ShardId);SELECT LAST_INSERT_ID();", fragmentTable);
                fragmentTable.Id = id;
            }
        }
        #endregion
        public static void UpdateShardGroup(int id)
        {
            using (var conn = Db.ShardCatalogConnection())
            {
                conn.Execute("update shardgroup set WriteAble=0 where WriteAble=1;Update shardgroup set WriteAble=1 where @id>=StartId and @id<=EndId", new { id });

            }
        }
    }
}
