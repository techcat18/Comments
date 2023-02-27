﻿using System;
using System.Threading;
using System.Threading.Tasks;
using Comments.Application.Comments.Commands.CreateComment;
using Comments.Application.Comments.Queries.GetAllComments;
using Comments.Application.Comments.Queries.GetCommentById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Comments.API.Controllers
{
    [ApiController]
    [Route("api/comments")]
    public class CommentsController: ControllerBase
    {
        private readonly IMediator _mediator;

        public CommentsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            return Ok(await _mediator.Send(new GetAllCommentsQuery(), cancellationToken));
        }

        [HttpGet("{id:guid}", Name = nameof(GetById))]
        public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
        {
            return Ok(await _mediator.Send(new GetCommentByIdQuery { Id = id }, cancellationToken));
        }
        
        [HttpPost]
        public async Task<IActionResult> Create(
            CreateCommentCommand command, 
            CancellationToken cancellationToken)
        {
            var comment = await _mediator.Send(command, cancellationToken);
            return CreatedAtAction(nameof(GetById), new { id = comment.Id }, comment);
        }
    }
}