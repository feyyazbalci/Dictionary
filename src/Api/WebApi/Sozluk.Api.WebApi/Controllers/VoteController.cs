﻿using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sozluk.Api.Application.Features.Commands.Entries.DeleteVote;
using Sozluk.Api.Application.Features.Commands.EntryComments.DeleteVote;
using Sozluk.Common.Models;
using Sozluk.Common.Models.RequestModels;

namespace Sozluk.Api.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class VoteController : BaseController
    {
        private readonly IMediator _mediator;

        public VoteController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Route("Entry/{entryId}")]
        //Default'u query string
        public async Task<IActionResult> CreateEntryVote(Guid entryId, VoteType voteType = VoteType.UpVote)
        {
            var result = await _mediator.Send(new CreateEntryVoteCommand(entryId, voteType, UserId.Value));
            return Ok(result);
        }

        [HttpPost]
        [Route("EntryComment/{entryCommentId}")]
        public async Task<IActionResult> CreateEntryCommentVote(Guid entryCommentId, VoteType voteType = VoteType.UpVote)
        {
            var result = await _mediator.Send(new CreateEntryCommentVoteCommand(entryCommentId, voteType, UserId.Value));

            return Ok(result);
        }


        [HttpPost]
        [Route("DeleteEntryVote/{entryId}")]
        public async Task<IActionResult> DeleteEntryVoteCommand(Guid entryId)
        {
            await _mediator.Send(new DeleteEntryVoteCommand(entryId, UserId.Value));

            return Ok();
        }

        [HttpPost]
        [Route("DeleteEntryCommentVote/{entryId}")]
        public async Task<IActionResult> DeleteEntryCommentVoteCommand(Guid entryCommentId)
        {
            await _mediator.Send(new DeleteEntryCommentVoteCommand(entryCommentId, UserId.Value));

            return Ok();
        }

    }
}
