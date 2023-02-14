using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Agenda.Domain.Models;

namespace Agenda.Domain.Services
{
    public interface IEventoService
    {
        /// <summary>
        /// Busca registro de Evento com usuarios participantes do evento
        /// </summary>
        /// <param name="identificadorEvento">Identificador do evento</param>
        /// <returns>
        /// Objeto de evento e usuarios vinculados
        /// </returns>
        Task<ParticipanteEventoRetorno> BuscarEventoParticipanteAsync(Guid identificadorEvento);

        /// <summary>
        /// Busca registro de Evento com usuarios participantes do evento
        /// </summary>
        /// <returns>
        /// Objeto de evento e usuarios vinculados
        /// </returns>
        Task<IEnumerable<ParticipanteEventoRetorno>> BuscarEventosAsync();

        /// <summary>
        /// Salva registro de Evento
        /// </summary>
        /// <param name="evento">
        /// Objeto de evento
        /// </param>
        Task SalvarEventoAsync(Evento evento, Guid identificadorUsuario);

        /// <summary>
        /// Salva Novo usuário participante no evento
        /// </summary>
        /// <param name="participanteEvento">
        /// Objeto de participante do evento
        /// </param>
        Task SalvarParticipanteEventoAsync(ParticipanteEvento participanteEvento);

        /// <summary>
        /// Atualiza registro de evento
        /// </summary>
        /// <param name="evento">
        /// Objeto de evento
        /// </param>
        /// <returns>
        /// Objeto de evento atualizado
        /// </returns>
        Task<Evento> AtualizarEventoAsync(Evento evento, Guid identificadorUsuario);

        /// <summary>
        /// Deleta registro de evento
        /// </summary>
        /// <param name="IdentificadorEvento">
        /// Identificador do evento
        /// </param>
        Task DeletarEventoAsync(Guid identificadorEvento);

    }
}
