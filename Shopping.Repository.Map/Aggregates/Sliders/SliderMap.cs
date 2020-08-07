using System.Data.Entity.ModelConfiguration;
using Shopping.DomainModel.Aggregates.Sliders.Aggregates;

namespace Shopping.Repository.Map.Aggregates.Sliders
{
    public class SliderMap:EntityTypeConfiguration<Slider>
    {
        public SliderMap()
        {
            HasKey(p => p.Id);
        }
    }
}