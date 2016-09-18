using ConsoleApplication1.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1.Data
{
    public class CreateTableContext: DbContext
    {
        public CreateTableContext() : base("ShardCatalog")
        {
            Database.SetInitializer<CreateTableContext>(null);
            Database.CreateIfNotExists();
        }
        public DbSet<FragmentTable> FragmentTable { get; set; }
        public DbSet<Partition> Partition { get; set; }
        public DbSet<Shard> Shard { get; set; }
        public DbSet<ShardGroup> ShardGroup { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Properties<string>().Configure(c => c.HasMaxLength(128));
            base.OnModelCreating(modelBuilder);
        }
    }
}
