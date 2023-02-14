using Agenda.Domain.Exceptions;

namespace Agenda.Domain.Services
{
    public interface IValidacaoBaseService
    {
        /// <summary>
        /// Valida campos obrigatorios do modelo
        /// </summary>
        /// <typeparam name="T">Modelo a ser validado</typeparam>
        CoreException VerificarCamposObrigatorios<T>(CoreException erros, T classe);

    }
}
