namespace Carrito_C.Models
{
    public class Carrito
    {
        public int CarritoId { get; set; }
        public Boolean activo { get; set; }
        public Cliente cliente { get; set; }
        public List <CarritoItem> CarritoItems{ get; set; }

        public List<Producto> Productos { get; set; }
        public int subtotal { get; set; }
    }
}
