using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Agenda.Domain.Adapters;
using Agenda.Domain.Models;
using AutoMapper;
using Dapper;

namespace Agenda.DbAdapter
{
    public class EventoReadAdapter : IEventoReadAdapter
    {
        private readonly IDbConnection dbConnection;
        private readonly IMapper mapper;

        static EventoReadAdapter() => SqlMapper.AddTypeMap(typeof(string), DbType.AnsiString);

        public EventoReadAdapter(IDbConnection dbConnection, IMapper mapper)
        {
            this.dbConnection = dbConnection ?? throw new ArgumentNullException(nameof(dbConnection));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<IEnumerable<ParticipanteEventoRetorno>> BuscarEventoAsync(TipoEvento tipoEvento)
        {
            var retorno = await dbConnection.QueryAsync<ParticipanteEventoRetorno>(@"Select 
                                            e.IdentificadorEvento,
                                            e.Nome,
                                            e.Descricao,
                                            e.DataHorario,
                                            e.Local,
                                            u.IdentificadorUsuario,
                                            u.Nome,
                                            U.Login
                                    FROM Evento as e
                                    INNER JOIN ParticipanteEvento as pe 
                                    ON e.IdentificadorEvento = pe.FK_IdentificadorEvento
                                    INNER JOIN Usuario as u
                                    ON pe.FK_IdentificadorUsuario = u.IdentificadorUsuario
                                    WHERE e.FK_IdentificadorTipoEvento = (select identificador FROM tipoEvento WHERE valor = @tipoEvento)",
                                    new[]
                                    {
                                        typeof(ParticipanteEventoRetorno),
                                        typeof(Usuario),

                                    },
                                    objeto =>
                                    {
                                        var ParticipanteEvento = objeto[0] as ParticipanteEventoRetorno;
                                        var usuario = objeto[1] as Usuario;
                                        ParticipanteEvento.Usuario = new List<Usuario>();
                                        ((List<Usuario>)ParticipanteEvento.Usuario).Add(usuario);
                                        return ParticipanteEvento;
                                    },
                                    param: new
                                    {
                                        tipoEvento = tipoEvento
                                    },
                                    splitOn: "IdentificadorUsuario");

            return retorno;
        }

        public async Task<ParticipanteEventoRetorno> BuscarEventoParticipanteAsync(Guid IdentificadorEvento)
        {
            var retorno = await dbConnection.QueryAsync<ParticipanteEventoRetorno>(@"Select 
                                            e.IdentificadorEvento,
                                            e.Nome,
                                            e.Descricao,
                                            e.DataHorario,
                                            e.Local,
                                            u.IdentificadorUsuario,
                                            u.Nome,
                                            U.Login
                                    FROM Evento as e
                                    INNER JOIN ParticipanteEvento as pe 
                                    ON e.IdentificadorEvento = pe.FK_IdentificadorEvento
                                    INNER JOIN Usuario as u
                                    ON pe.FK_IdentificadorUsuario = u.IdentificadorUsuario
                                    WHERE IdentificadorEvento = @IdentificadorEvento",
                                    new[]
                                    {
                                        typeof(ParticipanteEventoRetorno),
                                        typeof(Usuario),
                                        
                                    },
                                    objeto =>
                                    {
                                        var ParticipanteEvento = objeto[0] as ParticipanteEventoRetorno;
                                        var usuario = objeto[1] as Usuario;
                                        ParticipanteEvento.Usuario = new List<Usuario>();
                                        ((List<Usuario>)ParticipanteEvento.Usuario).Add(usuario);
                                        return ParticipanteEvento;
                                    },
                                    param: new
                                    {
                                        IdentificadorEvento = IdentificadorEvento
                                    },
                                    splitOn: "IdentificadorUsuario");

            return retorno.FirstOrDefault();
        }
    }
}
