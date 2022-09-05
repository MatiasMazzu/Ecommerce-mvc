namespace Carrito_C.Models
{
    public class CarritoItem
    {

        public int CarritoItemId { get; set; }

        public Carrito carrito { get; set; }

        public Producto producto { get; set; }

        public int valorUnitario { get; set; }

        public int cantidad { get; set; }

        public int subtotal { get; set; }

    }        
 
}
