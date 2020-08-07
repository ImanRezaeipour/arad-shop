using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using Shopping.Infrastructure.Core;
using Shopping.Infrastructure.Core.EntityFramework;
using Shopping.Repository.Map.Aggregates.Brands;
using Shopping.Repository.Write.Migrations;

namespace Shopping.Repository.Write.Context
{
    public class DataContext : DbContext, IContext
    {
        public DataContext() : base("ShoppingContext")
        {
            Initialize();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Conventions.Remove<PluralizingEntitySetNameConvention>();
            modelBuilder.ApplyEntityMapConfigurations(typeof(BrandMap));
        }

        private void Initialize()
        {
            //IDataContext
            Configuration.LazyLoadingEnabled = true;
            Configuration.ProxyCreationEnabled = true;
            Configuration.ValidateOnSaveEnabled = true;
            Configuration.AutoDetectChangesEnabled = true;
            Database.CommandTimeout = 100000;
            //Database.SetInitializer<DataContext>(null);
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<DataContext, Configuration>());
        }

        public static void ExecuteMigration()
        {
            DataContext dx = new DataContext();
            dx.Database.Initialize(true);
        }
    }
}
