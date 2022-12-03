using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sozluk.Common.Events.EntryComment
{
    public class DeleteEntryCommentVoteEvent : IRequest<bool>
    {
        public Guid EntryCommendId { get; set; }

        public Guid CreatedBy { get; set; }
    }
}
