using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
    }
}
