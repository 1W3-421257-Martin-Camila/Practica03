using ParcialWebApi.Models;
using ParcialWebApi.Repositories;

namespace ParcialWebApi.Services
{
    public interface ISolicitudService
    {
        List<Solicitud> GetAll(string tipoCambio);
        Solicitud? GetById(int id);
        bool Create(Solicitud solicitud);
        bool Update(int id, string estado);
    }
}
