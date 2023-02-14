using System;
using System.Linq;
using Agenda.Domain.Exceptions;
using Agenda.Domain.Models;
using Agenda.Domain.Services;

namespace Agenda.Application
{
    public class ValidacaoEventoService : IValidaEventoService
    {
        private readonly IValidacaoBaseService validacaoBaseService;

        public ValidacaoEventoService(IValidacaoBaseService validacaoBaseService)
        {
            this.validacaoBaseService = validacaoBaseService ??
                throw new ArgumentNullException(nameof(validacaoBaseService));
        }

        public void ValidaEvento(Evento evento)
        {
            var erros = new CoreException();

            erros = validacaoBaseService.VerificarCamposObrigatorios<Evento>(
                erros, evento);

            if (erros.Errors.Any())
            {
                throw CoreException.Exception(erros.Errors.ToList());
            }
        }

        public void ValidaParticipanteEvento(ParticipanteEvento participanteEvento)
        {
            var erros = new CoreException();

            erros = validacaoBaseService.VerificarCamposObrigatorios<ParticipanteEvento>(
                erros, participanteEvento);

            if (erros.Errors.Any())
            {
                throw CoreException.Exception(erros.Errors.ToList());
            }
        }
    }
}
