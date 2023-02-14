using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Agenda.Domain.Exceptions;
using Agenda.Domain.Models;
using Agenda.Domain.Services;
using Agenda.WebApi.Dtos;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace Agenda.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventoController : ControllerBase
    {
        private readonly IEventoService eventoService;

        private readonly IMapper mapper;
        public EventoController(IEventoService eventoService, IMapper mapper)
        {
            this.eventoService = eventoService ?? throw new ArgumentNullException(nameof(eventoService));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }


        [HttpGet("BuscarEventoParticipante")]
        [ProducesResponseType(typeof(ParticipanteEventoRetornoDto), 200)]
        public async Task<IActionResult> BuscarEventoParticipanteAsync(
            [FromQuery] Guid identificadorEvento)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var condominio = await eventoService.BuscarEventoParticipanteAsync(identificadorEvento);

                var resultado = mapper.Map<ParticipanteEventoRetornoDto>(condominio);

                return Ok(resultado);
            }
            catch (CoreException e)
            {
                return StatusCode((int)HttpStatusCode.BadRequest, e.Errors);
            }
            catch (Exception e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [HttpGet("BuscarEventos")]
        [ProducesResponseType(typeof(ParticipanteEventoRetornoDto), 200)]
        public async Task<IActionResult> BuscarEventosAsync()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var condominio = await eventoService.BuscarEventosAsync();

                var resultado = mapper.Map<IEnumerable<ParticipanteEventoRetornoDto>>(condominio);

                return Ok(resultado);
            }
            catch (CoreException e)
            {
                return StatusCode((int)HttpStatusCode.BadRequest, e.Errors);
            }
            catch (Exception e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
            }
        }

        /// <summary>
        /// Salvar Um Evento
        /// </summary>
        /// <param name="evento">
        /// Objeto de evento a ser salvo 
        /// </param>
        [HttpPost("SalvarEvento")]
        [ProducesResponseType(typeof(EventoDto), 200)]
        public async Task<IActionResult> SalvarEventoAsync(
            [FromBody] EventoPost evento)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var eventoParametro = mapper.Map<Evento>(evento);
                await eventoService.SalvarEventoAsync(eventoParametro, evento.UsuarioIdentificador.IdentificadorUsuario);
                return Ok();
            }
            catch (CoreException e)
            {
                return StatusCode((int)HttpStatusCode.BadRequest, e.Errors);
            }
            catch (Exception e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="participanteEvento"></param>
        /// <returns></returns>
        [HttpPost("SalvarParticipanteEvento")]
        [ProducesResponseType(typeof(EventoDto), 201)]
        public async Task<IActionResult> SalvarParticipanteEventoAsync(
            [FromBody] ParticipanteEventoDto participanteEvento)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var participanteEventoParamtero = mapper.Map<ParticipanteEvento>(participanteEvento);
                await eventoService.SalvarParticipanteEventoAsync(participanteEventoParamtero);
                return Ok();
            }
            catch (CoreException e)
            {
                return StatusCode((int)HttpStatusCode.BadRequest, e.Errors);
            }
            catch (Exception e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
            }
        }


        [HttpPut("AtualizarEvento")]
        [ProducesResponseType(typeof(EventoDto), 201)]
        public async Task<IActionResult> AtualizarEventoAsync(
            [FromBody] EventoPost evento)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var eventoParametro = mapper.Map<Evento>(evento);
                var resultado = await eventoService.AtualizarEventoAsync(eventoParametro, evento.UsuarioIdentificador.IdentificadorUsuario);
                var resultadoFinal = mapper.Map<EventoDto>(resultado);
                return Ok(resultadoFinal);
            }
            catch (CoreException e)
            {
                return StatusCode((int)HttpStatusCode.BadRequest, e.Errors);
            }
            catch (Exception e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
            }
        }
    }
}
