using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Agenda.Domain.Models;

namespace Agenda.Domain.Adapters
{
    public interface IEventoReadAdapter
    {
        Task<ParticipanteEventoRetorno> BuscarEventoParticipanteAsync(Guid identificadorEvento);

        Task<IEnumerable<ParticipanteEventoRetorno>> BuscarEventoAsync(TipoEvento tipoEvento);

    }
}
