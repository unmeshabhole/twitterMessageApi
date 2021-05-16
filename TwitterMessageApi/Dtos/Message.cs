using System;
using System.ComponentModel.DataAnnotations;

namespace TwitterMessageApi.Dtos
{
    public class Message
    {
        public string Name { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string MessageText { get; set; }
        public DateTime MessagePostedDate { get; set; }
    }
}
