using Shopping.Infrastructure.SeedWorks;

namespace Shopping.Commands.Commands.Comments.Commands
{
    public class CreateCommentCommand : ShoppingCommandBase
    {
        public int Degree { get; set; }
        public int ItemDegree { get; set; }
        public long FactorId { get; set; }
    }
}