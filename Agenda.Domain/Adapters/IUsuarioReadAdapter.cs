using System;
using System.Threading.Tasks;
using Agenda.Domain.Models;

namespace Agenda.Domain.Adapters
{
    public interface IUsuarioReadAdapter
    {
        Task<Usuario> BuscarUsuarioAsync(Guid identificador);

        Task<EventoUsuarioRetorno> BuscarUsuarioEventoAsync(Guid identificadorUsuario);
    }
}
