namespace Carrito_C.Helpers
{
    public class MsgError
    {
        public const string MsgMaxStr = "{0} debe ser menor que {1} caracteres";
        public const string MsgMinStr = "{0} debe ser mayor que {1} caracteres";
        public const string Requerido = "{0} es requerido";
        public const string MsgEmail = "Por favor, ingresar un {0} válido";
        public const string CommonError = "{0} debe estar comprendido entre {2} y {1} caracteres";
        public const string CommonError2 = "{0} debe estar comprendido entre {1} y {2} caracteres";
        public const string UserName = "El nombre de usuario debe estar comprendido entre {2} y {1} caracteres";
        public const string Password = CommonError;
        public const string Telefono = "El teléfono ingresado no es válido";
        public const string ValorNegativo = "El valor ingresado {0} debe ser siempre mayor a 0";
        public const string PassMissMatch = "El campo {0} no coincide";
        public const string DNIExistente = "DNI Existente";
        public const string EmailExistente = "Email Existente";
        public const string CUITExistente = "CUIT Existente";
        public const string NombreExistente = "Nombre Existente";

    }
}
