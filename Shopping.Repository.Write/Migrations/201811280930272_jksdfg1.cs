namespace Shopping.Repository.Write.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class jksdfg1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.SettlingItem", "Shop_Id", "dbo.Shop");
            DropForeignKey("dbo.OrderSuggestion", "Shop_Id", "dbo.Shop");
            DropForeignKey("dbo.Factor", "Shop_Id", "dbo.Shop");
            DropForeignKey("dbo.Complaint", "Shop_Id", "dbo.Shop");
            DropForeignKey("dbo.PrivateOrder", "Shop_Id", "dbo.Shop");
            DropForeignKey("dbo.AppInfo", "Person_Id", "dbo.Person");
            DropForeignKey("dbo.Shop", "Id", "dbo.Person");
            DropForeignKey("dbo.Customer", "Id", "dbo.Person");
            DropForeignKey("dbo.CustomerAddress", "Customer_Id", "dbo.Customer");
            DropForeignKey("dbo.OrderBase", "Customer_Id", "dbo.Customer");
            DropForeignKey("dbo.Factor", "Customer_Id", "dbo.Customer");
            DropIndex("dbo.SettlingItem", new[] { "Shop_Id" });
            DropIndex("dbo.AppInfo", new[] { "Person_Id" });
            DropIndex("dbo.CustomerAddress", new[] { "Customer_Id" });
            DropIndex("dbo.OrderBase", new[] { "Customer_Id" });
            DropIndex("dbo.OrderSuggestion", new[] { "Shop_Id" });
            DropIndex("dbo.Factor", new[] { "Customer_Id" });
            DropIndex("dbo.Factor", new[] { "Shop_Id" });
            DropIndex("dbo.Complaint", new[] { "Shop_Id" });
            DropIndex("dbo.Shop", new[] { "Id" });
            DropIndex("dbo.Customer", new[] { "Id" });
            DropIndex("dbo.PrivateOrder", new[] { "Shop_Id" });
            DropPrimaryKey("dbo.Shop");
            DropPrimaryKey("dbo.Person");
            DropPrimaryKey("dbo.Customer");
            AlterColumn("dbo.SettlingItem", "Shop_Id", c => c.Long());
            AlterColumn("dbo.Shop", "Id", c => c.Long(nullable: false));
            AlterColumn("dbo.Person", "Id", c => c.Long(nullable: false));
            AlterColumn("dbo.Customer", "Id", c => c.Long(nullable: false));
            AlterColumn("dbo.AppInfo", "Person_Id", c => c.Long());
            AlterColumn("dbo.ProductSuggestion", "PersonId", c => c.Long(nullable: false));
            AlterColumn("dbo.CustomerAddress", "Customer_Id", c => c.Long());
            AlterColumn("dbo.OrderBase", "Customer_Id", c => c.Long());
            AlterColumn("dbo.PrivateOrder", "Shop_Id", c => c.Long());
            AlterColumn("dbo.OrderSuggestion", "Shop_Id", c => c.Long());
            AlterColumn("dbo.PrivateMessage", "PersonId", c => c.Long(nullable: false));
            AlterColumn("dbo.Factor", "Customer_Id", c => c.Long());
            AlterColumn("dbo.Factor", "Shop_Id", c => c.Long());
            AlterColumn("dbo.Complaint", "Shop_Id", c => c.Long());
            AddPrimaryKey("dbo.Shop", "Id");
            AddPrimaryKey("dbo.Person", "Id");
            AddPrimaryKey("dbo.Customer", "Id");
            CreateIndex("dbo.SettlingItem", "Shop_Id");
            CreateIndex("dbo.AppInfo", "Person_Id");
            CreateIndex("dbo.CustomerAddress", "Customer_Id");
            CreateIndex("dbo.OrderBase", "Customer_Id");
            CreateIndex("dbo.OrderSuggestion", "Shop_Id");
            CreateIndex("dbo.Factor", "Customer_Id");
            CreateIndex("dbo.Factor", "Shop_Id");
            CreateIndex("dbo.Complaint", "Shop_Id");
            CreateIndex("dbo.Shop", "Id");
            CreateIndex("dbo.Customer", "Id");
            CreateIndex("dbo.PrivateOrder", "Shop_Id");
            AddForeignKey("dbo.SettlingItem", "Shop_Id", "dbo.Shop", "Id");
            AddForeignKey("dbo.OrderSuggestion", "Shop_Id", "dbo.Shop", "Id");
            AddForeignKey("dbo.Factor", "Shop_Id", "dbo.Shop", "Id");
            AddForeignKey("dbo.Complaint", "Shop_Id", "dbo.Shop", "Id");
            AddForeignKey("dbo.PrivateOrder", "Shop_Id", "dbo.Shop", "Id");
            AddForeignKey("dbo.AppInfo", "Person_Id", "dbo.Person", "Id");
            AddForeignKey("dbo.Shop", "Id", "dbo.Person", "Id");
            AddForeignKey("dbo.Customer", "Id", "dbo.Person", "Id");
            AddForeignKey("dbo.CustomerAddress", "Customer_Id", "dbo.Customer", "Id");
            AddForeignKey("dbo.OrderBase", "Customer_Id", "dbo.Customer", "Id");
            AddForeignKey("dbo.Factor", "Customer_Id", "dbo.Customer", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Factor", "Customer_Id", "dbo.Customer");
            DropForeignKey("dbo.OrderBase", "Customer_Id", "dbo.Customer");
            DropForeignKey("dbo.CustomerAddress", "Customer_Id", "dbo.Customer");
            DropForeignKey("dbo.Customer", "Id", "dbo.Person");
            DropForeignKey("dbo.Shop", "Id", "dbo.Person");
            DropForeignKey("dbo.AppInfo", "Person_Id", "dbo.Person");
            DropForeignKey("dbo.PrivateOrder", "Shop_Id", "dbo.Shop");
            DropForeignKey("dbo.Complaint", "Shop_Id", "dbo.Shop");
            DropForeignKey("dbo.Factor", "Shop_Id", "dbo.Shop");
            DropForeignKey("dbo.OrderSuggestion", "Shop_Id", "dbo.Shop");
            DropForeignKey("dbo.SettlingItem", "Shop_Id", "dbo.Shop");
            DropIndex("dbo.PrivateOrder", new[] { "Shop_Id" });
            DropIndex("dbo.Customer", new[] { "Id" });
            DropIndex("dbo.Shop", new[] { "Id" });
            DropIndex("dbo.Complaint", new[] { "Shop_Id" });
            DropIndex("dbo.Factor", new[] { "Shop_Id" });
            DropIndex("dbo.Factor", new[] { "Customer_Id" });
            DropIndex("dbo.OrderSuggestion", new[] { "Shop_Id" });
            DropIndex("dbo.OrderBase", new[] { "Customer_Id" });
            DropIndex("dbo.CustomerAddress", new[] { "Customer_Id" });
            DropIndex("dbo.AppInfo", new[] { "Person_Id" });
            DropIndex("dbo.SettlingItem", new[] { "Shop_Id" });
            DropPrimaryKey("dbo.Customer");
            DropPrimaryKey("dbo.Person");
            DropPrimaryKey("dbo.Shop");
            AlterColumn("dbo.Complaint", "Shop_Id", c => c.Guid());
            AlterColumn("dbo.Factor", "Shop_Id", c => c.Guid());
            AlterColumn("dbo.Factor", "Customer_Id", c => c.Guid());
            AlterColumn("dbo.PrivateMessage", "PersonId", c => c.Guid(nullable: false));
            AlterColumn("dbo.OrderSuggestion", "Shop_Id", c => c.Guid());
            AlterColumn("dbo.PrivateOrder", "Shop_Id", c => c.Guid());
            AlterColumn("dbo.OrderBase", "Customer_Id", c => c.Guid());
            AlterColumn("dbo.CustomerAddress", "Customer_Id", c => c.Guid());
            AlterColumn("dbo.ProductSuggestion", "PersonId", c => c.Guid(nullable: false));
            AlterColumn("dbo.AppInfo", "Person_Id", c => c.Guid());
            AlterColumn("dbo.Customer", "Id", c => c.Guid(nullable: false));
            AlterColumn("dbo.Person", "Id", c => c.Guid(nullable: false));
            AlterColumn("dbo.Shop", "Id", c => c.Guid(nullable: false));
            AlterColumn("dbo.SettlingItem", "Shop_Id", c => c.Guid());
            AddPrimaryKey("dbo.Customer", "Id");
            AddPrimaryKey("dbo.Person", "Id");
            AddPrimaryKey("dbo.Shop", "Id");
            CreateIndex("dbo.PrivateOrder", "Shop_Id");
            CreateIndex("dbo.Customer", "Id");
            CreateIndex("dbo.Shop", "Id");
            CreateIndex("dbo.Complaint", "Shop_Id");
            CreateIndex("dbo.Factor", "Shop_Id");
            CreateIndex("dbo.Factor", "Customer_Id");
            CreateIndex("dbo.OrderSuggestion", "Shop_Id");
            CreateIndex("dbo.OrderBase", "Customer_Id");
            CreateIndex("dbo.CustomerAddress", "Customer_Id");
            CreateIndex("dbo.AppInfo", "Person_Id");
            CreateIndex("dbo.SettlingItem", "Shop_Id");
            AddForeignKey("dbo.Factor", "Customer_Id", "dbo.Customer", "Id");
            AddForeignKey("dbo.OrderBase", "Customer_Id", "dbo.Customer", "Id");
            AddForeignKey("dbo.CustomerAddress", "Customer_Id", "dbo.Customer", "Id");
            AddForeignKey("dbo.Customer", "Id", "dbo.Person", "Id");
            AddForeignKey("dbo.Shop", "Id", "dbo.Person", "Id");
            AddForeignKey("dbo.AppInfo", "Person_Id", "dbo.Person", "Id");
            AddForeignKey("dbo.PrivateOrder", "Shop_Id", "dbo.Shop", "Id");
            AddForeignKey("dbo.Complaint", "Shop_Id", "dbo.Shop", "Id");
            AddForeignKey("dbo.Factor", "Shop_Id", "dbo.Shop", "Id");
            AddForeignKey("dbo.OrderSuggestion", "Shop_Id", "dbo.Shop", "Id");
            AddForeignKey("dbo.SettlingItem", "Shop_Id", "dbo.Shop", "Id");
        }
    }
}
