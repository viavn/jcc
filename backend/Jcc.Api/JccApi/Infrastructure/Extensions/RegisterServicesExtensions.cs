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
            services.AddScoped<IFamilyRepository, FamilyRepository>();
            services.AddScoped<IGenreRepository, GenreRepository>();
            services.AddScoped<IGiftRepository, GiftRepository>();
            services.AddScoped<IGiftTypeRepository, GiftTypeRepository>();
            services.AddScoped<IGodParentRepository, GodParentRepository>();
            services.AddScoped<ILegalPersonTypeRepository, LegalPersonTypeRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserTypeRepository, UserTypeRepository>();

            return services;
        }
    }
}