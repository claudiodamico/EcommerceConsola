
using TP1_ORM_AccessData.Data;
using TP1_ORM_AccessData.Entities;

namespace TP1_ORM_Core.Services
{
    public class ClientesServices
    {
        //Traemos cliente por dni
        public Cliente GetCliente(string dni)
        {
            using (var _context = new TiendaDbContext())
            {
                return _context.Clientes.FirstOrDefault(x => x.Dni == dni);
            }
        }
        //Traemos cliente por dni
        public Cliente GetCliente(int dni)
        {
            return GetCliente(dni.ToString());
        }
        //Registro de cliente
        public void Registrar(Cliente cliente)
        {
            using (var _context = new TiendaDbContext())
            {
                _context.Clientes.Add(cliente);
                _context.SaveChanges();
            }
        }
        //Registramos los clientes
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
                //Validamos si el cliente esta registrado
                if (cliente != null)
                {
                    Console.WriteLine("El cliente ya esta registrado" + "\n");
                    return;
                }
                //Validamos si los datos no estan vacios
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

                    Console.WriteLine("El cliente se registro exitosamente!" + "\n");
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

