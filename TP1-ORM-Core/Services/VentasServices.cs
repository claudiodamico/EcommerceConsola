
using TP1_ORM_AccessData.Data;
using TP1_ORM_AccessData.Entities;
using TP1_ORM_Core.Validations;

namespace TP1_ORM_Core.Services
{
    public class VentasServices
    {
        private readonly Validate _validate;
        private readonly ProductosServices _productosServices;
        private readonly ClientesServices _clientesServices;
        private readonly OrdenSevices _ordenSevices;
        private readonly CarritoServices _carritoServices;

        public VentasServices()
        {
            _validate = new Validate();
            _productosServices = new ProductosServices();
            _clientesServices = new ClientesServices();
            _ordenSevices = new OrdenSevices();
            _carritoServices = new CarritoServices();
        }

        public void ListarVentas()
        {
            using (var _context = new TiendaDbContext())
            {
                ProductosServices productos = new ProductosServices();

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

                Console.WriteLine("Total de ventas: " + ordenes.Count);
                Console.WriteLine("Monto total de ventas: " + montoTotal);
                                              
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

                Console.Write("Ingrese codigo de producto a buscar: ");
                string codigo = Console.ReadLine();

                var producto = ventas.FirstOrDefault(x => x.codigo == codigo);
                if (codigo != "" && codigo != null)
                {
                    Console.WriteLine("Nombre del producto: " + producto.nombre + "\n" +
                                      "Codigo del producto: " + producto.codigo + "\n" +
                                      "Marca del producto: " + producto.marca + "\n" +
                                      "Descripcion del producto: " + producto.descripcion + "\n" +
                                      "Precio del producto: $" + producto.precio + "\n");
                }
                else
                    Console.WriteLine("\n El codigo ingresado es incorrecto");

            }
        }

        public void RegistrarVenta()
        {
            using (var _context = new TiendaDbContext())
            {
                var carrito = new Carrito
                {
                    CarritoId = Guid.NewGuid(),
                    ClienteId = 1,
                    Estado = false
                };

                _context.Carritos.Add(carrito);

                try
                {
                    decimal total = 0;

                    Console.WriteLine("Ingrese su DNI: ");
                    string dni = Console.ReadLine();

                    if (_validate.ValidarCliente(dni) == null)
                    {
                        Console.WriteLine("No esta registrado en el sistema\n");
                        _clientesServices.RegistraCliente();
                    };
                    

                    Console.WriteLine("Desea comprar un producto? si para seguir, no para volver al menu principal\n");

                    string op = Console.ReadLine();

                    while (op == "si")
                    {
                        _productosServices.ListarProductos();

                        Console.Write("Seleccione el codigo del producto que desea comprar: \n");
                        string codigo = Console.ReadLine();

                        var producto = _productosServices.ProductoByCodigo(codigo);

                        while (_validate.ValidarProducto(codigo) == null)
                        {
                            Console.WriteLine("El codigo no existe en el catalaogo de productos.\n" +
                                          "Por favor vuelva a intentar con otro codigo valido.\n"); 

                            codigo = Console.ReadLine();
                            producto = _productosServices.ProductoByCodigo(codigo);
                        }

                        Console.Write("Cantidad de unidades: ");
                        int cantidad = int.Parse(Console.ReadLine());

                        if (cantidad != 0 && cantidad != null)
                        {
                            var itemCarrito = new CarritoProducto
                            {
                                CarritoId = carrito.CarritoId,
                                ProductoId = producto.ProductoId,
                                Cantidad = cantidad
                            };
                            _context.CarritoProductos.Add(itemCarrito);
                            _context.SaveChanges();

                            total = producto.Precio * cantidad;

                            Console.Write("Desea agregar otro producto al carrito? \nsi/no: \n");
                            op = Console.ReadLine();

                            while (op == "si")
                            {
                                _productosServices.ListarProductos();
                                Console.Write("Seleccione el codigo del producto que desea agregar al carrito: \n");
                                codigo = Console.ReadLine();

                                producto = _productosServices.ProductoByCodigo(codigo);

                                if (_validate.ValidarProducto(codigo) == null)
                                {
                                    Console.WriteLine("El codigo no existe en el catalaogo de productos.\n" +
                                                  "Por favor vuelva a intentar con otro codigo valido.\n" +
                                                  "Presione una tecla para volver al menú principal.");
                                    Console.ReadKey();
                                    return;
                                }

                                Console.Write("Cantidad de unidades: ");
                                cantidad = int.Parse(Console.ReadLine());

                                if (cantidad != 0 && cantidad != null)
                                {
                                    _carritoServices.ModifyCarrito(carrito.CarritoId, dni);
                                    _ordenSevices.AddOrden(carrito.CarritoId, total);
                                    _context.SaveChanges();
                                    Console.WriteLine("Se ha agregado el producto exitosamente!");
                                    Console.Write("Desea agregar otro producto al carrito? \nsi/no: \n");
                                    op = Console.ReadLine();
                                }
                            }
                            Console.WriteLine("Se ha realizado la venta exitosamente!");
                        }
                        else
                        {
                            Console.WriteLine("ocurrio un error!");
                        }
                    }                   
                }
                catch (Exception ex)
                {
                    Console.WriteLine("ocurrio un error!");
                    Console.ReadKey();
                    Console.Clear();
                }
            }
        }
    }
}
