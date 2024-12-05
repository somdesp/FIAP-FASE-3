using AutoMapper;
using FIAP.TECH.CORE.APPLICATION.DTO;
using FIAP.TECH.CORE.APPLICATION.Services.Contacts;
using FIAP.TECH.CORE.DOMAIN.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FIAP.TECH.API.CREATION.Controllers
{
    [ApiController]
    [Authorize]
    [Route("[controller]/Create")]
    public class CreationController : ControllerBase
    {
        private readonly IContactService _contactService;
        private readonly IMapper _mapper;

        public CreationController(IMapper mapper,
            IContactService contactService)
        {
            _contactService = contactService;
            _mapper = mapper;
        }

        /// <summary>
        /// Método para incluir um novo Contato
        /// </summary>
        /// <param name="contactDTO">Objeto Contact contendo as informações do contato que deseja cadastrar</param>
        /// <returns>Mensagem de sucesso ou falha ao tentar criar um novo Contato</returns>
        /// <response code="200">Mensagem de contato criado com sucesso</response>
        /// <response code="400">A requisição foi mal formada</response>
        /// <response code="401">Usuário não enviou o token de acesso ou o token está expirado</response>
        /// <remarks>
        /// Exemplo:
        /// {
        /// "name": "Contact test",
        /// "email": "test@testmail.com",
        /// "phoneNumber": "999999999",
        /// "ddd": "11"
        ///}
        /// </remarks>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ContactDto contactDTO)
        {
            try
            {
                var contact = _mapper.Map<Contact>(contactDTO);
                await _contactService.SendMessageAsync(contact);
                return Accepted();

            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
        }
    }
}
