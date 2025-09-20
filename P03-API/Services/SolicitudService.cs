using ParcialWebApi.Models;
using ParcialWebApi.Repositories;

namespace ParcialWebApi.Services
{
    public class SolicitudService : ISolicitudService
    {
        private readonly ISolicitudRepository _repository;
        public SolicitudService(ISolicitudRepository repository)
        {
            _repository = repository;
        }

        public bool Create(Solicitud solicitud)
        {
            return _repository.Create(solicitud);
        }

        public List<Solicitud> GetAll(string tipoCambio)
        {
            return _repository.GetAll(tipoCambio);
        }

        public Solicitud? GetById(int id)
        {
            return _repository?.GetById(id);
        }

        public bool Update(int id, string estado)
        {
            return _repository.Update(id, estado);
        }
    }
}
