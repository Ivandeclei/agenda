using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Agenda.Domain.Models
{
    public class Evento : EventoBase
    {
        [Required(ErrorMessage = "O campo nome é obrigatório")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O campo decrição é obrigatório")]
        public string Descricao { get; set; }

        [Required(ErrorMessage = "O campo data é obrigatório")]
        public DateTimeOffset Data { get; set; }

        [Required(ErrorMessage = "O campo local é obrigatório")]
        public string Local { get; set; }

        [Required(ErrorMessage = "O tipo de evento é obrigatório")]
        public TipoEvento TipoEvento { get; set; }

        public IEnumerable<Usuario> Usuario { get; set; }
    }
}
