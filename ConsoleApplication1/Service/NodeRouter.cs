using ConsoleApplication1.Config;
using ConsoleApplication1.Data;
using ConsoleApplication1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1.Service
{
    public class NodeRouter
    {
        public static RouterTable GetRouterTable(int id)
        {
            var options = new ConfigureOptions();
            var routerTable = new RouterTable();
            var writeAbleShardGroup = WriteAbleShardGroup.Instance;
            if (id >= writeAbleShardGroup.StartId && id <= writeAbleShardGroup.EndId)
            {
                //find db  想让余数放在哪个库上是可以配置的，按照机器性能不同分配 ，好的多分配点就这样
                var hashValue = id % options.Mod;
                var shards = writeAbleShardGroup.Shards.Select(wasg => new { HashValues = wasg.HashValue.Split(',').Select(int.Parse), wasg.FragmentTables, wasg.Id });

                var shard = shards.Where(s => s.HashValues.Contains(hashValue)).First();
                routerTable.DbNode = shard.Id;


                //find table
                foreach (var fragmentTable in shard.FragmentTables)
                {
                    if (id >= fragmentTable.StartId && id <= fragmentTable.EndId)
                    {
                        routerTable.TableNode = fragmentTable.Id;
                        break;
                    }
                }
            }
            else
            {
                //update writeable
                ShardCatalogService.UpdateShardGroup(id);
                WriteAbleShardGroup.ResetInstance();
                
                routerTable = GetRouterTable(id);
            }
            return routerTable;
        }
    }
}
