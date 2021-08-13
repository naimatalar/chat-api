using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace chat.Entites
{
    public class MessageTopic:BaseEntity
    {
        public Guid WebSiteId { get; set; }
        public WebSites WebSite { get; set; }
        public string RiderName { get; set; }
        public string RiderMail { get; set; }
        public bool IsRead { get; set; }
        public string ConnectionId { get; set; }
        public IList<MessageContent> messageContents { get; set; }
    }
}
