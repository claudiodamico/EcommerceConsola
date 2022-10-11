
using TP1_ORM_AccessData.Data;

namespace TP1_ORM_Core.Validations
{
    public class Validate
    {
        //Validacion de productos
        public string? ValidarProducto(string codigo)
        {
            using (var _context = new TiendaDbContext())
            {
                var productoValidado = _context.Productos.SingleOrDefault(x => x.Codigo == codigo);

                if (productoValidado != null && productoValidado.ToString() != "")
                {
                    return codigo;
                }
                else
                {
                    return null;
                }              
            }
        }
        //Validacion de cliente
        public string? ValidarCliente(string dni)
        {
            using (var _context = new TiendaDbContext())
            {
                var clienteValidado = _context.Clientes.SingleOrDefault(x => x.Dni == dni.ToString());

                if (clienteValidado.Dni != null && clienteValidado.Dni != "")
                { 
                    return dni;
                }
                else
                {
                    return null;
                }
            }               
        }
    }
}
