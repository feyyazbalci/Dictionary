using Sozluk.Api.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sozluk.Api.Application.Interfaces
{
    public interface IEmailConfirmationRepository : IGenericRepository<EmailConfirmation>
    {
    }
}
