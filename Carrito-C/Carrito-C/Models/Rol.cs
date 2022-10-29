using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Carrito_C.Models
{
    public class Rol : IdentityRole<int>
    {
        public Rol() : base()
        {

        }

        public Rol(String name) : base(name)
        {

        }
        //public int id { get; set; }

        //[Display(Name = Alias.RoleName)]
        public override String Name
        {
            get { return base.Name; }
            set { base.Name = value; }
        }

        public override string NormalizedName
        {
            get => base.NormalizedName;
            set => base.NormalizedName = value;
        }
    }
}
