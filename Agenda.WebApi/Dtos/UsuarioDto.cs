using System;

namespace Agenda.WebApi.Dtos
{
    public class UsuarioDto : UsuarioBaseDto
    {
        /// <summary>
        /// Nome
        /// </summary>
        public string Nome { get; set; }

       /// <summary>
       /// Login de usuário
       /// </summary>
        public string Login { get; set; }

        /// <summary>
        /// Senha
        /// </summary>
        public string Senha { get; set; }
    }
}
