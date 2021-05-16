using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using TwitterMessageApi.Dtos;
using TwitterMessageApi.Services;

namespace TwitterMessageApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessagesController : ControllerBase
    {
        private IMessagesService _messagesService;

        public MessagesController(IMessagesService messagesService)
        {
            _messagesService = messagesService;
        }

        // GET api/messages
        [HttpGet]
        public ActionResult<IEnumerable<Message>> Get(DateTime startDate, DateTime endDate)
        {
            return _messagesService.RetrieveMessages(startDate, endDate);
        }

        // POST api/messages
        [HttpPost]
        public void Post([FromBody] Message message)
        {
            _messagesService.InsertMessage(message);
        }
    }
}
