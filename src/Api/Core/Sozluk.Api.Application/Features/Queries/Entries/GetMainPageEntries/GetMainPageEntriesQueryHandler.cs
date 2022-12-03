using MediatR;
using Microsoft.EntityFrameworkCore;
using Sozluk.Api.Application.Interfaces;
using Sozluk.Common.Infrastructure.Extensions;
using Sozluk.Common.Models;
using Sozluk.Common.Models.Page;
using Sozluk.Common.Models.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sozluk.Api.Application.Features.Queries.Entries.GetMainPageEntries
{
    public class GetMainPageEntriesHandler : IRequestHandler<GetMainPageEntriesQuery, PagedViewModel<GetEntryDetailViewModel>>
    {

        private readonly IEntryRepository _entryRepository;

        public GetMainPageEntriesHandler(IEntryRepository entryRepository)
        {

            _entryRepository = entryRepository;
        }

        public async Task<PagedViewModel<GetEntryDetailViewModel>> Handle(GetMainPageEntriesQuery request, CancellationToken cancellationToken)
        {
            var query = _entryRepository.AsQueryable();

            query = query
                .Include(i => i.EntryFavorites)
                .Include(i => i.CreatedBy)
                .Include(i => i.EntryVotes);

            var list = query.Select(i => new GetEntryDetailViewModel()
            {
                Id = i.Id,
                Subject = i.Subject,
                Content = i.Content,
                CreatedDate = i.CreateDate,
                IsFavorited = request.UserId.HasValue && i.EntryFavorites.Any(j => j.CreatedById == request.UserId),
                CreatedByUserName = i.CreatedBy.UserName,
                FavoritedCount = i.EntryFavorites.Count,
                VoteType = request.UserId.HasValue && i.EntryVotes.Any(j => j.CreatedById == request.UserId)
                ? i.EntryVotes.FirstOrDefault(j => j.CreatedById == request.UserId).VoteType : VoteType.None


            });

            var entries = await list.GetPaged(request.Page, request.PageSize);

            return entries;

        }
    }
}
