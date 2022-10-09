
using TP1_ORM_AccessData.Data;
using TP1_ORM_AccessData.Entities;

namespace TP1_ORM_Core.Services
{
    public class ClientesServices
    {
        public Cliente GetCliente(string dni)
        {
            using (var _context = new TiendaDbContext())
            {
                return _context.Clientes.FirstOrDefault(x => x.Dni == dni);
            }
        }
        public Cliente GetCliente(int dni)
        {
            return GetCliente(dni.ToString());
        }
        //
        public void Registrar(Cliente cliente)
        {
            using (var _context = new TiendaDbContext())
            {
                _context.Clientes.Add(cliente);
                _context.SaveChanges();
            }
        }
        public void RegistraCliente()
        {
            try
            {
                Console.WriteLine("Ingrese su Nombre: ");
                string nombre = Console.ReadLine();
             
                Console.WriteLine("Ingrese su Apellido: ");
                string apellido = Console.ReadLine();

                Console.WriteLine("Ingrese su Direccion: ");
                string direccion = Console.ReadLine(); 

                Console.WriteLine("Ingrese su Telefono: ");
                string telefono = Console.ReadLine();

                Console.WriteLine("Ingrese su Dni: ");
                string dni = Console.ReadLine();
                var cliente = GetCliente(dni);
               
                if (cliente != null)
                {
                    Console.WriteLine("El cliente ya esta registrado" + "\n" +
                                       "Presione enter para continuar y volver al menú principal.");
                    Console.ReadKey();
                    return;
                }
                
                if (nombre != "" && apellido != "" && direccion != "" && telefono != "")
                {
                    Registrar(new Cliente
                    {
                        Nombre = nombre,
                        Apellido = apellido,
                        Dni = dni,
                        Direccion = direccion,
                        Telefono = telefono

                    });

                    Console.WriteLine("El cliente se registro exitosamente!" + "\n" +
                                      "Presione enter para continuar y volver al menú principal.");
                    Console.ReadKey();
                }
                else
                {
                    Console.WriteLine("Debe ingresar los datos solicitados, intente nuevamente por favor: ");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Datos incompletos para el registro, intente de nuevo por favor");
            }

        }

    }
}

