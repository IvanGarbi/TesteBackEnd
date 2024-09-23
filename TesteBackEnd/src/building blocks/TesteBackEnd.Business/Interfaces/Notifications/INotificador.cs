using TesteBackEnd.Business.Notifications;

namespace TesteBackEnd.Business.Interfaces.Notifications
{
    public interface INotificador
    {
        bool TemNotificacao();
        List<Notificacao> GetNotificacoes();
        void AdicionarNotificacao(Notificacao notificacao);
    }
}
