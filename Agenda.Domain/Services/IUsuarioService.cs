using System;
using System.Threading.Tasks;
using Agenda.Domain.Models;

namespace Agenda.Domain.Services
{
    public interface IUsuarioService
    {
        /// <summary>
        /// Buscar usuario e seus eventos de agenda
        /// </summary>
        /// <param name="IdentificadorUsuario">
        /// Propriedade identificadora do usuario
        /// </param>
        /// <returns>
        /// Retorna o objeto de usuario e eventos em que participa
        /// </returns>
        Task<EventoUsuarioRetorno> BuscarUsuarioAsync(Guid identificadorUsuario);

        /// <summary>
        /// Salva usuário na base de dados
        /// </summary>
        /// <param name="usuario">
        /// Objeto de usuário
        /// </param>
        Task SalvarUsuarioAsync(Usuario usuario);

        /// <summary>
        /// Atualiza registro do usuário
        /// </summary>
        /// <param name="usuario">
        /// Objeto de usuario
        /// </param>
        /// <returns>
        /// Retorna Objeto de usuario atualizado
        /// </returns>
        Task<Usuario> AtualizarEventoAsync(Usuario usuario);

        /// <summary>
        /// Exclui da base de dados registro de usuario
        /// </summary>
        /// <param name="IdentificadorUsuario">
        /// Propriedade identificadora do Usuario
        /// </param>
        
        Task DeletarUsuarioAsync(Guid identificadorUsuario);
    }
}
