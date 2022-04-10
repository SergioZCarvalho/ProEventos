using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProEventos.API.Models;

namespace ProEventos.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventoController : ControllerBase
    {

        public IEnumerable<Evento> _evento = new Evento[]
                    {
                new Evento () {
                EventoId = 1,
                Tema = "Inauguração Outback",
                Local = "Mogi das Cruzes",
                lote = "Lote Unico",
                QtdPessoas = 210,
                DataEvento = DateTime.Now.AddDays(1).ToString("dd/MM/yyyy"),
                ImagemURL = "Logo do Outback"
                },
                new Evento () {
                EventoId = 2,
                Tema = "Inauguração Starbucks",
                Local = "Mogi das Cruzes",
                lote = "Lote Unico",
                QtdPessoas = 50,
                DataEvento = DateTime.Now.AddDays(1).ToString("dd/MM/yyyy"),
                ImagemURL = "Logo da Starbucks"
                }
                    };

        public EventoController()
        {
        }

        [HttpGet]
        public IEnumerable<Evento> Get()
        {
            return _evento;
        }

        [HttpGet("{id}")]
        public IEnumerable<Evento> Get(int id)
        {
            return _evento.Where(evento => evento.EventoId == id);
        }

        [HttpPost]
        public string Post()
        {
            return "teste post";
        }

        [HttpPut("{id}")]
        public string Put(int id)
        {
            return $"teste Put com id = {id}";
        }

        [HttpDelete("{id}")]
        public string Delete(int id)
        {
            return $"teste Delete com id = {id}";
        }
    }
}
