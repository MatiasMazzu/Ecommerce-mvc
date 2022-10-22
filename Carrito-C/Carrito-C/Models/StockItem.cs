﻿using Carrito_C.Helpers;
using System.ComponentModel.DataAnnotations;

namespace Carrito_C.Models
{
    public class StockItem
    {

        [Required(ErrorMessage = MsgError.Requerido)]
        [Key]
        [Display(Name = Alias.ProductoId)]
        
        public int ProductoId { get; set; } 
        public Producto Producto { get; set; }

        [Required(ErrorMessage = MsgError.Requerido)]
        [Key]
        [Display(Name = Alias.SucursalId)]
        
        public int SucursalId { get; set; } 
        public Sucursal Sucursal { get; set; }

        [Required(ErrorMessage = MsgError.Requerido)]
        [Range(Validaciones.CantidadMinInt, Validaciones.CantidadMaxInt, ErrorMessage = MsgError.CommonError2)]
        public int Cantidad { get; set; }


    }
}
