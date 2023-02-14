using System;
using System.Data;
using System.Data.SqlClient;
using Agenda.DbAdapter.DbAdapterCofiguration;
using Agenda.Domain.Adapters;
using Microsoft.Extensions.DependencyInjection;

namespace Agenda.DbAdapter.Microsoft.Extensions.DependencyInjection
{
    public static class DbRepositoryServiceCollectionExtensions
    {
        public static IServiceCollection AddDbAdapter(this IServiceCollection services,
            DbAdapterConfiguration dbAdapterConfiguration)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            if (dbAdapterConfiguration == null)
            {
                throw new ArgumentNullException(nameof(dbAdapterConfiguration));
            }

            services.AddSingleton(dbAdapterConfiguration);

            services.AddScoped<IDbConnection>(d => {
                return new SqlConnection(dbAdapterConfiguration.ConnectionString);
            });

            services.AddScoped<IEventoReadAdapter, EventoReadAdapter>();
            services.AddScoped<IEventoWriteAdapter, EventoWriteAdapter>();
            services.AddScoped<IUsuarioReadAdapter, UsuarioReadAdapter>();
            services.AddScoped<IUsuarioWriteAdapter, UsuarioWriteAdapter>();
            services.AddScoped<ILogAgendaWriteAdapter, LogAgendaWriteAdapter>();

            return services;

        }
    }
}
