namespace Carrito_C.Helpers
{
    public class Validaciones
    {
        public const int NombreMaxString = 50;
        public const int NombreMinString = 2;
        public const int CategoriaMaxString = 50;
        public const int CategoriaMinString = 2;
        public const int DniMin = 100000;
        public const int DniMax = 99999999;
        public const int NumeracionMin = 1;
        public const int NumeracionMax = 25000;
        public const int ApellidoMaxString = 100;
        public const int ApellidoMinString = 2;
        public const int DireccionMaxString = 100;
        public const int DireccionMinString = 5; 
        public const int DescripcionMaxString = 100;
        public const int DescripcionMinString = 5;
        public const int UserNameMaxString = 25;
        public const int UserNameMinString = 5;
        public const int PasswordMaxString = 25;
        public const int PasswordMinString = 8;
        public const int CantidadMaxInt = 100;
        public const int CantidadMinInt = 0;
        public const string RegexTelefono = "^(?!0+$)(\\+\\d{1,3}[- ]?)?(?!0+$)\\d{10,15}$";
    }
}
