using TP1_ORM_AccessData.Data;
using TP1_ORM_AccessData.Entities;

namespace TP1_ORM_Core.Services
{
    public class ProductosServices
    {
        //Listamos productos existentes para la venta
        public void ListarProductos()
        {
            using (var _context = new TiendaDbContext())
            {
                List<Producto> productos = (from l in _context.Productos where l.ProductoId > 0 select l).OrderBy(l => l.Nombre).ToList();
                if (productos.Count == 0)
                {
                    Console.WriteLine("No hay stock del producto.");
                    return;
                }
                foreach (var producto in productos)
                {
                    Console.WriteLine("Nombre: " + producto.Nombre + " " + "Marca: " + " " + producto.Marca + " " + "Codigo: " + " " + producto.Codigo + " " + "Precio: $" + producto.Precio);
                }
            }
        }
        //Traemos producto por su codigo
        public Producto ProductoByCodigo(string codigo)
        {
            using (var _context = new TiendaDbContext())
            {
                var producto = _context.Productos.FirstOrDefault(x => x.Codigo == codigo);

                return producto;
            }
        }
    }
}
