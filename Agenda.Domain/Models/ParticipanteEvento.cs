using System;
using System.ComponentModel.DataAnnotations;

namespace Agenda.Domain.Models
{
    public class ParticipanteEvento
    {
        public Guid Identificador { get; set; }

        [Required(ErrorMessage = "O Identificador de usuário é obrigatório")]
        public Guid IdentificadorUsuario { get; set; }

        [Required(ErrorMessage = "O Identificador de evento é obrigatório")]
        public Guid IdentificadorEvento { get; set; }
    }
}
