using System;
using System.Threading.Tasks;
using Agenda.Domain.Models;

namespace Agenda.Domain.Adapters
{
    public interface IEventoWriteAdapter
    {
        /// <summary>
        /// Salva na base de dados o registro de evento
        /// </summary>
        /// <param name="evento">
        /// Objeto de evento
        /// </param>
        Task SalvarEventoAsync(Evento evento, Guid identificadorUsuario);

        /// <summary>
        /// Atualiza registro de evento na base de dados 
        /// </summary>
        /// <param name="evento">
        /// Objeto de evento
        /// </param>
        /// <returns>
        /// Retorna Registro de evento atualizado
        /// </returns>
        Task<Evento> AtualizarEventoAsync(Evento evento, Guid identificadorUsuario);

        /// <summary>
        /// Adiciona usuario a um evento
        /// </summary>
        /// <param name="participanteEvento">
        /// Objeto de participante de evento
        /// </param>
        Task SalvarParticipanteEvento(ParticipanteEvento participanteEvento);

        /// <summary>
        /// Deletar registro do banco de dados 
        /// </summary>
        /// <param name="identificadorEvento">
        /// Identificador do registro a ser apagado
        /// </param>
        Task DeletarEventoAsync(Guid identificadorEvento);
    }
}
