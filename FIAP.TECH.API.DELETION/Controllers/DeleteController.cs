using AutoMapper;
using FIAP.TECH.CORE.APPLICATION.Services.Contacts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FIAP.TECH.API.DELETION.Controllers
{

    [ApiController]
    [Authorize]
    [Route("[controller]")]
    public class DeleteController : ControllerBase
    {
        private readonly IContactService _contactService;
        private readonly IMapper _mapper;

        public DeleteController(IContactService contactService, IMapper mapper)
        {
            _contactService = contactService;
            _mapper = mapper;
        }

        /// <summary>
        /// Método para excluir um Contato cadastrado
        /// </summary>
        /// <param name="id">ID do contato que deseja excluir</param>
        /// <returns>Mensagem de sucesso ou falha ao tentar excluir um Contato</returns>
        /// <response code="200">Mensagem de contato criado com sucesso</response>
        /// <response code="400">A requisição foi mal formada</response>
        /// <response code="401">Usuário não enviou o token de acesso ou o token está expirado</response>
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            try
            {
                await _contactService.Delete(id);
                return Ok(new { message = "Contato removido com sucesso." });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }
    }
}
