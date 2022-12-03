using AutoMapper;
using Sozluk.Api.Domain.Models;
using Sozluk.Common.Models.Queries;
using Sozluk.Common.Models.RequestModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sozluk.Api.Application.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, LoginUserViewModel>().ReverseMap();

            CreateMap<CreateUserCommand, User>();

            CreateMap<UpdateUserCommand, User>();

            CreateMap<CreateEntryCommand, Entry>().ReverseMap();

            CreateMap<Entry, GetEntriesViewModel>().ForMember(x => x.CommentCount, y => y.MapFrom(z => z.EntryComments.Count));

            CreateMap<CreateEntryCommentCommand, EntryComment>().ReverseMap();

            CreateMap<UserDetailViewModel, User>().ReverseMap();
        }
    }
}
