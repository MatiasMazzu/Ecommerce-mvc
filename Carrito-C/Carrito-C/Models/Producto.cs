using System.ComponentModel.DataAnnotations;
using Carrito_C.Helpers;

namespace Carrito_C.Models
{
    public class Producto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = MsgError.Requerido)]
        [StringLength(Validaciones.NombreMaxString, MinimumLength = Validaciones.NombreMinString, 
            ErrorMessage = MsgError.CommonError)]
        public string Nombre { get; set; }

        [Required(ErrorMessage = MsgError.Requerido)]
        [StringLength(Validaciones.DescripcionMaxString, MinimumLength = Validaciones.DescripcionMinString, 
            ErrorMessage = MsgError.CommonError)]
        public string Descripcion { get; set; }

        [Required(ErrorMessage = MsgError.Requerido)]
        [Range(double.MinValue, short.MaxValue, ErrorMessage = MsgError.ValorNegativo)]
        [DataType(DataType.Currency)]
        [Display(Name = Alias.PrecioVigente)]
        public int PrecioVigente { get; set; }


        [Required(ErrorMessage = MsgError.Requerido)]
        public bool Activo { get; set; }

        [Required(ErrorMessage = MsgError.Requerido)]
        [Display(Name = Alias.CategoriaId)]
        public int CategoriaId { get; set; }
        public Categoria Categoria { get; set; }

        public List<StockItem> ProductoSucursales { get; set; }

        [Display(Name = Alias.ImagenArchivo)]
        public string ImagenArchivo { get; set; }
    }
}
