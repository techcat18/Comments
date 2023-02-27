﻿using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Comments.Domain.Entities;

namespace Comments.Application.Common.Interfaces
{
    public interface ICommentRepository
    {
        Task<IEnumerable<Comment>> GetAllAsync(CancellationToken cancellationToken);
        Task CreateAsync(Comment comment, CancellationToken cancellationToken);
        void Update(Comment comment);
        void Delete(Comment comment);
    }
}