using AutoMapper;
using FIAP.TECH.CORE.APPLICATION.DTO;
using FIAP.TECH.CORE.APPLICATION.Services.Contacts;
using FIAP.TECH.CORE.DOMAIN.Models;
using Microsoft.AspNetCore.Mvc;

namespace FIAP.TECH.API.CONSULT.Controllers
{
    [ApiController]
    //[Authorize]
    [Route("[controller]")]
    public class ConsultController : ControllerBase
    {
        private readonly IContactService _contactService;
        private readonly IMapper _mapper;

        public ConsultController(IContactService contactService, IMapper mapper)
        {
            _contactService = contactService;
            _mapper = mapper;
        }

        /// <summary>
        /// Método para listar todos os Contatos cadastrados
        /// </summary>
        /// <returns>Lista de contatos</returns>
        /// <response code="200">Retorna a lista de objetos Contact</response>
        /// <response code="400">A requisição foi mal formada</response>
        /// <response code="401">Usuário não enviou o token de acesso ou o token está expirado</response>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            Task.Delay(5000).Wait();
            return Ok(await _contactService.GetAll());
        }

        /// <summary>
        /// Método para listar todos os Contatos cadastrados com o DDD informado
        /// </summary>
        /// <param name="ddd">DDD da região em que os contatos estão cadastrados</param>
        /// <returns>Lista de contatos e suas determinadas regiões</returns>
        /// <response code="200">Retorna a lista de objetos Contact com Region</response>
        /// <response code="400">A requisição foi mal formada</response>
        /// <response code="401">Usuário não enviou o token de acesso ou o token está expirado</response>
        [HttpGet("get-by-region/{ddd}")]
        public async Task<IActionResult> GetByDdd([FromRoute] string ddd)
        {
            try
            {
                var response = await _contactService.SendResponseMessageAsync(new ContactByDDD(ddd));
                if (!response.Success)
                {
                    return BadRequest(response.Message);
                }

                return Ok(_mapper.Map<IEnumerable<ContactDto>>(response.Contacts));

            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
        }
    }
}
