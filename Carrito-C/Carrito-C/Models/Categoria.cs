using Microsoft.AspNetCore.Authentication;

namespace Carrito_C.Models
{
    public class Categoria
    {
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        List<Producto> Productos;
    }

}
