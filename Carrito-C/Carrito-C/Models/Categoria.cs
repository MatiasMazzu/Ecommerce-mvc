﻿using Carrito_C.Helpers;
using System.ComponentModel.DataAnnotations;

namespace Carrito_C.Models
{
    public class Categoria
    {
        public int Id { get; set; }
        [Required(ErrorMessage = MsgError.Requerido), StringLength(50, MinimumLength = 2, ErrorMessage = MsgError.CommonError)]
        public string Nombre { get; set; }
        [Required(ErrorMessage = MsgError.Requerido), StringLength(50, MinimumLength = 2, ErrorMessage = MsgError.CommonError)]
        public string Descripcion { get; set; }
        List<Producto> Productos;
    }

}
