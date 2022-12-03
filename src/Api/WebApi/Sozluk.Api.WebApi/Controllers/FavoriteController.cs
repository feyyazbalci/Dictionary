using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sozluk.Api.Application.Features.Commands.Entries.CreateFav;
using Sozluk.Api.Application.Features.Commands.Entries.DeleteFav;
using Sozluk.Api.Application.Features.Commands.EntryComments.CreateFav;
using Sozluk.Api.Application.Features.Commands.EntryComments.DeleteFav;
using Sozluk.Api.Domain.Models;

namespace Sozluk.Api.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FavoriteController : BaseController
    {
        private readonly IMediator mediator;

        public FavoriteController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost]
        [Route("entry/{entryId}")]
        public async Task<IActionResult> CreateEntryFav(Guid entryId)
        {
            var result = await mediator.Send(new CreateEntryFavCommand(entryId, UserId));

            return Ok(result);
        }

        [HttpPost]
        [Route("entrycomment/{entrycommentId}")]
        public async Task<IActionResult> CreateEntryCommentFav(Guid entrycommentId)
        {
            var result = await mediator.Send(new CreateEntryCommentFavCommand(entrycommentId, UserId.Value));

            return Ok(result);
        }

        [HttpPost]
        [Route("deleteentryfav/{entryId}")]
        public async Task<IActionResult> DeleteEntryFav(Guid entryId)
        {
            var result = await mediator.Send(new DeleteEntryFavCommand(entryId, UserId.Value));

            return Ok(result);
        }

        [HttpPost]
        [Route("deleteentrycommentfav/{entrycommentId}")]
        public async Task<IActionResult> DeleteEntryCommentFav(Guid entrycommentId)
        {
            var result = await mediator.Send(new DeleteEntryCommentFavCommand(entrycommentId, UserId.Value));

            return Ok(result);
        }
    }
}
