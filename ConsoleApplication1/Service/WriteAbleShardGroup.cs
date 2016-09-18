using ConsoleApplication1.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1.Service
{
    public class WriteAbleShardGroup
    {
        private static ShardGroup _shardGroup;
        private static readonly object lockObj = new object();
        private WriteAbleShardGroup() { }

        private static ShardGroup LoadShardGroup() {
            var partitions = ShardCatalogService.GetAllPartitions();
            ShardGroup shardGroupInstance = null;
            foreach (var partition in partitions)
            {
                var shardGroups = ShardCatalogService.GetShardGroupsByPartitionId(partition.Id);
                if (shardGroups == null) continue;
                foreach (var shardGroup in shardGroups)
                {
                    if (shardGroup.WriteAble)
                    {
                        shardGroupInstance = shardGroup;
                        break;
                    }
                }
            }
            //load shard
            var shards = ShardCatalogService.GetShardsByShardGroupId(shardGroupInstance.Id);
            shardGroupInstance.Shards = shards;
            foreach (var shard in shards)
            {
                //load fragmentTables
                var fragmentTables = ShardCatalogService.GetFragmentTablesByShardId(shard.Id);
                shard.FragmentTables = fragmentTables;
            }
            return shardGroupInstance;
        }
        public static ShardGroup Instance
        {
            get
            {
                //double check lock 
                if (_shardGroup == null)
                {
                    lock (lockObj)
                    {
                        if (_shardGroup == null)
                        {
                            _shardGroup = LoadShardGroup();
                        }
                    }

                }
                return _shardGroup;
            }
        }
        internal static void ResetInstance()
        {
            if (_shardGroup != null)
            {
                lock (lockObj)
                {
                    if(_shardGroup!=null) _shardGroup = null;
                }
            }
           
        }
    }
}
