namespace Carrito_C.Models
{
    public class Compra
    {

        public int Id { get; set; }

        public Cliente Cliente { get; set; }
        public Carrito Carrito { get; set; }
        public int Total { get; set; }
    }
}
