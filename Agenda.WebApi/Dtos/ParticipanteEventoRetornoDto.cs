using System.Collections.Generic;

namespace Agenda.WebApi.Dtos
{
    public class ParticipanteEventoRetornoDto : EventoDto
    {
        /// <summary>
        /// Usuarios Participantes do evento
        /// </summary>
        public IEnumerable<UsuarioDto> Usuario { get; set; }
    }
}
