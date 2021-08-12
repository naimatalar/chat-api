using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace chat.Entites
{
    public class WebSites:BaseEntity
    {
        public string WebSiteName { get; set; }
        public string Key { get; set; }
        public IList<MessageTopic> MessageTopics { get; set; }
    }
}
