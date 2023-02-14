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
    public class UsuarioWriteAdapter : IUsuarioWriteAdapter
    {
        private readonly IDbConnection dbConnection;
        private readonly IMapper mapper;

        static UsuarioWriteAdapter() => SqlMapper.AddTypeMap(typeof(string), DbType.AnsiString);

        public UsuarioWriteAdapter(IDbConnection dbConnection, IMapper mapper)
        {
            this.dbConnection = dbConnection ??
                throw new ArgumentNullException(nameof(dbConnection));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        public async Task<Usuario> AtualizarUsuarioAsync(Usuario usuario)
        {
            var retorno = await dbConnection.QueryAsync<Usuario>(
                @"UPDATE [dbo].[Usuario]
                       SET 
                          [Nome] = @Nome
                          ,[Login] = @Login
                                 
                     WHERE identificadorUsuario = @Identificador

                    Select 
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
                        Identificador = usuario.IdentificadorUsuario,
                        Nome = usuario.Nome,
                        Login = usuario.Login,

                    });

            return retorno.FirstOrDefault<Usuario>();
        }

        public Task DeletarUsuarioAsync(Guid identificadorEvento)
        {
            throw new NotImplementedException();
        }

        public async Task SalvarUsuarioAsync(Usuario usuario)
        {
            await dbConnection.QueryAsync<Usuario>(
              @"INSERT INTO [dbo].[Usuario](
                                [Nome]
                               ,[Login]
                               ,[Senha]
                               )
                         VALUES
                        (
                                @Nome,
                                @Login,
                                @Senha
                        )",
              param: new
              {
                  Nome = usuario.Nome,
                  Login = usuario.Login,
                  Senha = usuario.Senha,
                  
              });
        }
    }
}
