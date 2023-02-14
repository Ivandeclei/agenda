using System;
using System.Threading.Tasks;
using Agenda.Domain.Adapters;
using Agenda.Domain.Models;
using Agenda.Domain.Services;
using Microsoft.Extensions.Logging;

namespace Agenda.Application
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioReadAdapter dbUsuarioReadAdapter;
        private readonly IUsuarioWriteAdapter dbUsuarioWriteAdapter;
        private readonly ILogger logger;
        private readonly IValidaUsuarioService validaUsuarioService;
        private readonly ILogAgendaService logAgendaService;

        public UsuarioService(IUsuarioReadAdapter dbUsuarioReadAdapter, IUsuarioWriteAdapter dbUsuarioWriteAdapter,
            ILoggerFactory loggerFactory, IValidaUsuarioService validaUsuarioService, ILogAgendaService logAgendaService)
        {
            this.dbUsuarioReadAdapter = dbUsuarioReadAdapter ?? throw new ArgumentNullException(nameof(dbUsuarioReadAdapter));
            this.dbUsuarioWriteAdapter = dbUsuarioWriteAdapter ?? throw new ArgumentNullException(nameof(dbUsuarioWriteAdapter));
            this.logger = loggerFactory.CreateLogger<UsuarioService>() ?? throw new ArgumentNullException(nameof(logger));
            this.validaUsuarioService = validaUsuarioService ?? throw new ArgumentNullException(nameof(validaUsuarioService));
            this.logAgendaService = logAgendaService ?? throw new ArgumentNullException(nameof(logAgendaService));
        }

        public async Task<Usuario> AtualizarEventoAsync(Usuario usuario)
        {
            logger.LogInformation("Realizando chamada ao metodo" +
                nameof(AtualizarEventoAsync));

            if (usuario is null)
            {
                throw new ArgumentNullException(nameof(usuario));
            }

            validaUsuarioService.ValidaUsuario(usuario);

            try
            {
                return await dbUsuarioWriteAdapter.AtualizarUsuarioAsync(usuario);
            }
            catch (Exception e)
            {
                await logAgendaService.GerarLogPorMetodoAsync(e,
                    nameof(IUsuarioWriteAdapter.AtualizarUsuarioAsync));

                logger.LogInformation("Falha na chamada do metodo" + nameof(
                    AtualizarEventoAsync));

                throw;
            }
        }

        public async Task<EventoUsuarioRetorno> BuscarUsuarioAsync(Guid identificadorUsuario)
        {
            logger.LogInformation("Realizando chamada ao metodo" +
                nameof(BuscarUsuarioAsync));

            if (!Guid.TryParse(identificadorUsuario.ToString(), out _))
            {
                throw new ArgumentNullException(nameof(identificadorUsuario));
            }

            try
            {
                return await dbUsuarioReadAdapter.BuscarUsuarioEventoAsync(identificadorUsuario);
            }
            catch (Exception e)
            {
                await logAgendaService.GerarLogPorMetodoAsync(e,
                    nameof(IUsuarioReadAdapter.BuscarUsuarioEventoAsync));

                logger.LogInformation("Falha na chamada do metodo" + nameof(
                    BuscarUsuarioAsync));

                throw;
            }
        }

        public async  Task DeletarUsuarioAsync(Guid identificadorUsuario)
        {
            logger.LogInformation("Realizando chamada ao metodo" +
                nameof(DeletarUsuarioAsync));

            if (!Guid.TryParse(identificadorUsuario.ToString(), out _))
            {
                throw new ArgumentNullException(nameof(identificadorUsuario));
            }

            try
            {
                 await dbUsuarioWriteAdapter.DeletarUsuarioAsync(identificadorUsuario);
            }
            catch (Exception e)
            {
                await logAgendaService.GerarLogPorMetodoAsync(e,
                    nameof(IUsuarioWriteAdapter.DeletarUsuarioAsync));

                logger.LogInformation("Falha na chamada do metodo" + nameof(
                    DeletarUsuarioAsync));

                throw;
            }
        }

        public async Task SalvarUsuarioAsync(Usuario usuario)
        {
            logger.LogInformation("Realizando chamada ao metodo" +
                nameof(SalvarUsuarioAsync));

            if (usuario is null)
            {
                throw new ArgumentNullException(nameof(usuario));
            }

            validaUsuarioService.ValidaUsuario(usuario);

            try
            {
                await dbUsuarioWriteAdapter.SalvarUsuarioAsync(usuario);
            }
            catch (Exception e)
            {
                await logAgendaService.GerarLogPorMetodoAsync(e,
                    nameof(IUsuarioWriteAdapter.SalvarUsuarioAsync));

                logger.LogInformation("Falha na chamada do metodo" + nameof(
                    SalvarUsuarioAsync));

                throw;
            }
        }
    }
}
