
using TP1_ORM_Core.Services;

namespace TP1_ORM_Core.Business
{
    public  class VentasBusiness
    {
        private readonly VentasServices _services;

        public VentasBusiness(VentasServices services)
        {
            _services = new VentasServices();
        }

        public void RegistrarVenta()
        {
            _services.RegistrarVenta();
        }

        public void ListaDeVentas()
        {
            _services.ListaDeVentas();
        }

        public void ListaDeProductosVendidos()
        {
            _services.ListaDeProductosVendidos();
        }
    }
}
