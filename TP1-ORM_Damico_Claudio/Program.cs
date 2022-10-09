
using TP1_ORM_AccessData.Entities;
using TP1_ORM_Core.Services;

namespace TP1_ORM_Presentation
{
    class Program
    {
        static void Main(string[] args)
        {
            Menu();
        }

        static void Menu()
        {
            string op;
            do
            {
                Console.Clear();
                Console.WriteLine("*******************************************************************\n" +
                                  "****                          Tiendita                         ****\n" +
                                  "*******************************************************************\n");
                Console.WriteLine("Bienvenido a nuestra tiendita. Por favor, seleccione una opción del menú: \n\n");
                Console.WriteLine("1) Registrar cliente\n" +
                                  "2) Registrar una venta\n" +
                                  "3) Ver Ventas del dia\n" +
                                  "4) Buscar producto en ventas\n" +
                                  "5) Salir del programa.");

                op = Console.ReadLine();
                switch (op)
                {
                    case "1":
                        Console.Clear();
                        RegistroDeCliente();
                        break;

                    case "2":
                        Console.Clear();
                        RegistrarVenta();

                        break;

                    case "3":
                        Console.Clear();
                        VerListaDeVentas();
                        break;

                    case "4":
                        Console.Clear();
                        BuscarProducto();
                        break;

                    case "5":
                        Console.Clear();
                        Console.WriteLine("Saliendo del programa");
                        break;

                    default:
                        Console.Clear();
                        Console.WriteLine("La opción ingresada no es correcta, por favor inténtelo nuevamente.");
                        Console.WriteLine("Presione enter para continuar.");
                        Console.ReadLine();
                        break;
                }
            } while (op != "6");
        }

        static void RegistroDeCliente()
        {
            var service = new ClientesServices();
            Console.WriteLine("Registrece en el sistema");
            service.RegistraCliente();
            Console.WriteLine("\n");
            Console.WriteLine("Presione enter para continuar y volver al menú principal.");
            Console.ReadKey();
        }

        static void RegistrarVenta()
        {
            var service = new VentasServices();
            service.RegistrarVenta();
            Console.WriteLine("\n");
            Console.WriteLine("Presione enter para continuar y volver al menú principal.");
            Console.ReadKey();
        }

        static void VerListaDeVentas()
        {
            var service = new VentasServices();
            Console.WriteLine("Listado de ventas del dia\n");
            service.ListarVentas();
            Console.WriteLine("\n");
            Console.WriteLine("Presione enter para continuar y volver al menú principal.");
            Console.ReadKey();
        }

        static void BuscarProducto()
        {
            var service = new VentasServices();
            Console.WriteLine("Buscar productos vendidos\n");
            service.ListarProductosEnVentas();
            Console.WriteLine("\n");
            Console.WriteLine("Presione enter para continuar y volver al menú principal.");
            Console.ReadKey();
        }                 
    }
}

