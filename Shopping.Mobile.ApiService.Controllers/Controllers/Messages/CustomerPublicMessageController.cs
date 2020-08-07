﻿using System.Web.Http;
using Shopping.Infrastructure.SeedWorks;
using Shopping.Mobile.ApiService.Controllers.SeedWorks;
using Shopping.QueryService.Interfaces.Messages;

namespace Shopping.Mobile.ApiService.Controllers.Controllers.Messages
{
    public class CustomerPublicMessageController : ApiMobileControllerBase
    {
        private readonly IMessageQueryService _messageQueryService;
        public CustomerPublicMessageController(IMessageQueryService messageQueryService)
        {
            _messageQueryService = messageQueryService;
        }
        public IHttpActionResult Get([FromUri]PagedInputDto pagedInput)
        {
            return Ok(_messageQueryService.GetCustomerPublicMessages(pagedInput));
        }
    }
}