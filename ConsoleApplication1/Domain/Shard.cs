using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1.Domain
{
    /// <summary>
    /// 碎片 库
    /// </summary>
    public class Shard
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string HashValue { get; set; }


        public int ShardGroupId { get; set; }


        public IList<FragmentTable> FragmentTables { get; set; }
    }
}
