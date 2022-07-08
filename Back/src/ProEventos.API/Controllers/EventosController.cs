using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProEventos.Persistence;
using ProEventos.Domain;
using ProEventos.Persistence.Contextos;
using ProEventos.Application.Contratos;
using Microsoft.AspNetCore.Http;

namespace ProEventos.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventosController : ControllerBase
    {


        private readonly IEventoService eventoService;

        public EventosController(IEventoService eventoService)
        {
            this.eventoService = eventoService;

        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var eventos = await eventoService.GetAllEventosAsync(true);
                if (eventos == null) return NotFound("Nenhum evento encontrado.");

                return Ok(eventos);
            }
            catch (Exception ex)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError, $"erro ao tentar recuperar eventos {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById (int id)
        {
            try
            {
                var evento = await eventoService.GetEventoByIdAsync(id, true);
                if (evento == null) return NotFound("evento por id encontrado.");

                return Ok(evento);
            }
            catch (Exception ex)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError, 
                $"erro ao tentar recuperar eventos. erro: {ex.Message}");
            }
        }

    [HttpGet("{tema}/tema")]
        public async Task<IActionResult> GetByTema (string tema)
        {
            try
            {
                var evento = await eventoService.GetAllEventosByTemaAsync(tema, true);
                if (evento == null) return NotFound("eventos por tema não encontrados.");

                return Ok(evento);
            }
            catch (Exception ex)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError, 
                $"erro ao tentar recuperar eventos. erro: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post( Evento model)
        {
            try
            {
                var evento = await eventoService.AddEventos(model);
                if (evento == null) return BadRequest("ero ao tentar ao adicionar evento.");

                return Ok(evento);
            }
            catch (Exception ex)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError,
                $"erro ao tentar adicionar eventos. erro: {ex.Message}");
            }
        }
        

        [HttpPut("{id}")]
        public async Task<IActionResult> Put( int id, Evento model)
        {
            try
            {
                var evento = await eventoService.UpdateEvento(id, model);
                if (evento == null) return BadRequest("ero ao tentar efetuar o upload evento.");

                return Ok(evento);
            }
            catch (Exception ex)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError,
                $"erro ao tentar atualizer eventos. erro: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                if(await eventoService.DeleteEvento(id))
                    return Ok ("deletado");
                
                else
                    return BadRequest("evento não deletado");
                
            }
            catch (Exception ex)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError,
                $"erro ao tentar deletar eventos. erro: {ex.Message}");
            }
        }
    }
}
