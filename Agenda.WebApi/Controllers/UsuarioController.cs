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
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioService usuarioService;

        private readonly IMapper mapper;

        public UsuarioController(IUsuarioService usuarioService, IMapper mapper)
        {
            this.usuarioService = usuarioService ?? throw new ArgumentNullException(nameof(usuarioService));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet("BuscarUsuario")]
        [ProducesResponseType(typeof(EventoUsuarioRetornoDto), 200)]
        public async Task<IActionResult> BuscarEventosAsync(
            [FromQuery] Guid identificadorUsuario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var retorno = await usuarioService.BuscarUsuarioAsync(identificadorUsuario);

                var resultado = mapper.Map<EventoUsuarioRetornoDto>(retorno);

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

        [HttpPost("SalvarUsuario")]
        [ProducesResponseType(typeof(UsuarioDto), 200)]
        public async Task<IActionResult> SalvarEventoAsync(
            [FromBody] UsuarioDto usuario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var usuarioMap = mapper.Map<Usuario>(usuario);
                await usuarioService.SalvarUsuarioAsync(usuarioMap);
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

        [HttpPut("AtualizarUsuario")]
        [ProducesResponseType(typeof(EventoDto), 201)]
        public async Task<IActionResult> AtualizarUsuarioAsync(
            [FromBody] UsuarioDto usuario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var usuarioMap = mapper.Map<Usuario>(usuario);
                var resultado = await usuarioService.AtualizarEventoAsync(usuarioMap);
                var resultadoFinal = mapper.Map<UsuarioDto>(resultado);
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
