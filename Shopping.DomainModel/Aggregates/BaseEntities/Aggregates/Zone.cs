using Shopping.Infrastructure.Core.Domain.Entities;

namespace Shopping.DomainModel.Aggregates.BaseEntities.Aggregates
{
    public class Zone : Entity<long>
    {
        protected Zone()
        {
        }
        public Zone(string title)
        {
            IsActive = true;
            Title = title;
        }
        public string Title { get; set; }
        public bool IsActive { get; private set; }
        public void Active() => IsActive = true;
        public void DeActive() => IsActive = false;
        public override void Validate()
        {
        }
    }
}