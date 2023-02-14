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
    public class UsuarioReadAdapter : IUsuarioReadAdapter
    {
        private readonly IDbConnection dbConnection;
        private readonly IMapper mapper;

        static UsuarioReadAdapter() => SqlMapper.AddTypeMap(typeof(string), DbType.AnsiString);

        public UsuarioReadAdapter(IDbConnection dbConnection, IMapper mapper)
        {
            this.dbConnection = dbConnection ?? throw new ArgumentNullException(nameof(dbConnection));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        public async Task<Usuario> BuscarUsuarioAsync(Guid identificador)
        {
            var retorno = await dbConnection.QueryAsync<Usuario>(@"Select 
                                            IdentificadorUsuario,
                                            Nome,
                                            Login
                                    FROM Usuario
                                    WHERE IdentificadorUsuario = @Identificador",
                                    new[]
                                    {
                                        typeof(Usuario),
                                    },
                                    objeto =>
                                    {
                                        var usuario = objeto[0] as Usuario;
                                        return usuario;
                                    },
                                    param: new
                                    {
                                        Identificador = identificador,
                                    });

            return retorno.FirstOrDefault<Usuario>();
        }

        public async Task<EventoUsuarioRetorno> BuscarUsuarioEventoAsync(Guid IdentificadorUsuario)
        {
            var retorno = await dbConnection.QueryAsync<EventoUsuarioRetorno>(@"Select 
                                            u.IdentificadorUsuario,
                                            u.Nome,
                                            U.Login,                                          
                                            e.IdentificadorEvento,
                                            e.Nome,
                                            e.Descricao,
                                            e.DataHorario,
                                            e.Local
                                    FROM Evento as e
                                    INNER JOIN ParticipanteEvento as pe 
                                    ON e.IdentificadorEvento = pe.FK_IdentificadorEvento
                                    RIGHT JOIN Usuario as u
                                    ON e.FK_IdentificadorUsuario = u.IdentificadorUsuario
                                    WHERE IdentificadorUsuario = @IdentificadorUsuario",
                                    new[]
                                    {
                                        typeof(EventoUsuarioRetorno),
                                        typeof(Evento),

                                    },
                                    objeto =>
                                    {
                                        var eventoUsuarioRetorno = objeto[0] as EventoUsuarioRetorno;
                                        var evento = objeto[1] as Evento;
                                        
                                        eventoUsuarioRetorno.Evento = new List<Evento>();
                                        if (evento?.Nome != null)
                                            ((List<Evento>)eventoUsuarioRetorno.Evento).Add(evento);

                                        return eventoUsuarioRetorno;
                                    },
                                    param: new
                                    {
                                        IdentificadorUsuario = IdentificadorUsuario
                                    },
                                    splitOn: "IdentificadorEvento");

            return retorno.FirstOrDefault();
        }
    }
}
