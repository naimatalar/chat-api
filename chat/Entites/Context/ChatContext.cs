using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace chat.Entites.Context
{
    public class ChatContext:DbContext
    {
        public ChatContext(DbContextOptions<ChatContext> options) : base(options)
        {

        }
        public DbSet<User> Users { get; set; }
        public DbSet<WebSites> WebSites { get; set; }
        public DbSet<MessageTopic> MessageTopics { get; set; }
        public DbSet<MessageContent> MessageContents { get; set; }



    }
}
