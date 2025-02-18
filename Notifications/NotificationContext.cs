namespace APICatalogo.Notifications;

public class NotificationContext
{
    private readonly List<Notification> _notifications = new List<Notification>();

    // Lista de notificações (erros)
    public IReadOnlyCollection<Notification> Notifications => _notifications.AsReadOnly();

    // Adiciona uma notificação (erro)
    public void AddNotification(string key, string message)
    {
        _notifications.Add(new Notification(key, message));
    }

    // Verifica se há notificações (erros)
    public bool HasNotifications => _notifications.Any();
}
