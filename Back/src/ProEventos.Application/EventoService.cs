using System;
using System.Threading.Tasks;
using ProEventos.Application.Contratos;
using ProEventos.Domain;
using ProEventos.Persistence.Contratos;

namespace ProEventos.Application
{
    public class EventoService : IEventoService
    {
        private readonly IGeralPersist geralPersist;
        private readonly IEventoPersist eventoPersist;
        public EventoService(IGeralPersist geralPersist, IEventoPersist eventoPersist)
        {
            this.eventoPersist = eventoPersist;
            this.geralPersist = geralPersist;

        }
        public async Task<Evento> AddEventos(Evento model)
        {
            try
            {
                geralPersist.Add<Evento>(model);
                if (await geralPersist.SaveChangesAsync())
                {
                    return await eventoPersist.GetEventoByIdAsync(model.Id, false);
                }
                return null;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
        public async Task<Evento> UpdateEvento(int eventoId, Evento model)
        {
            try
            {
                var evento = await eventoPersist.GetEventoByIdAsync(eventoId, false);
                if (evento == null) return null;

                model.Id = evento.Id;

                geralPersist.Update(model);

                if (await geralPersist.SaveChangesAsync())
                {
                    return await eventoPersist.GetEventoByIdAsync(model.Id, false);
                }
                return null;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
        public async Task<bool> DeleteEvento(int eventoId)
        {
            try
            {
                var evento = await eventoPersist.GetEventoByIdAsync(eventoId, false);
                if (evento == null) throw new Exception("Evento para o delete não foi encontrado. ");


                geralPersist.Delete<Evento>(evento);
                return await geralPersist.SaveChangesAsync();

            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public async Task<Evento[]> GetAllEventosAsync(bool IncludePalestrantes = false)
        {
            try
            {
                var eventos = await eventoPersist.GetAllEventosAsync(IncludePalestrantes);
                if (eventos == null) return null;

                return eventos;

            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public async Task<Evento[]> GetAllEventosByTemaAsync(string tema, bool includePalestrantes = false)
        {
            try
            {
                var eventos = await eventoPersist.GetAllEventosByTemaAsync(tema, includePalestrantes);
                if (eventos == null) return null;

                return eventos;

            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public async Task<Evento> GetEventoByIdAsync(int eventoId, bool includePalestrantes = false)
        {
            try
            {
                var eventos = await eventoPersist.GetEventoByIdAsync(eventoId, includePalestrantes);
                if (eventos == null) return null;

                return eventos;

            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

    }
}