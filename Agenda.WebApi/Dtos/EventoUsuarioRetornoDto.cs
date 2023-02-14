using System.Collections.Generic;

namespace Agenda.WebApi.Dtos
{
    public class EventoUsuarioRetornoDto : UsuarioDto
    {
        /// <summary>
        /// Evento que o usuario esta participando
        /// </summary>
        public IEnumerable<EventoDto> Evento { get; set; }
    }
}
