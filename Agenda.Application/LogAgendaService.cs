using System;
using System.Threading.Tasks;
using Agenda.Domain.Adapters;
using Agenda.Domain.Models;
using Agenda.Domain.Services;
using Newtonsoft.Json;

namespace Agenda.Application
{
    public class LogAgendaService : ILogAgendaService
    {
        private readonly ILogAgendaWriteAdapter logAgendaAdapter;
        public LogAgendaService(ILogAgendaWriteAdapter logAgendaAdapter)
        {
            this.logAgendaAdapter = logAgendaAdapter ??
                throw new ArgumentNullException(nameof(logAgendaAdapter));
        }

        public async Task GerarLogPorMetodoAsync(Exception exception, string nomeMetodo)
        {
            if (exception is null)
            {
                throw new ArgumentNullException(nameof(exception));
            }

            if (nomeMetodo is null)
            {
                throw new ArgumentNullException(nameof(nomeMetodo));
            }

            var logCondominio = new AgendaLog()
            {
                Erros = JsonConvert.SerializeObject(exception.Message),
                Excecao = JsonConvert.SerializeObject(exception),
                Metodo = nomeMetodo,
                TipoObjeto = exception.GetType().Name,
                DataHorario = DateTimeOffset.Now

            };

            await logAgendaAdapter.InserirLogAgendaAsync(logCondominio);
        }
    }
}
