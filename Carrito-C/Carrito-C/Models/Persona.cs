﻿using Carrito_C.Helpers;
using System.ComponentModel.DataAnnotations;

namespace Carrito_C.Models
{
    public class Persona
    {
        public int Id { get; set; }
        [StringLength(50, MinimumLength = 2, ErrorMessage = MsgError.CommonError)]
        public string Nombre { get; set; }
        [StringLength(100, MinimumLength = 2, ErrorMessage = MsgError.CommonError)]
        public string Apellido { get; set; }
        [Required(ErrorMessage = MsgError.Requerido), Range(100000, 99999999, ErrorMessage = MsgError.CommonError2)]
        public int Telefono { get; set; }
        [Required(ErrorMessage = MsgError.Requerido), StringLength(50, MinimumLength = 2, ErrorMessage = MsgError.CommonError)]
        public string Direccion { get; set; }
        [Required(ErrorMessage = MsgError.Requerido)]
        [EmailAddress(ErrorMessage = MsgError.MsgEmail)]
        [Display(Name = "Correo electronico")]
        public string Email { get; set; }
        public DateTime FechaAlta { get; set; }
    }
}