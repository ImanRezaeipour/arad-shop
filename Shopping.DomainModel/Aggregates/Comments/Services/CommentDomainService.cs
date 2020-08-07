using System.Linq;
using Shopping.DomainModel.Aggregates.Comments.Aggregates;
using Shopping.DomainModel.Aggregates.Comments.Interfaces;
using Shopping.Infrastructure.Core;
using Shopping.Repository.Write.Interface;

namespace Shopping.DomainModel.Aggregates.Comments.Services
{
    public class CommentDomainService : ICommentDomainService
    {
        private readonly IRepository<Comment> _repository;
        public CommentDomainService(IRepository<Comment> repository)
        {
            _repository = repository;
        }
        public void IsRegisterComment(long factorId)
        {
            if (_repository.AsQuery().Any(p => p.FactorId == factorId))
            {
                throw new DomainException("برای این فاکتور نظر ثبت شده است");
            }
        }
    }
}