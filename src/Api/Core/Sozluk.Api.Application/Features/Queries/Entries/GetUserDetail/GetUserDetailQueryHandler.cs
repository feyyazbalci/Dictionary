﻿using AutoMapper;
using MediatR;
using Sozluk.Api.Application.Interfaces;
using Sozluk.Api.Domain.Models;
using Sozluk.Common.Models.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sozluk.Api.Application.Features.Queries.Entries.GetUserDetail
{
    public class GetUserDetailQueryHandler : IRequestHandler<GetUserDetailQuery, UserDetailViewModel>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public GetUserDetailQueryHandler(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<UserDetailViewModel> Handle(GetUserDetailQuery request, CancellationToken cancellationToken)
        {
            User dbUser = null;

            if (request.UserId != Guid.Empty)
                dbUser = await _userRepository.GetByIdAsync(request.UserId);
            else if (!string.IsNullOrEmpty(request.userName))
                dbUser = await _userRepository.GetSingleAsync(i => i.UserName == request.userName);

            //TODO if both are empty, throw new exception  //Hem kullanici adi hem de id eksik ise ona göre bir validasyon olusturulabilir


            return _mapper.Map<UserDetailViewModel>(dbUser);

        }
    }
}
