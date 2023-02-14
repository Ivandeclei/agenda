using System;
using System.Data;
using System.Threading.Tasks;
using Agenda.Domain.Adapters;
using Agenda.Domain.Models;
using Dapper;

namespace Agenda.DbAdapter
{
    public class LogAgendaWriteAdapter : ILogAgendaWriteAdapter
    {
        private readonly IDbConnection dbConnection;
        static LogAgendaWriteAdapter() => SqlMapper.AddTypeMap(typeof(string), DbType.AnsiString);

        public LogAgendaWriteAdapter(IDbConnection dbConnection)
        {
            this.dbConnection = dbConnection ??
               throw new ArgumentNullException(nameof(dbConnection));
        }

        public async Task InserirLogAgendaAsync(AgendaLog agendaLog)
        {
            await dbConnection.ExecuteAsync(@"INSERT INTO AgendaLog(
                                                    Metodo, 
                                                    Excecao, 
                                                    Erros, 
                                                    DataHorario)
                                             VALUES(
                                                    @Metodo, 
                                                    @Excecao, 
                                                    @Erros, 
                                                    @DataHorario)", param: new
            {
                Metodo = agendaLog.Metodo,
                Excecao = agendaLog.Excecao,
                Erros = agendaLog.Erros,
                DataHorario = agendaLog.DataHorario
            });
        }
    }
}
