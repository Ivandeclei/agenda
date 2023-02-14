using System;
using System.Threading.Tasks;
using Agenda.Domain.Models;

namespace Agenda.Domain.Adapters
{
    public interface IUsuarioWriteAdapter
    {
        /// <summary>
        /// Insere na base de dados um novo registro de banco de dados
        /// </summary>
        /// <param name="usuario"> 
        /// Objeto de usuario
        /// </param>
        Task SalvarUsuarioAsync(Usuario usuario);

        /// <summary>
        /// Atualiza um registro de usuario na base de dados
        /// </summary>
        /// <param name="usuario">
        /// Objeto de usuario
        /// </param>
        /// <returns>
        /// Retorna objeto de usuario atualizado
        /// </returns>
        Task<Usuario> AtualizarUsuarioAsync(Usuario usuario);

        /// <summary>
        /// Deletar registro do banco de dados 
        /// </summary>
        /// <param name="identificadorEvento">
        /// Identificador do registro a ser apagado
        /// </param>
        Task DeletarUsuarioAsync(Guid identificadorEvento);
    }
}
