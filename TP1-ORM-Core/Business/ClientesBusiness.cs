
using TP1_ORM_Core.Services;

namespace TP1_ORM_Core.Business
{
    public class ClientesBusiness
    {
        private readonly ClientesServices _services;

        public ClientesBusiness(ClientesServices services)
        {
            _services = services;
        }
        public void RegistrarCliente()
        {
            _services.RegistraCliente();
        }
    }
}
