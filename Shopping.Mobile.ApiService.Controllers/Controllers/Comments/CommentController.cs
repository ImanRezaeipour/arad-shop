using System.Threading.Tasks;
using System.Web.Http;
using Shopping.Commands.Commands.Comments.Commands;
using Shopping.Commands.Commands.Comments.Responses;
using Shopping.Infrastructure.Core.CommandBus;
using Shopping.Infrastructure.SeedWorks;
using Shopping.Mobile.ApiService.Controllers.SeedWorks;

namespace Shopping.Mobile.ApiService.Controllers.Controllers.Comments
{
    public class CommentController : ApiMobileControllerBase
    {
        public CommentController(ICommandBus bus) : base(bus)
        {
        }
        public async Task<IHttpActionResult> Post(CreateCommentCommand command)
        {
            var commandResponse = await Bus.Send<CreateCommentCommand, CreateCommentCommandResponse>(command);
            var response = new ResponseModel
            {
                Message = "ثبت نام نظر با موفقیت انجام شد",
                Success = true,
                ResponseData = commandResponse
            };
            return Ok(response);
        }
    }
}