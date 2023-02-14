using System;

namespace Agenda.WebApi.Dtos
{
    public class ParticipanteEventoDto
    {
        /// <summary>
        /// Identificador do Usuario Participante
        /// </summary>
        public Guid IdentificadorUsuario { get; set; }

        /// <summary>
        /// Identificador do Evento
        /// </summary>
        public Guid IdentificadorEvento { get; set; }
    }
}
