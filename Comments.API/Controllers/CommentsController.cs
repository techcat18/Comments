﻿using System;
using System.Threading;
using System.Threading.Tasks;
using Comments.Application.Comments.Commands.CreateComment;
using Comments.Application.Comments.Commands.DeleteComment;
using Comments.Application.Comments.Commands.UpdateComment;
using Comments.Application.Comments.Commands.UploadAttachment;
using Comments.Application.Comments.Queries.GetAllComments;
using Comments.Application.Comments.Queries.GetAllReplies;
using Comments.Application.Comments.Queries.GetCommentById;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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
        public async Task<IActionResult> GetAll(
            [FromQuery]GetAllCommentsQuery query,
            CancellationToken cancellationToken)
        {
            return Ok(await _mediator.Send(query, cancellationToken));
        }

        [HttpGet("{id:guid}/replies")]
        public async Task<IActionResult> GetAllReplies(Guid id, CancellationToken cancellationToken)
        {
            return Ok(await _mediator.Send(new GetAllRepliesQuery { ParentCommentId = id }, cancellationToken));
        }

        [HttpGet("{id:guid}", Name = nameof(GetById))]
        public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
        {
            return Ok(await _mediator.Send(new GetCommentByIdQuery { Id = id }, cancellationToken));
        }
        
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost]
        public async Task<IActionResult> Create(
            CreateCommentCommand command, 
            CancellationToken cancellationToken)
        {
            var comment = await _mediator.Send(command, cancellationToken);
            return CreatedAtAction(nameof(GetById), new { id = comment.Id }, comment);
        }
        
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost("{id}/attachment")]
        public async Task<IActionResult> UploadAttachment(
            Guid id,
            [FromForm(Name = "file")] IFormFile file,
            CancellationToken cancellationToken)
        {
            await _mediator.Send(new UploadAttachmentCommand { Id = id, File = file }, cancellationToken);
            return NoContent();
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPut]
        public async Task<IActionResult> Put(
            UpdateCommentCommand command,
            CancellationToken cancellationToken)
        {
            await _mediator.Send(command, cancellationToken);
            return NoContent();
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(
            Guid id,
            CancellationToken cancellationToken)
        {
            await _mediator.Send(new DeleteCommentCommand { Id = id }, cancellationToken);
            return NoContent();
        }
    }
}