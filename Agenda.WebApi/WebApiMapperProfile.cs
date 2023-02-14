using Agenda.Domain.Models;
using Agenda.WebApi.Dtos;
using AutoMapper;

namespace Agenda.WebApi
{
    public class WebApiMapperProfile : Profile
    {
        public WebApiMapperProfile()
        {
            CreateMap<EventoDto, Evento>().ReverseMap();
            CreateMap<EventoPost, Evento>().ReverseMap();
            CreateMap<EventoPost, UsuarioBase>()
               .ForPath(d => d.IdentificadorUsuario, o => o.MapFrom(s => s.UsuarioIdentificador.IdentificadorUsuario));
            CreateMap<UsuarioBaseDto, UsuarioBase>().ReverseMap();
            CreateMap<UsuarioDto, Usuario>().ReverseMap();
            CreateMap<EventoUsuarioRetornoDto, EventoUsuarioRetorno>().ReverseMap();
            CreateMap<ParticipanteEventoDto, ParticipanteEvento>().ReverseMap();
            CreateMap<ParticipanteEventoRetornoDto, ParticipanteEventoRetorno>().ReverseMap();
        }
    }
}
