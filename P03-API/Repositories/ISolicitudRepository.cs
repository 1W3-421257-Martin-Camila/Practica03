using ParcialWebApi.Models;

namespace ParcialWebApi.Repositories
{
    public interface ISolicitudRepository
    {
        List<Solicitud> GetAll(string tipoCambio);
        Solicitud? GetById(int id);
        bool Create(Solicitud solicitud);
        bool Update(int id, string estado);
    }
}
