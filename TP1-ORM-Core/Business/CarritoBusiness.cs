
using TP1_ORM_Core.Services;

namespace TP1_ORM_Core.Business
{
    public class CarritoBusiness
    {
        private readonly CarritoServices _services;

        public CarritoBusiness(CarritoServices services)
        {
            _services = services;
        }

        public void GetCarritoId(Guid Id)
        {
            _services.GetCarritoId(Id);
        }

        public void ModifyCarrito(Guid Id, string dni)
        {
            _services.ModifyCarrito(Id, dni);
        }
    }
}
