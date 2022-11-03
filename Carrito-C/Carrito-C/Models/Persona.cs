﻿using Carrito_C.Helpers;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Carrito_C.Models
{
    public class Persona : IdentityUser<int>
    {
        //public int Id { get; set; }

        [Required(ErrorMessage = MsgError.Requerido)]
        [StringLength(Validaciones.NombreMaxString, MinimumLength = Validaciones.NombreMinString,
            ErrorMessage = MsgError.CommonError)]
        public string Nombre { get; set; }

        [Required(ErrorMessage = MsgError.Requerido)]
        [StringLength(Validaciones.ApellidoMaxString, MinimumLength = Validaciones.ApellidoMinString,
        ErrorMessage = MsgError.CommonError)]
        public string Apellido { get; set; }

        [Required(ErrorMessage = MsgError.Requerido)]
        [Range(Validaciones.DniMin, Validaciones.DniMax, ErrorMessage = MsgError.CommonError)]
        [Display(Name = Alias.Dni)]
        public int Dni { get; set; }

        public Telefono Telefono{ get; set; }

        public Direccion Direccion { get; set; }

        [Required(ErrorMessage = MsgError.Requerido)]
        [EmailAddress(ErrorMessage = MsgError.MsgEmail)]
        [Display(Name = Alias.Mail)]
        public override string Email
        {
            get { return base.Email; }
            set { base.Email = value; }
        }

        [Required(ErrorMessage = MsgError.Requerido)]
        [DataType(DataType.Date)]
        [Display(Name = Alias.FechaAlta)]
        public DateTime FechaAlta { get; set; } = DateTime.Now;

        [Required(ErrorMessage = MsgError.Requerido)]
        [StringLength(Validaciones.UserNameMaxString, MinimumLength = Validaciones.UserNameMinString, ErrorMessage = MsgError.UserName)]
        [Display(Name = Alias.UserName)]
        public override string UserName
        {
            get { return base.UserName; }
            set { base.UserName = value; }
        }

        [Required(ErrorMessage = MsgError.Requerido)]
        [StringLength(Validaciones.PasswordMaxString, MinimumLength = Validaciones.PasswordMinString, ErrorMessage = MsgError.Password)]
        [Display(Name = Alias.Password)]
        public override string PasswordHash
        {
            get { return base.PasswordHash; }
            set { base.PasswordHash = value; }
        }
    }
}
