using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProEventos.Domain;
using ProEventos.Persistence.Contextos;
using ProEventos.Persistence.Contratos;

namespace ProEventos.Persistence
{
    public class EventoPersist : IEventoPersist
    {
        public ProEventosContext Context { get; set; }
        private readonly ProEventosContext context;
        public EventoPersist(ProEventosContext context)
        {
            this.context = context;
            this.Context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }
        public async Task<Evento[]> GetAllEventosAsync(bool IncludePalestrantes)
        {
            IQueryable<Evento> query = context.Eventos
            .Include(e => e.Lotes)
            .Include(e => e.RedesSociais);

            if (IncludePalestrantes)
            {
                query = query
                .Include(e => e.PalestrantesEventos)
                .ThenInclude(pe => pe.Palestrante);
            }

            query = query.OrderBy(e => e.Id);

            return await query.ToArrayAsync();
        }

        public async Task<Evento[]> GetAllEventosByTemaAsync(string tema, bool includePalestrantes = false)
        {
            IQueryable<Evento> query = context.Eventos
                .Include(e => e.Lotes)
                .Include(e => e.RedesSociais);

            if (includePalestrantes)
            {
                query = query
                .Include(e => e.PalestrantesEventos)
                .ThenInclude(pe => pe.Palestrante);
            }

            query = query
            .OrderBy(e => e.Id)
            .Where(e => e.Tema.ToLower().Contains(tema.ToLower()));

            return await query.ToArrayAsync();
        }

        public async Task<Evento> GetEventoByIdAsync(int eventoId, bool includePalestrantes = false)
        {
            IQueryable<Evento> query = context.Eventos
            .Include(e => e.Lotes)
            .Include(e => e.RedesSociais);

            if (includePalestrantes)
            {
                query = query
                .Include(e => e.PalestrantesEventos)
                .ThenInclude(pe => pe.Palestrante);
            }

            query = query
            .OrderBy(e => e.Id)
            .Where(e => e.Id == eventoId);

            return await query.FirstOrDefaultAsync();
        }

    }
};