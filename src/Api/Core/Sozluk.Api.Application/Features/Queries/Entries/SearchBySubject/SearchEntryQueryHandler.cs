using MediatR;
using Microsoft.EntityFrameworkCore;
using Sozluk.Api.Application.Interfaces;
using Sozluk.Common.Models.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sozluk.Api.Application.Features.Queries.Entries.SearchBySubject
{
    public class SearchEntryQueryHandler : IRequestHandler<SearchEntryQuery, List<SearchEntryViewModel>>
    {
        private readonly IEntryRepository _entryRepository;

        public SearchEntryQueryHandler(IEntryRepository entryRepository)
        {
            _entryRepository = entryRepository;
        }

        public async Task<List<SearchEntryViewModel>> Handle(SearchEntryQuery request, CancellationToken cancellationToken)
        {
            //TODO validation, request.SearchText lenght should be checked
            var result = _entryRepository
                 .Get(i => EF.Functions.Like(i.Subject, $"{request.SearchText}%"))
                 .Select(i => new SearchEntryViewModel()
                 {
                     Id = i.Id,
                     Subject = i.Subject
                 });

            return await result.ToListAsync(cancellationToken);
        }
    }
}
