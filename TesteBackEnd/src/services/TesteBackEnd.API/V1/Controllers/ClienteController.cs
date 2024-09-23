using Asp.Versioning;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TesteBackEnd.API.Controllers;
using TesteBackEnd.API.ViewModels;
using TesteBackEnd.Business.Interfaces.Notifications;
using TesteBackEnd.Business.Interfaces.Repository;
using TesteBackEnd.Business.Interfaces.Services;
using TesteBackEnd.Business.Notifications;
using TesteBackEnd.Core.Models;

namespace TesteBackEnd.API.V1.Controllers
{
    [Route("v{version:apiVersion}/Cliente/[controller]")]
    [ApiVersion("1.0")]
    public class ClienteController : MainController
    {
        private readonly IClienteRepository _clienteRepository;
        private readonly IClienteService _clienteService;
        private readonly IMapper _mapper;

        public ClienteController(IClienteRepository articleRepository, IMapper mapper, IClienteService articleService, INotificador notificador) : base(notificador)
        {
            _clienteRepository = articleRepository;
            _mapper = mapper;
            _clienteService = articleService;
        }

        [HttpGet]
        public async Task<IEnumerable<GetClienteViewModel>> Get()
        {
            var articles = await _clienteRepository.Read();
            var articlesViewModels = _mapper.Map<IEnumerable<GetClienteViewModel>>(articles);
            return articlesViewModels;
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<GetClienteViewModel>> Get(Guid id)
        {
            var article = await _clienteRepository.ReadById(id);

            if (article == null)
            {
                _notificador.AdicionarNotificacao(new Notificacao("Esse Cliente não existe."));

                return Respose();
            }

            return _mapper.Map<GetClienteViewModel>(article);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] PostClienteViewModel articleViewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { ModelState });
            }

            var article = _mapper.Map<Cliente>(articleViewModel);

            await _clienteService.Create(article);

            return Respose();
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Put(Guid id, [FromBody] PostClienteViewModel articleViewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { ModelState });
            }

            var article = _mapper.Map<Cliente>(articleViewModel);

            article.Id = id;

            await _clienteService.Update(article);

            return Respose();
        }

        [HttpPatch("{id:guid}")]
        public async Task<IActionResult> Patch(Guid id, [FromBody] PostClienteViewModel articleViewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { ModelState });
            }

            var dbCliente = await _clienteRepository.ReadById(id);

            if (dbCliente == null)
            {
                return BadRequest();
            }

            var article = _mapper.Map<Cliente>(articleViewModel);

            article.Id = id;

            await _clienteService.Update(article);

            return Respose();
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { ModelState });
            }

            await _clienteService.Delete(id);

            return Respose();
        }
    }
}
