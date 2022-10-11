
using TP1_ORM_AccessData.Data;
using TP1_ORM_AccessData.Entities;
using TP1_ORM_Core.Validations;

namespace TP1_ORM_Core.Services
{
    public class CarritoServices
    {
        private readonly Validate _validate;
        public readonly ClientesServices _cliente;

        public CarritoServices()
        {
            _validate = new Validate();
            _cliente = new ClientesServices();

        }
        //Traemos Carrito por Guid Id
        public Carrito GetCarritoId(Guid Id)
        {
            using (var _context = new TiendaDbContext())
            {
                return _context.Carritos.SingleOrDefault(x => x.CarritoId == Id);
            }
        }

        //Modificamos el carrito con otro producto
        public void ModifyCarrito(Guid Id, string dni)
        {
            using (var _context = new TiendaDbContext())
            {
                if (_validate.ValidarCliente(dni) != null)
                {
                    var cliente = _cliente.GetCliente(dni);

                    Carrito carrito = (from x in _context.Carritos where x.CarritoId == Id select x).FirstOrDefault();

                    carrito.ClienteId = cliente.ClienteId;

                    carrito.Estado = true;

                    _context.SaveChanges();
                }
               
            }
        }
    }
}
