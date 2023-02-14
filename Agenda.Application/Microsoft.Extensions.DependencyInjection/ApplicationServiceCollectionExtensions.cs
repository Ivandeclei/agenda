using System;
using Agenda.Domain.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Agenda.Application.Microsoft.Extensions.DependencyInjection
{
    public static class ApplicationServiceCollectionExtensions
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            services.AddScoped<IEventoService, EventoService>();
            services.AddScoped<IUsuarioService, UsuarioService>();
            services.AddScoped<IValidacaoBaseService, ValidacaoBaseService>();
            services.AddScoped<IValidaEventoService, ValidacaoEventoService>();
            services.AddScoped<IValidaUsuarioService, ValidacaoUsuarioService>();
            services.AddScoped<ILogAgendaService, LogAgendaService>();

            return services;

        }
    }
}
