using JccApi.Infrastructure.Repository;
using JccApi.Infrastructure.Repository.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace JccApi.Infrastructure.Extensions
{
    public static class RegisterExtensions
    {
        public static IServiceCollection AddDependencyGroup(this IServiceCollection services)
        {
            services.AddScoped<IChildRepository, ChildRepository>();
            services.AddScoped<IFamilyMemberRepository, FamilyMemberRepository>();
            services.AddScoped<IFamilyRepository, IFamilyRepository>();
            services.AddScoped<IGenreRepository, GenreRepository>();
            services.AddScoped<IGiftRepository, IGiftRepository>();
            services.AddScoped<IGiftTypeRepository, IGiftTypeRepository>();
            services.AddScoped<ILegalPersonTypeRepository, LegalPersonTypeRepository>();
            services.AddScoped<IUserRepository, IUserRepository>();
            services.AddScoped<IUserTypeRepository, IUserTypeRepository>();

            return services;
        }
    }
}