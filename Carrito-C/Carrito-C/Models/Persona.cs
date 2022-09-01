namespace Carrito_C.Models
{
    public class Persona
    {
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public int Telefono { get; set; }
        public string Direccion { get; set; }
        public string Email { get; set; }
        public DateOnly FechaAlta { get; set; }
    }
}
