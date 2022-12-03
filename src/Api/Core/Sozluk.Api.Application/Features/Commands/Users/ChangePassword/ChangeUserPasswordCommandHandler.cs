using MediatR;
using Sozluk.Api.Application.Interfaces;
using Sozluk.Common.Events.User;
using Sozluk.Common.Infrastructure;
using Sozluk.Common.Infrastructure.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sozluk.Api.Application.Features.Commands.Users.ChangePassword
{
    public class ChangeUserPasswordCommandHandler : IRequestHandler<ChangeUserPasswordCommand, bool>
    {
        private readonly IUserRepository _userRepository;

        public ChangeUserPasswordCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<bool> Handle(ChangeUserPasswordCommand request, CancellationToken cancellationToken)
        {
            var dbUser = await _userRepository.GetByIdAsync(request.UserId.Value);

            if (dbUser is null)
                throw new DatabaseValidationException("User not found");
            var encPass = PasswordEnc.Encrpt(request.OldPassword);

            if(dbUser.Password != encPass)
            {
                throw new DatabaseValidationException("Old Password Wrong");
            }

            dbUser.Password = PasswordEnc.Encrpt(request.NewPassword);
            await _userRepository.UpdateAsync(dbUser);
            return true;
            
        }
    }
}
