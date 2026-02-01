namespace PgExtension.GUI.Models
{
    public class MessageEventArgs(string title, string message) : EventArgs
    {
        public string Title => title;
        public string Message => message;
    }
}
