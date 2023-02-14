using System;
using System.Linq;
using Agenda.Domain.Exceptions;
using Agenda.Domain.Models;
using Agenda.Domain.Services;

namespace Agenda.Application
{
    public class ValidacaoUsuarioService : IValidaUsuarioService
    {
        private readonly IValidacaoBaseService validacaoBaseService;

        public ValidacaoUsuarioService( IValidacaoBaseService validacaoBaseService)
        {
            this.validacaoBaseService = validacaoBaseService ?? 
                throw new ArgumentNullException(nameof(validacaoBaseService));  
        }

        public void ValidaUsuario(Usuario usuario)
        {
            var erros = new CoreException();

            erros = validacaoBaseService.VerificarCamposObrigatorios<Usuario>(
                erros, usuario);

            if (erros.Errors.Any())
            {
                throw CoreException.Exception(erros.Errors.ToList());
            }
        }
    }
}
