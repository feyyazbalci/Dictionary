using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sozluk.Api.Application.Interfaces;
using Sozluk.Infrastructure.Persistence.Context;
using Sozluk.Infrastructure.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sozluk.Infrastructure.Persistence.Extensions
{
    public static class Registration
    {
        public static IServiceCollection AddInfrastructureRegistration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<SozlukContext>(conf =>
            {
                var conStr = configuration["ConnectionStrings:DefaultConnection"].ToString();
                conf.UseSqlServer(conStr, opt =>
                {
                    opt.EnableRetryOnFailure(); //Veritabanina baglanirken bir hata alirsak diye
                });
            });

            services.AddScoped<IUserRepository, UserRepository>(); //Generic repository'i kullanabilmek icin ilgili repository'ler buraya eklenmelidir.
            services.AddScoped<IEmailConfirmationRepository, EmailConfirmationRepository>();
            services.AddScoped<IEntryCommentRepository, EntryCommentRepository>();
            services.AddScoped<IEntryRepository, EntryRepository>();

            var seedData = new SeedData();
            seedData.SeedAsync(configuration).GetAwaiter().GetResult();
            return services;
        }
    }
}
