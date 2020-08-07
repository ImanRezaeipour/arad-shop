using System.Data.Entity.ModelConfiguration;
using Shopping.DomainModel.Aggregates.Persons.Aggregates.Abstract;

namespace Shopping.Repository.Map.Aggregates.Persons
{
    public class PersonMap : EntityTypeConfiguration<Person>
    {
        public PersonMap()
        {
            HasKey(p => p.Id);
        }
    }
}