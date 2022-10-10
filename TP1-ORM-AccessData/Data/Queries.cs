
namespace TP1_ORM_AccessData.Data
{
    public class Queries
    {
        public Queries()
        {
            
        }

        public void ListarVentas()
        {
            using (var _context = new TiendaDbContext())
            {
                var ordenes = (from c in _context.Carritos
                               join cl in _context.Clientes
                               on c.ClienteId equals cl.ClienteId
                               join o in _context.Ordenes
                               on c.CarritoId equals o.CarritoId
                               join cp in _context.CarritoProductos
                               on c.CarritoId equals cp.CarritoId
                               join p in _context.Productos
                               on cp.ProductoId equals p.ProductoId
                               select new
                               {
                                   orden = o.OrdenId,
                                   fecha = o.Fecha,
                                   cliente = cl.Nombre,
                                   apellido = cl.Apellido,
                                   producto = p.Nombre,
                                   codigo = p.Codigo,
                                   cantidad = cp.Cantidad,
                                   total = o.Total,
                               }).OrderByDescending(x => x.fecha).ToList();
                

                if (ordenes.Count == 0)
                {
                    Console.WriteLine("No hay ventas realizadas en el dia de la fecha");
                    return;
                }
                else
                {
                    foreach (var venta in ordenes)
                    {
                        Console.WriteLine("Orden: " + venta.orden + "\n" +
                                          "Fecha: " + venta.fecha + "\n" +
                                           "Cliente: " + venta.cliente + " " + venta.apellido + "\n" +
                                           "Producto: " + venta.producto + "\n" +
                                           "Codigo: " + venta.codigo + "\n" +
                                           "Cantidad: " + venta.cantidad + "\n" +
                                           "Total: " + venta.total + "\n");
                    }
                }

                decimal montoTotal = _context.Ordenes.Sum(t => t.Total);

                Console.WriteLine("Total de ventas: " + ordenes.Count + "\n");
                Console.WriteLine("Monto total de ventas: $" + montoTotal);

                return;
            }
        }

        public void ListarProductosEnVentas()
        {
            using (var _context = new TiendaDbContext())
            {
                var ventas = (from c in _context.Carritos
                              join cl in _context.Clientes
                              on c.ClienteId equals cl.ClienteId
                              join o in _context.Ordenes
                              on c.CarritoId equals o.CarritoId
                              join cp in _context.CarritoProductos
                              on c.CarritoId equals cp.CarritoId
                              join p in _context.Productos
                              on cp.ProductoId equals p.ProductoId
                              select new
                              {
                                  orden = o.OrdenId,
                                  fecha = o.Fecha,
                                  cliente = cl.Nombre,
                                  apellido = cl.Apellido,
                                  producto = p.Nombre,
                                  codigo = p.Codigo,
                                  cantidad = cp.Cantidad,
                                  marca = p.Marca,
                                  nombre = p.Nombre,
                                  precio = p.Precio,
                                  descripcion = p.Descripcion
                              }).ToList();
                
                var op = "si";
                
                do
                {
                    Console.Write("Ingrese codigo de producto a buscar: ");
                    string codigo = Console.ReadLine();
                                   
                    var producto = ventas.FirstOrDefault(x => x.codigo == codigo);

                    if (producto != null) 
                    {
                        Console.WriteLine("Nombre del producto: " + producto.nombre + "\n" +
                                          "Codigo del producto: " + producto.codigo + "\n" +
                                          "Marca del producto: " + producto.marca + "\n" +
                                          "Descripcion del producto: " + producto.descripcion + "\n" +
                                          "Precio del producto: $" + producto.precio + "\n");

                        Console.Write("Desea buscar otro producto? si para seguir, no para volver al menu principal\n");
                        op = Console.ReadLine();
                    }
                    else
                    {
                        Console.WriteLine("\n El codigo ingresado es incorrecto");
                    }
                } while (op == "si");               
            }
        }
    }
}
