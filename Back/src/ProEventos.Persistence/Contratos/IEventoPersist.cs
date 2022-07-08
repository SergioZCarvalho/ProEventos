using System.Threading.Tasks;
using ProEventos.Domain;

namespace ProEventos.Persistence.Contratos
{
    public interface IEventoPersist
    {

        Task<Evento[]> GetAllEventosByTemaAsync(string tema, bool IncludePalestrantes);
        Task<Evento[]> GetAllEventosAsync(bool IncludePalestrantes);
        Task<Evento> GetEventoByIdAsync(int eventoId, bool IncludePalestrantes);

    }
}