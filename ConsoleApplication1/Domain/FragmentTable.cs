using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1.Domain
{
    /// <summary>
    /// 片段表
    /// </summary>
   public class FragmentTable
    {
        public int Id { get; set; }


        public string Name { get; set; }


        public int StartId { get; set; }

        public int EndId { get; set; }


        public int ShardId { get; set; }
    }
}
