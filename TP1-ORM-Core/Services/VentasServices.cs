
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
        //Lista de ventas del dia
        public void ListaDeVentas()
        {
            _queries.ListarVentas();
        }
        //Lista de productos vendidos
        public void ListaDeProductosVendidos()
        {
            _queries.ListarProductosEnVentas();
        }
        //Registro de ventas
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
                    //Validamos que el cliente este registrado para seguir con la venta
                    if (_validate.ValidarCliente(dni) == null)
                    {
                        Console.WriteLine("No esta registrado en el sistema\n");
                        _clientesServices.RegistraCliente();
                    };

                    Console.WriteLine("Bienvenido a nuestra tienda, elija el producto que desea comprar\n");

                    var op = "si";
                    //Mientras el cliente ingrese si, seguira agregando productos
                    while (op == "si")
                    {
                        _productosServices.ListarProductos();

                        Console.Write("Seleccione el codigo del producto que desea comprar: ");
                        string codigo = Console.ReadLine();

                        var producto = _productosServices.ProductoByCodigo(codigo);
                        //Validamos que el producto no venga nulo segun validate
                        while (_validate.ValidarProducto(codigo) == null)
                        {
                            Console.WriteLine("El codigo no existe en el catalaogo de productos.\n" +
                                          "Por favor vuelva a intentar con otro codigo valido.\n");

                            codigo = Console.ReadLine();
                            producto = _productosServices.ProductoByCodigo(codigo);
                        }
                        //Validamos que ingresen unidades
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
                        }
                        else
                        {
                            Console.WriteLine("ocurrio un error!");
                        }
                    }

                    _ordenSevices.AddOrden(carrito.CarritoId, total);
                    Console.WriteLine("Generando orden de compra...\n");
                    carrito.Estado = false;
                    _carritoServices.ModifyCarrito(carrito.CarritoId, dni);
                    _context.SaveChanges();
                    Console.WriteLine("Se ha realizado la compra exitosamente!");
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
