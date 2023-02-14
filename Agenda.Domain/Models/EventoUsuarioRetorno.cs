using System.Collections.Generic;

namespace Agenda.Domain.Models
{
    public class EventoUsuarioRetorno : Usuario
    {
        public IEnumerable<Evento> Evento { get; set; }
    }
}
