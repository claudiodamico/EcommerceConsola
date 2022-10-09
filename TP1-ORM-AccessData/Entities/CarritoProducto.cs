
namespace TP1_ORM_AccessData.Entities
{
    public class CarritoProducto
    {
        public Guid? CarritoId { get; set; }
        public int ProductoId { get; set; }
        public int? Cantidad { get; set; }
        public virtual Carrito Carrito { get; set; } 
        public virtual Producto Producto { get; set; } 
    }
}
