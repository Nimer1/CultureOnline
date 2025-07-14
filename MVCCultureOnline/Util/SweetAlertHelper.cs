namespace MVCCultureOnline.Util
{
    public class SweetAlertHelper
    {
        public static string Mensaje(string titulo, string mensaje, SweetAlertMessageType tipoMensaje)
        {
            return $"Swal.fire({{ title: '{titulo}', text: '{mensaje}', icon: '{tipoMensaje.ToString().ToLower()}' }});";
        }

    }
    public enum SweetAlertMessageType
    {
        success, error, warning, info, question
    }
}
