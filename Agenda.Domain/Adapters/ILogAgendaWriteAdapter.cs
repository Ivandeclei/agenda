using System.Threading.Tasks;
using Agenda.Domain.Models;

namespace Agenda.Domain.Adapters
{
    public interface ILogAgendaWriteAdapter
    {
        /// <summary>
        /// Insere log de erros da aplicação
        /// </summary>
        /// <param name="agendaLog">
        /// objeto com dados para gerar os log da aplicação</param>

        Task InserirLogAgendaAsync(AgendaLog agendaLog);
    }
}
