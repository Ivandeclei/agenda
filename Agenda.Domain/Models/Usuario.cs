using System;
using System.ComponentModel.DataAnnotations;

namespace Agenda.Domain.Models
{
    public class Usuario : UsuarioBase
    {
        [Required(ErrorMessage = "O campo nome é obrigatório")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O campo Login é obrigatório")]
        public string Login { get; set; }
       
        [Required(ErrorMessage = "A campo senha é obrigatório")]
        public string Senha { get; set; }
    }
}
