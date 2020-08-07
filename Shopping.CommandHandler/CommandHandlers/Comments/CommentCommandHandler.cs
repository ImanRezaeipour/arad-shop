using System;
using System.Threading.Tasks;
using Shopping.AsyncCommanBus.Handling;
using Shopping.Commands.Commands.Comments.Commands;
using Shopping.Commands.Commands.Comments.Responses;
using Shopping.DomainModel.Aggregates.Comments.Aggregates;
using Shopping.DomainModel.Aggregates.Comments.Interfaces;
using Shopping.Repository.Write.Interface;

namespace Shopping.CommandHandler.CommandHandlers.Comments
{
    public class CommentCommandHandler:ICommandHandler<CreateCommentCommand, CreateCommentCommandResponse>
    {
        private readonly ICommentDomainService _commentDomainService;
        private readonly IRepository<Comment> _repository;
        public CommentCommandHandler(ICommentDomainService commentDomainService, IRepository<Comment> repository)
        {
            _commentDomainService = commentDomainService;
            _repository = repository;
        }

        public Task<CreateCommentCommandResponse> Handle(CreateCommentCommand command)
        {
            _commentDomainService.IsRegisterComment(command.FactorId);
            var comment = new Comment(Guid.NewGuid(), command.Degree, command.ItemDegree, command.FactorId);
            _repository.Add(comment);
            return Task.FromResult(new CreateCommentCommandResponse());
        }
    }
}