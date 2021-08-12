using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace chat.Entites
{
    public class BaseEntity
    {
        public Guid Id { get; set; }
        public DateTime CreateDate { get; set; } = DateTime.Now;
    }
}
