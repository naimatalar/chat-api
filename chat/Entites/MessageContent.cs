using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace chat.Entites
{
    public class MessageContent:BaseEntity
    {
        public string Content { get; set; }
        public  bool IsCustomer { get; set; }
        public User User { get; set; }
        public Guid UserId { get; set; }
        public bool IsRead { get; set; }
        public MessageTopic MessageTopic { get; set; }
        public Guid MessageTopicId { get; set; }
    }
}
