namespace APICatalogo.Notifications;

public class Notification
{
    public string Key { get; } // Campo que gerou o erro (ex: "Nome")
    public string Message { get; } // Mensagem de erro (ex: "O nome é obrigatório")

    public Notification(string key, string message)
    {
        Key = key;
        Message = message;
    }
}
