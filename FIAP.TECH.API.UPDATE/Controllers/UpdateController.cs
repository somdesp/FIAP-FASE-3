using AutoMapper;
using FIAP.TECH.CORE.APPLICATION.DTO;
using FIAP.TECH.CORE.APPLICATION.Services.Contacts;
using FIAP.TECH.CORE.DOMAIN.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FIAP.TECH.API.UPDATE.Controllers
{

    [ApiController]
    [Authorize]
    [Route("[controller]")]
    public class UpdateController : ControllerBase
    {
        private readonly IContactService _contactService;
        private readonly IMapper _mapper;

        public UpdateController(IContactService contactService, IMapper mapper)
        {
            _contactService = contactService;
            _mapper = mapper;
        }

        /// <summary>
        /// Método para alterar um Contato existente
        /// </summary>
        /// <param name="id">ID do contato que deseja alterar</param>
        /// <param name="contactDTO">Objeto Contact contendo as informações do contato que deseja alterar</param>
        /// <returns>Mensagem de sucesso ou falha ao tentar alterar um Contato</returns>
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
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Put([FromRoute] int id, [FromBody] ContactUpdateDto contactDTO)
        {
            try
            {
                contactDTO.Id = id;
                var contact = _mapper.Map<Contact>(contactDTO);

                await _contactService.SendMessageAsync(contact);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
        }
    }
}
