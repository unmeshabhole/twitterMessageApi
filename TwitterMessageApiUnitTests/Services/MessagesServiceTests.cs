using Microsoft.Extensions.Caching.Memory;
using NSubstitute;
using System;
using System.Collections.Generic;
using TwitterMessageApi.Dtos;
using TwitterMessageApi.Services;
using Xunit;

namespace TwitterMessageApiUnitTests
{
    public class MessagesServiceTests
    {
        IMemoryCache _memoryCache;
        IMessagesService _sut;

        public MessagesServiceTests()
        {
            _memoryCache = Substitute.For<IMemoryCache>();
            _sut = new MessagesService(_memoryCache);
        }

        [Fact]
        public void MessageService_RetrieveMessages_Returns_NoMessages_When_No_MessagesStored()
        {
            var startDate = DateTime.Now.Date.AddDays(-5);
            var endDate = DateTime.Now.Date;
            _memoryCache.TryGetValue(Arg.Any<string>(), out Arg.Any<object>()).Returns(false);
            var messages = _sut.RetrieveMessages(startDate, endDate);
            Assert.True(messages.Length == 0);
        }

        [Fact]
        public void MessageService_RetrieveMessages_Returns_ExpectedResult_WhenMessagesAvailable()
        {
            var startDate = DateTime.Now.Date.AddDays(-5);
            var endDate = DateTime.Now.Date;

            _memoryCache.TryGetValue(Arg.Any<string>(), out Arg.Any<List<Message>>()).Returns(x => {
                x[1] = new List<Message>()
                {
                    new Message()
                    {
                        Email = "a@a.com",
                        Name = "test",
                        MessageText = "text",
                        MessagePostedDate = DateTime.Now.AddDays(-1)
                    }
                };
                return true;
            });

            var messages = _sut.RetrieveMessages(startDate, endDate);
            Assert.True(messages.Length == 1);
            Assert.True(messages[0].MessageText == "text");
        }

        [Fact]
        public void MessageService_InsertMessage_Returns_SameMessage_When_NoStoredMessages()
        {
            var message = new Message()
            {
              Email = "a@a.com",
              MessageText = "Hi there",
              Name = "test"
            };

            _memoryCache.TryGetValue(Arg.Any<string>(), out Arg.Any<object>()).Returns(false);
            _sut.InsertMessage(message);
            _memoryCache.Received(1).Set("TwitterMessages", new List<Message>() { message });
        }
    }
}
