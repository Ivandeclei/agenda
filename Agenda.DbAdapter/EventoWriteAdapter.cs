using System;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Agenda.Domain.Adapters;
using Agenda.Domain.Models;
using AutoMapper;
using Dapper;

namespace Agenda.DbAdapter
{
    public class EventoWriteAdapter : IEventoWriteAdapter
    {
        private readonly IDbConnection dbConnection;
        private readonly IMapper mapper;

        static EventoWriteAdapter() => SqlMapper.AddTypeMap(typeof(string), DbType.AnsiString);

        public EventoWriteAdapter(IDbConnection dbConnection, IMapper mapper)
        {
            this.dbConnection = dbConnection ??
                throw new ArgumentNullException(nameof(dbConnection));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<Evento> AtualizarEventoAsync(Evento evento, Guid identificadorUsuario)
        {
            var retorno = await dbConnection.QueryAsync<Evento>(
               @"UPDATE [dbo].[Evento]
                       SET 
                         [Nome] = @Nome
                        ,[Descricao] = @Descricao
                        ,[DataHorario] = @Data
                        ,[Local] = @Local
                        ,[FK_IdentificadorUsuario] = @IdentificadorUsuario
                                 
                     WHERE identificadorEvento = @IdentificadorEvento

                    Select 
                            IdentificadorEvento,
                            Nome,
                            Descricao,
                            DataHorario,
                            Local,
                            FK_IdentificadorTipoEvento,
                            FK_IdentificadorUsuario
                    FROM Evento
                    WHERE IdentificadorEvento = @IdentificadorEvento and FK_IdentificadorUsuario = @IdentificadorUsuario",
                    new[]
                    {
                        typeof(Evento)

                    },
                    objeto =>
                    {
                        var evento = objeto[0] as Evento;

                        return evento;
                    },
                    param: new
                    {
                        Nome = evento.Nome,
                        Descricao = evento.Descricao,
                        Data = evento.Data,
                        Local = evento.Local,
                        IdentificadorUsuario = identificadorUsuario,
                        IdentificadorEvento = evento.IdentificadorEvento
                    });

            return retorno.FirstOrDefault<Evento>();
        }

        public Task DeletarEventoAsync(Guid identificadorEvento)
        {
            throw new NotImplementedException();
        }

        public async Task SalvarEventoAsync(Evento evento, Guid identificadorUsuario)
        {
            await dbConnection.QueryAsync(
              @"INSERT INTO [dbo].[Evento](
                            Nome,
                            Descricao,
                            DataHorario,
                            Local,
                            FK_IdentificadorTipoEvento,
                            FK_IdentificadorUsuario
                         )
                         VALUES
                         (
                                @Nome,
                                @Descricao,
                                @Data,
                                @Local,
                                (select Identificador from TipoEvento where Valor = @Valor),
                                @IdentificadorUsuario
                        )",
              param: new
              {
                  Nome = evento.Nome,
                  Descricao = evento.Descricao,
                  Data = evento.Data,
                  Local = evento.Local,
                  IdentificadorUsuario = identificadorUsuario,
                  IdentificadorTipoEvento = evento.TipoEvento,
                  Valor = evento.TipoEvento

              });
        }

        public async Task SalvarParticipanteEvento(ParticipanteEvento participanteEvento)
        {
            await dbConnection.QueryAsync<Usuario>(
              @"INSERT INTO [dbo].[ParticipanteEvento](
                            FK_IdentificadorEvento,
                            FK_IdentificadorUsuario
                         )
                         VALUES
                         (
                                @IdentificadorEvento,
                                @IdentificadorUsuario
                        )",
              param: new
              {
                  IdentificadorEvento = participanteEvento.IdentificadorEvento,
                  IdentificadorUsuario = participanteEvento.IdentificadorUsuario,
                  
              });
        }
    }
}
