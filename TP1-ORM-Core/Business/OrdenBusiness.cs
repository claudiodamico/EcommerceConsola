
using TP1_ORM_Core.Services;

namespace TP1_ORM_Core.Business
{
    
    public class OrdenBusiness
    {
        private readonly OrdenSevices _services;

        public OrdenBusiness(OrdenSevices services)
        {
            _services = services;
        }
        public void AddOrden(Guid Id, decimal total)
        {
            _services.AddOrden(Id, total);
        }
    }
}
