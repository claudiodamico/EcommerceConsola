
using TP1_ORM_AccessData.Data;
using TP1_ORM_AccessData.Entities;

namespace TP1_ORM_Core.Services
{
    public class OrdenSevices
    {
        //Generamos una orden nueva
        public void AddOrden(Guid Id, decimal total)
        {
            using (var _context = new TiendaDbContext())
            {
                var orden = new Orden
                {
                    OrdenId = Guid.NewGuid(),
                    CarritoId = Id,
                    Fecha = DateTime.Now,
                    Total = total
                };
                _context.Ordenes.Add(orden);
                _context.SaveChanges();
            }
        }
    }
}
