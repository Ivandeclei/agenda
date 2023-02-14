using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Agenda.Domain.Adapters;
using Agenda.Domain.Models;
using Agenda.Domain.Services;
using Microsoft.Extensions.Logging;

namespace Agenda.Application
{
    public class EventoService : IEventoService
    {
        private readonly IEventoReadAdapter dbEventoReadAdapter;
        private readonly IEventoWriteAdapter dbEventoWriteAdapter;
        private readonly ILogger logger;
        private readonly IValidaEventoService validaEventoService;
        private readonly ILogAgendaService logAgendaService;

        public EventoService(IEventoReadAdapter dbEventoReadAdapter, IEventoWriteAdapter dbEventoWriteAdapter,
            ILoggerFactory loggerFactory, IValidaEventoService validaEventoService, ILogAgendaService logAgendaService)
        {
            this.dbEventoReadAdapter = dbEventoReadAdapter ?? throw new ArgumentNullException(nameof(dbEventoReadAdapter));
            this.dbEventoWriteAdapter = dbEventoWriteAdapter ?? throw new ArgumentNullException(nameof(dbEventoWriteAdapter));
            this.logger = loggerFactory.CreateLogger<EventoService>() ?? throw new ArgumentNullException(nameof(logger));
            this.validaEventoService = validaEventoService ?? throw new ArgumentNullException(nameof(validaEventoService));
            this.logAgendaService = logAgendaService ?? throw new ArgumentNullException(nameof(logAgendaService));
        }

        public async Task<Evento> AtualizarEventoAsync(Evento evento, Guid identificadorUsuario)
        {
            logger.LogInformation("Realizando chamada ao metodo" +
                nameof(AtualizarEventoAsync));

            if (evento is null)
            {
                throw new ArgumentNullException(nameof(evento));
            }

            if (!Guid.TryParse(identificadorUsuario.ToString(), out _))
            {
                throw new ArgumentNullException(nameof(identificadorUsuario));
            }

            validaEventoService.ValidaEvento(evento);

            try
            {
                return  await dbEventoWriteAdapter.AtualizarEventoAsync(evento, identificadorUsuario);
            }
            catch (Exception e)
            {

                await logAgendaService.GerarLogPorMetodoAsync(e,
                    nameof(IEventoWriteAdapter.AtualizarEventoAsync));

                logger.LogInformation("Falha na chamada do metodo" + nameof(
                    AtualizarEventoAsync));

                throw;
            }
        }

        public async  Task<ParticipanteEventoRetorno> BuscarEventoParticipanteAsync( Guid identificadorEvento)
        {
            logger.LogInformation("Realizando chamada ao metodo" +
                nameof(BuscarEventoParticipanteAsync));

            if (!Guid.TryParse(identificadorEvento.ToString(), out _))
            {
                throw new ArgumentNullException(nameof(identificadorEvento));
            }

            try
            {
                var eventos = await dbEventoReadAdapter.BuscarEventoParticipanteAsync(identificadorEvento);
                return eventos;

            }
            catch (Exception e)
            {
                await logAgendaService.GerarLogPorMetodoAsync(e,
                    nameof(IEventoReadAdapter.BuscarEventoParticipanteAsync));

                logger.LogInformation("Falha na chamada do metodo" + nameof(
                    BuscarEventoParticipanteAsync));

                throw;
            }
        }

        public async Task<IEnumerable<ParticipanteEventoRetorno>> BuscarEventosAsync()
        {
            logger.LogInformation("Realizando chamada ao metodo" +
                nameof(BuscarEventosAsync));

            try
            {
                var eventos = await dbEventoReadAdapter.BuscarEventoAsync(TipoEvento.Compartilhado);
                return eventos;

            }
            catch (Exception e)
            {
                await logAgendaService.GerarLogPorMetodoAsync(e,
                    nameof(IEventoReadAdapter.BuscarEventoAsync));

                logger.LogInformation("Falha na chamada do metodo" + nameof(
                    BuscarEventosAsync));

                throw;
            }
        }

        public async Task DeletarEventoAsync(Guid IdentificadorEvento)
        {
            logger.LogInformation("Realizando chamada ao metodo" +
                nameof(DeletarEventoAsync));

            if (!Guid.TryParse(IdentificadorEvento.ToString(), out _))
            {
                throw new ArgumentNullException(nameof(IdentificadorEvento));
            }

            try
            {
                 await dbEventoWriteAdapter.DeletarEventoAsync(IdentificadorEvento);

            }
            catch (Exception e)
            {
                await logAgendaService.GerarLogPorMetodoAsync(e,
                    nameof(IEventoWriteAdapter.DeletarEventoAsync));

                logger.LogInformation("Falha na chamada do metodo" + nameof(
                    DeletarEventoAsync));

                throw;
            }
        }

        public async Task SalvarEventoAsync(Evento evento, Guid identificadorUsuario)
        {
            logger.LogInformation("Realizando chamada ao metodo" +
                nameof(SalvarEventoAsync));

            if (evento is null)
            {
                throw new ArgumentNullException(nameof(evento));
            }
            if (!Guid.TryParse(identificadorUsuario.ToString(), out _))
            {
                throw new ArgumentNullException(nameof(identificadorUsuario));
            }

            validaEventoService.ValidaEvento(evento);

            try
            {
                await dbEventoWriteAdapter.SalvarEventoAsync(evento, identificadorUsuario);
            }
            catch (Exception e)
            {

                await logAgendaService.GerarLogPorMetodoAsync(e,
                    nameof(IEventoWriteAdapter.SalvarEventoAsync));

                logger.LogInformation("Falha na chamada do metodo" + nameof(
                    SalvarEventoAsync));

                throw;
            }

        }

        public async Task SalvarParticipanteEventoAsync(ParticipanteEvento participanteEvento)
        {
            logger.LogInformation("Realizando chamada ao metodo" +
                nameof(SalvarParticipanteEventoAsync));

            if (participanteEvento is null)
            {
                throw new ArgumentNullException(nameof(participanteEvento));
            }

            validaEventoService.ValidaParticipanteEvento(participanteEvento);

            try
            {
                await dbEventoWriteAdapter.SalvarParticipanteEvento(participanteEvento);
            }
            catch (Exception e)
            {

                await logAgendaService.GerarLogPorMetodoAsync(e,
                    nameof(IEventoWriteAdapter.SalvarParticipanteEvento));

                logger.LogInformation("Falha na chamada do metodo" + nameof(
                    SalvarParticipanteEventoAsync));

                throw;
            }
        }

    }
}
