﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace chat.Entites
{
    public class User:BaseEntity
    {
        public string Name { get; set; }
        public string Mail { get; set; }
        public string Password { get; set; }
        public IList<MessageContent> MessageContents { get; set; }
    }
}
