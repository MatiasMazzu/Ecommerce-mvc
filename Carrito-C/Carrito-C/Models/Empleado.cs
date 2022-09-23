namespace Carrito_C.Models
{
    public class Empleado : Persona
    {
        public Guid Legajo { set; get; } = Guid.NewGuid();
    }
}
