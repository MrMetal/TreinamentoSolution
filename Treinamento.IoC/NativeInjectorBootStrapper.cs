using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Treinamento.Application.CQRS.Auth;
using Treinamento.Application.CQRS.Empresas;
using Treinamento.Application.Interfaces;
using Treinamento.Data.Context;
using Treinamento.Data.Identity;
using Treinamento.Data.Identity.Jwt;
using Treinamento.Data.Interfaces;
using Treinamento.Data.Repositories;
using Treinamento.Data.Services;
using Treinamento.Domain;
using Treinamento.Domain.Interfaces;

namespace Treinamento.IoC;

public class NativeInjectorBootStrapper
{
    public static void RegisterServices(IServiceCollection services)
    {
        #region Configs

        services.AddScoped<ApplicationDbContext>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<INotificador, Notificador>();
        services.AddScoped<IUser, AspNetUser>();
        services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

        services.AddScoped<TokenSettings>();
        services.AddScoped<AppSettings>();

        services.AddScoped<IMediator, Mediator>();
        services.AddScoped<IDataBaseService, DataBaseService>();
        services.AddScoped<UserManager<ApplicationUser>>();

        #endregion
        
        #region Repositories

        services.AddScoped<IEmpresaRepository, EmpresaRepository>();
        services.AddScoped<ISetorRepository, SetorRepository>();
        services.AddScoped<IEnderecoRepository, EnderecoRepository>();

        #endregion

        #region Services

        services.AddScoped<IEnderecoService, EnderecoService>();

        #endregion

        #region CQRS

        #region Auth

        services.AddScoped<IRequestHandler<RegisterCommand, ResultData>, AuthCommandHandler>();
        services.AddScoped<IRequestHandler<LoginCommand, ResultData>, AuthCommandHandler>();

        #endregion

        #region Empresa

        services.AddScoped<IRequestHandler<CreateEmpresaCommand, ResultData>, EmpresaCommandHandler>();
        services.AddScoped<IRequestHandler<UpdateEmpresaCommand, ResultData>, EmpresaCommandHandler>();
        services.AddScoped<IRequestHandler<EnderecoCommand, ResultData>, EmpresaCommandHandler>();
        services.AddScoped<IRequestHandler<GetAllEmpresasQuery, ResultData>, EmpresaQueryHandler>();
        services.AddScoped<IRequestHandler<GetEmpresaByIdQuery, ResultData>, EmpresaQueryHandler>();
        

        #endregion

        #endregion

    }
}