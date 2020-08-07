using System.Data.Entity.ModelConfiguration;
using Shopping.DomainModel.Aggregates.ApplicationSettings.Aggregates;

namespace Shopping.Repository.Map.Aggregates.ApplicationSettings
{
    public class ApplicationSettingMap:EntityTypeConfiguration<ApplicationSetting>
    {
        public ApplicationSettingMap()
        {
            HasKey(p => p.Id);
        }
    }
}