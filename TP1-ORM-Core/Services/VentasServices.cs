
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
        private readonly Queries _queries;
        public VentasServices()
        {
            _validate = new Validate();
            _productosServices = new ProductosServices();
            _clientesServices = new ClientesServices();
            _ordenSevices = new OrdenSevices();
            _carritoServices = new CarritoServices();
            _queries = new Queries();
        }      
        public void ListaDeVentas()
        {
            _queries.ListarVentas();
        }       
        public void ListaDeProductosVendidos()
        {
            _queries.ListarProductosEnVentas();
        }
        public void RegistrarVenta()
        {
            using (var _context = new TiendaDbContext())
            {
                var carrito = new Carrito
                {
                    CarritoId = Guid.NewGuid(),
                    ClienteId = 1, //Cliente harcodeado para poder realizar la venta
                    Estado = true
                };

                _context.Carritos.Add(carrito);

                try
                {
                    decimal total = 0;

                    Console.WriteLine("Ingrese su DNI: ");
                    string dni = Console.ReadLine();
                    //Validamos que el cliente este registrado para poder realizar la compra
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
                                    carrito.Estado = false;
                                    _context.SaveChanges();
                                    Console.WriteLine("Se ha agregado el producto exitosamente!");
                                    Console.Write("Desea agregar otro producto al carrito? \nsi/no: \n");
                                    op = Console.ReadLine();
                                }
                            }
                            _ordenSevices.AddOrden(carrito.CarritoId, total);
                            carrito.Estado = false;
                            _context.SaveChanges();
                            Console.WriteLine("Se ha realizado la compra exitosamente!");
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
