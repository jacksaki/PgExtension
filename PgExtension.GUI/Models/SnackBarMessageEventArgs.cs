namespace PgExtension.GUI.Models
{
    public class SnackBarMessageEventArgs(string message) : EventArgs
    {
        public string Message => message;
    }
}
