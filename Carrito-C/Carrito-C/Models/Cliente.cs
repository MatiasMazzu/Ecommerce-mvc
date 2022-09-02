namespace Carrito_C.Models
{
    public class Cliente : Persona
    {
        public List<Compra> Compras { get; set; }
        public List<Carrito> Carritos { get; set; }
    }
}
