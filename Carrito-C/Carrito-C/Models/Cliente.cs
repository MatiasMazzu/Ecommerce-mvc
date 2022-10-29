namespace Carrito_C.Models
{
    public class Cliente : Persona
    {
        public long CUIT { get; set; } 
        public List<Compra> Compras { get; set; }
        public List<Carrito> Carritos { get; set; }
    }
}
