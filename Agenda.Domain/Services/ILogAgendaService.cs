using System;
using System.Threading.Tasks;

namespace Agenda.Domain.Services
{
    public interface ILogAgendaService
    {
        /// <summary>
        /// Gera logo de erros para toda API 
        /// </summary>
        /// <param name="exception"></param>
        /// <param name="nomeMetodo"></param>
        /// <exception cref="ArgumentNullException"></exception>
        Task GerarLogPorMetodoAsync(Exception exception, string nomeMetodo);
    }
}
