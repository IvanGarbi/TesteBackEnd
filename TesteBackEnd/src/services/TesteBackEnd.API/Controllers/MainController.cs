using Microsoft.AspNetCore.Mvc;
using TesteBackEnd.Business.Interfaces.Notifications;

namespace TesteBackEnd.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MainController : ControllerBase
    {
        protected readonly INotificador _notificador;

        public MainController(INotificador notificador)
        {
            _notificador = notificador;
        }

        protected ActionResult Respose(object result = null)
        {
            if (!_notificador.TemNotificacao())
            {
                return Ok(new
                {
                    sucesso = true,
                    data = result
                });
            }

            return BadRequest(new
            {
                sucesso = false,
                errors = _notificador.GetNotificacoes().Select(n => n.Mensagem)
            });
        }
    }
}
