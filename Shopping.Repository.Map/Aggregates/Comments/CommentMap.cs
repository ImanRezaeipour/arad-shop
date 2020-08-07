using System.Data.Entity.ModelConfiguration;
using Shopping.DomainModel.Aggregates.Comments.Aggregates;

namespace Shopping.Repository.Map.Aggregates.Comments
{
    public class CommentMap : EntityTypeConfiguration<Comment>
    {
        public CommentMap()
        {
            HasKey(p => p.Id);
        }
    }
}