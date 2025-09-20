using Microsoft.AspNetCore.Http.HttpResults;
using ParcialWebApi.Models;

namespace ParcialWebApi.Repositories
{
    public class SolicitudRepository : ISolicitudRepository
    {
        private readonly SolicitudesContext _context;
        public SolicitudRepository(SolicitudesContext context)
        {
           _context = context;
        }
        public bool Create(Solicitud solicitud)
        {
            _context.Solicitudes.Add(solicitud);
            return _context.SaveChanges() > 0;
        }

        public List<Solicitud> GetAll(string tipoCambio)
        {
            return _context.Solicitudes.Where(s => (s.TipoCambioNavigation.Nombre.Equals("Correctivo")
                || s.TipoCambioNavigation.Nombre.Equals("Bug")
                || s.TipoCambioNavigation.Nombre.Equals("Evolutivo"))
                && (s.Estado.Equals("Abierta")
                || s.Estado.Equals("En Proceso")))
                .ToList();
        }

        public Solicitud? GetById(int id)
        {
            return _context.Solicitudes.Find(id);
        }

        public bool Update(int id, string estado)
        {
            var solicitud = GetById(id);

            // Verifica que no esté cancelada ni que se guarde como cancelada
            if (solicitud == null
                || solicitud.Estado.Equals("Cancelada")
                || estado.Equals("Cancelada"))
                return false;

            solicitud.Estado = estado;
            return _context.SaveChanges() > 0;
        }

    }
}
