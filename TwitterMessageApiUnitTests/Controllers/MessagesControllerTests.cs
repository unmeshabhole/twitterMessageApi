using Microsoft.Extensions.Caching.Memory;
using NSubstitute;
using System;
using TwitterMessageApi.Controllers;
using TwitterMessageApi.Dtos;
using TwitterMessageApi.Services;
using Xunit;

namespace TwitterMessageApiUnitTests
{
    public class MessagesControllerTests
    {
        IMessagesService _messagesService;
        MessagesController _sut;

        public MessagesControllerTests()
        {
            _messagesService = Substitute.For<IMessagesService>();
            _sut = new MessagesController(_messagesService);
        }

        [Fact]
        public void MessageController_GetMessages_CallsMessageService_RetrieveMessages_Once()
        {
            var startDate = DateTime.Now.Date.AddDays(-5);
            var endDate = DateTime.Now.Date;
            var results = _sut.Get(startDate, endDate);
            _messagesService.Received(1).RetrieveMessages(startDate, endDate);
        }

        [Fact]
        public void MessageController_PostMessages_CallsMessageService_InsertMessages_Once()
        {
            var message = new Message()
            {
              Email = "a@a.com",
              MessageText = "Hi there",
              Name = "test"
            };

            _sut.Post(message);

            _messagesService.Received(1).InsertMessage(message);
        }
    }
}
