using Agenda.Domain.Models;

namespace Agenda.Domain.Services
{
    public interface IValidaEventoService
    {
        void ValidaEvento(Evento evento);
        void ValidaParticipanteEvento(ParticipanteEvento participanteEvento);
    }
}
