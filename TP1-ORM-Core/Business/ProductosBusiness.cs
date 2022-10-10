
using TP1_ORM_Core.Services;

namespace TP1_ORM_Core.Business
{
    public class ProductosBusiness
    {
        private readonly ProductosServices _services;

        public ProductosBusiness(ProductosServices services)
        {
            _services = services;
        }

        public void ListarProductos()
        {
            _services.ListarProductos();
        }

        public void ProductoByCodigo(string codigo)
        {
            _services.ProductoByCodigo(codigo);
        }
    }
}
