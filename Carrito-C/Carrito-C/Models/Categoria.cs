using Carrito_C.Helpers;
using System.ComponentModel.DataAnnotations;

namespace Carrito_C.Models
{
    public class Categoria
    {
        public int Id { get; set; }

        [Required(ErrorMessage = MsgError.Requerido)]
        [StringLength(Validaciones.CategoriaMaxString, MinimumLength = Validaciones.CategoriaMinString,
            ErrorMessage = MsgError.CommonError)]
        public string Nombre { get; set; }

        [Required(ErrorMessage = MsgError.Requerido)]
        [ StringLength(Validaciones.DescripcionMaxString, MinimumLength = Validaciones.DireccionMinString,
            ErrorMessage = MsgError.CommonError)]
        public string Descripcion { get; set; }
        public List<Producto> Productos { get; set; }
    }

}
