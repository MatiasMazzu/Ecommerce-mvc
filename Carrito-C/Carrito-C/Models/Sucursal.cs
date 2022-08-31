namespace Carrito_C.Models
{
    public class Sucursal
    {
        public string Nombre { get; set; }
        public string Direccion { get; set; }
        public int Telefono { get; set; }
        public string Email {  get; set; }
        List<StockItem> StockItems { get; set; }

    }
}
