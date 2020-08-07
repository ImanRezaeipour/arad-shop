using System;
using System.Data.Entity.Migrations;
using Shopping.Infrastructure;
using Shopping.Repository.Write.Context;

namespace Shopping.Repository.Write.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<DataContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }
        protected override void Seed(DataContext context)
        {
            try
            {
                context.Database
                    .ExecuteSqlCommand(
                        $"CREATE SEQUENCE {SqNames.FactorIdSequence} " +
                        "AS BIGINT " +
                        "START WITH 1 " +
                        "MINVALUE 0 " +
                        "MAXVALUE 999999999999999999 " +
                        "INCREMENT BY 1 " +
                        "CACHE 100;"
                    );
               
            }
            catch (Exception e)
            {
            }
            try
            {
                context.Database
                    .ExecuteSqlCommand(
                        $"CREATE SEQUENCE {SqNames.CustomerNumberSequence} " +
                        "AS BIGINT " +
                        "START WITH 1 " +
                        "MINVALUE 0 " +
                        "MAXVALUE 999999999999999999 " +
                        "INCREMENT BY 1 " +
                        "CACHE 100;"
                    );
            
            }
            catch (Exception e)
            {
                
            }
            try
            {
                context.Database
                    .ExecuteSqlCommand(
                        $"CREATE SEQUENCE {SqNames.ShopNumberSequence} " +
                        "AS BIGINT " +
                        "START WITH 1300 " +
                        "MINVALUE 0 " +
                        "MAXVALUE 999999999999999999 " +
                        "INCREMENT BY 1 " +
                        "CACHE 100;"
                    );
            }
            catch (Exception e)
            {
            }

            try
            {
                context.Database
                    .ExecuteSqlCommand(
                        $"CREATE SEQUENCE {SqNames.PromoterSequence} " +
                        "AS BIGINT " +
                        "START WITH 900001 " +
                        "MINVALUE 900001 " +
                        "MAXVALUE 999999 " +
                        "INCREMENT BY 1 " +
                        "CACHE 10;"
                    );
            }
            catch (Exception e)
            {
                
            }
            base.Seed(context);
        }
    }
}
