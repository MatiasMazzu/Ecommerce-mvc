namespace Carrito_C.Models
{
    public class Compra
    {

        public int Id { get; set; }

        public Cliente cliente { get; set; }
        public Carrito carrito { get; set; }
        public int total { get; set; }
    }
}
