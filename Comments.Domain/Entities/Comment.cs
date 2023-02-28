﻿using System;
using System.Collections.Generic;

namespace Comments.Domain.Entities
{
    public class Comment: AuditEntity<Guid>
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string HomePage { get; set; }
        public string Text { get; set; }

        public virtual Comment? ParentComment { get; set; }
        public virtual ICollection<Comment> Replies { get; set; }
    }
}