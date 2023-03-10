using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Agenda.Domain.Exceptions;
using Agenda.Domain.Services;

namespace Agenda.Application
{
    public class ValidacaoBaseService : IValidacaoBaseService
    {
        public CoreException VerificarCamposObrigatorios<T>(CoreException erros, T classe)
        {
            if (classe == null)
            {
                throw new System.ArgumentNullException(nameof(classe));
            }

            var coreException = new CoreException();

            if (!ModelValidator.TryValidate(classe, out IEnumerable<ValidationResult> errors))
            {
                foreach (var item in errors)
                {
                    coreException.Errors.Add(new CoreError()
                    {
                        Key = item.MemberNames.FirstOrDefault(),
                        Message = item.ErrorMessage
                    });
                }

                if (coreException.Errors.Any())
                {
                    throw CoreException.Exception(coreException.Errors.ToList());
                }

                return coreException;
            }

            return coreException;
        }
    }
}
