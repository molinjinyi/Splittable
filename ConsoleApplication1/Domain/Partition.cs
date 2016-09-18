using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1.Domain
{
    /// <summary>
    /// 分区
    /// </summary>
    public class Partition
    {
        public int Id { get; set; }

        public String Name { get; set; }


        public IList<ShardGroup> ShardGroups { get; set; }
    }
}
