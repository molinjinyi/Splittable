using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1.Domain
{
    /// <summary>
    /// 碎片组 集群
    /// </summary>
    public class ShardGroup
    {
        public int Id { get; set; }

        public String Name { get; set; }

        public bool WriteAble { get; set; }


        public int StartId { get; set; }

        public int EndId { get; set; }


        public int PartitionId { get; set; }


        public IList<Shard> Shards { get; set; }
    }
}
