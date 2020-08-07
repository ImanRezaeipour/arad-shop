using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using Shopping.Infrastructure.Core.EntityFramework;
using Shopping.Repository.Map.Aggregates.Brands;

namespace Shopping.Repository.Read.Context
{
    public class ReadOnlyDataContext : DbContext
    {
        public ReadOnlyDataContext() : base("ShoppingContext")
        {
            Initialize();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Conventions.Remove<PluralizingEntitySetNameConvention>();
            modelBuilder.ApplyEntityMapConfigurations(typeof(BrandMap));
        }

        private void Initialize()
        {
            //IReadOnlyContext
            Configuration.LazyLoadingEnabled = true;
            Configuration.ProxyCreationEnabled = true;
            Configuration.ValidateOnSaveEnabled = false;
            Configuration.AutoDetectChangesEnabled = false;
            Database.SetInitializer(new NullDatabaseInitializer<ReadOnlyDataContext>());
        }
        public static void ExecuteMigration()
        {
            ReadOnlyDataContext dx = new ReadOnlyDataContext();
            dx.Database.Initialize(true);
        }
    }
}