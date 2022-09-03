using System.ComponentModel.DataAnnotations;
using Carrito_C.Helpers;

namespace Carrito_C.Models
{
    public class Producto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = MsgError.Requerido)]
        [StringLength (100, MinimumLength = 3, ErrorMessage = MsgError.CommonError)]
        public string Nombre { get; set; }

        [Required(ErrorMessage = MsgError.Requerido)]
        [StringLength(100, MinimumLength = 3, ErrorMessage = MsgError.CommonError)]
        public string Descripcion { get; set; }

        [Required(ErrorMessage = MsgError.Requerido)]
        [Range(0, 10000, ErrorMessage = MsgError.CommonError)]
        public int PrecioVigente { get; set; }
        [Required(ErrorMessage = MsgError.Requerido)]
        public bool Activo { get; set; }
        public string Categoria { get; set; }
    }
}
