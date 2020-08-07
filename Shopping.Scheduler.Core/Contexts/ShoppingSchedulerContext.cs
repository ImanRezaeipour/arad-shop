using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using Shopping.DomainModel.Aggregates.ApplicationSettings.Aggregates;
using Shopping.DomainModel.Aggregates.Discounts.Aggregates;
using Shopping.DomainModel.Aggregates.Discounts.Aggregates.Abstract;
using Shopping.DomainModel.Aggregates.Factors.Aggregates;
using Shopping.DomainModel.Aggregates.Orders.Aggregates;
using Shopping.DomainModel.Aggregates.Orders.Aggregates.Abstract;
using Shopping.DomainModel.Aggregates.OrdersSuggestions.Aggregates;
using Shopping.DomainModel.Aggregates.OrdersSuggestions.Entities;
using Shopping.DomainModel.Aggregates.OrdersSuggestions.Entities.Abstract;
using Shopping.DomainModel.Aggregates.Persons.Aggregates;
using Shopping.DomainModel.Aggregates.Persons.Aggregates.Abstract;
using Shopping.DomainModel.Aggregates.Products.Aggregates;
using Shopping.DomainModel.Aggregates.ShopCustomerSubsetSettlements.Aggregates;

namespace Shopping.Scheduler.Core.Contexts
{
    public class ShoppingSchedulerContext : DbContext
    {
        public ShoppingSchedulerContext() : base("ShoppingContext")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Conventions.Remove<PluralizingEntitySetNameConvention>();
            modelBuilder.Entity<Product>().HasKey(p => p.Id);
            modelBuilder.Entity<Product>().HasMany(p => p.ProductImages);

            modelBuilder.Entity<Factor>().HasKey(p => p.Id);
            modelBuilder.Entity<Factor>().Property(e => e.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            modelBuilder.Entity<Factor>().HasMany(p => p.FactorRaws);
            modelBuilder.Entity<Factor>().HasOptional(p => p.Accounting);
            modelBuilder.Entity<ApplicationSetting>().HasKey(p => p.Id);
            modelBuilder.Entity<ShopCustomerSubsetSettlement>().HasKey(p => p.Id);

            modelBuilder.Entity<OrderBase>().HasKey(p => p.Id);
            modelBuilder.Entity<OrderBase>().HasMany(p => p.OrderItems);
            modelBuilder.Entity<AreaOrder>().ToTable("AreaOrder");
            modelBuilder.Entity<PrivateOrder>().ToTable("PrivateOrder");

            modelBuilder.Entity<OrderSuggestion>().HasKey(p => p.Id);
            modelBuilder.Entity<OrderSuggestion>().HasMany(p => p.OrderSuggestionItems);
            modelBuilder.Entity<OrderSuggestionItemBase>().HasKey(p => p.Id);
            modelBuilder.Entity<OrderSuggestionItemBase>().ToTable("OrderSuggestionItemBase");
            modelBuilder.Entity<NoProductSuggestionItem>().ToTable("NoProductSuggestionItem");
            modelBuilder.Entity<HasProductSuggestionItem>().ToTable("HasProductSuggestionItem");
            modelBuilder.Entity<AlternativeProductSuggestionItem>().ToTable("AlternativeProductSuggestionItem");

            modelBuilder.Entity<Person>().HasKey(p => p.Id);
            modelBuilder.Entity<Shop>().ToTable("Shop")
                .HasMany(item => item.ShopStatusLogs);
            modelBuilder.Entity<Customer>().ToTable("Customer")
                .HasMany(item => item.CustomerAddresses);

            modelBuilder.Entity<DiscountBase>().HasKey(p => p.Id);
            modelBuilder.Entity<DiscountBase>().HasMany(p => p.Sells);
            modelBuilder.Entity<PercentDiscount>().ToTable("PercentDiscount");
            modelBuilder.Entity<PercentDiscount>().HasMany(p=>p.ProductDiscounts);
            modelBuilder.Entity<PercentDiscount>().Property(p => p.Timestamp).IsRowVersion() ;

        }

        public DbSet<Product> Products { get; set; }
        public DbSet<OrderSuggestion> OrderSuggestions { get; set; }
        public DbSet<Person> Persons { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Shop> Shops { get; set; }
        public DbSet<Factor> Factors { get; set; }
        public DbSet<ApplicationSetting> Settings { get; set; }
        public DbSet<ShopCustomerSubsetSettlement> ShopCustomerSubsetSettlements { get; set; }
        public DbSet<AreaOrder> AreaOrder { get; set; }
        public DbSet<PrivateOrder> PrivateOrder { get; set; }
        public DbSet<DiscountBase> DiscountBase { get; set; }
        public DbSet<PercentDiscount> PercentDiscount { get; set; }
    }
}