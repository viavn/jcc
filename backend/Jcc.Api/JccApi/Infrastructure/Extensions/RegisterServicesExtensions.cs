using FluentValidation;
using JccApi.Application;
using JccApi.Application.Abstractions.UseCases;
using JccApi.Application.Validators;
using JccApi.Infrastructure.Repository;
using JccApi.Infrastructure.Repository.Abstractions;
using JccApi.Models;
using Microsoft.Extensions.DependencyInjection;

namespace JccApi.Infrastructure.Extensions
{
    public static class RegisterExtensions
    {
        public static IServiceCollection AddRepositoryDependencyGroup(this IServiceCollection services)
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

        public static IServiceCollection AddUseCaseDependencyGroup(this IServiceCollection services)
        {
            services.AddScoped<ICreateFamilyUseCaseAsync, CreateFamilyUseCaseAsync>();

            return services;
        }

        public static IServiceCollection AddValidatorDependecyGroup(this IServiceCollection services)
        {
            services.AddScoped<IValidator<CreateFamilyRequest>, CreateFamilyValidator>();

            return services;
        }
    }
}