using System;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using System.Linq;

namespace Shopping.Infrastructure.Core.EntityFramework
{
    public static class ApplyDbContextConfiguration
    {
        public static void ApplyEntityMapConfigurations(this DbModelBuilder modelBuilder, Type type)
        {
            var mapTypes = type.Assembly.GetTypes().Where(t =>
                t.BaseType != null && t.BaseType.IsGenericType &&
                t.BaseType.GetGenericTypeDefinition() == typeof(EntityTypeConfiguration<>)).ToList();
            foreach (var mapType in mapTypes)
            {
                dynamic mapInstance = Activator.CreateInstance(mapType);
                modelBuilder.Configurations.Add(mapInstance);
            }
        }
    }
}
