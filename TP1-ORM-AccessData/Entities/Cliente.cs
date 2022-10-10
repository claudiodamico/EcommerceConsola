
using System.ComponentModel.DataAnnotations;

namespace TP1_ORM_AccessData.Entities
{
    public class Cliente
    {
        public Cliente()
        {
            Carritos = new HashSet<Carrito>();
        }
        public int ClienteId { get; set; }
        public string Dni { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; } 
        public virtual ICollection<Carrito> Carritos { get; set; }
    }
}
