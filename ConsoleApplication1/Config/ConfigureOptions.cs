using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1.Config
{
    public class ConfigureOptions
    {
        public int Mod { get; set; } = 4;




        /// <summary>
        /// 每组分多少条数据   保证 options.EachGroup/ options.Mod  能整除
        /// </summary>
        public int EachGroup { get; set; } = 100000000;

        
    }
}
