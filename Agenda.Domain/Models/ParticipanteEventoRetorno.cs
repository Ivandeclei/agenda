using System.Collections.Generic;

namespace Agenda.Domain.Models
{
    public class ParticipanteEventoRetorno : Evento
    {
        public IEnumerable<Usuario> Usuario { get; set; }
    }
}
