using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using TwitterMessageApi.Dtos;

namespace TwitterMessageApi.Services
{
    public interface IMessagesService
    {
        void InsertMessage(Message message);
        Message[] RetrieveMessages(DateTime startDate, DateTime endDate);
    }

    public class MessagesService : IMessagesService
    {
        private IMemoryCache _memoryCache;
        private string cacheKey = "TwitterMessages";

        public MessagesService(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        public void InsertMessage(Message message)
        {
            List<Message> messageList;

            if (!_memoryCache.TryGetValue(cacheKey, out messageList))
            {
                messageList = new List<Message>();
            }

            message.MessagePostedDate = DateTime.Now.Date;
            messageList.Add(message);

            _memoryCache.Set(cacheKey, messageList);
        }

        public Message[] RetrieveMessages(DateTime startDate, DateTime endDate)
        {
            List<Message> messageList;

            if (!_memoryCache.TryGetValue(cacheKey, out messageList))
            {
                return new Message[] { };
            }

            return messageList.FindAll(a => startDate <= a.MessagePostedDate && a.MessagePostedDate <= endDate).ToArray();
        }
    }
}
