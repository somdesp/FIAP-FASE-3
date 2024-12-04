using FIAP.TECH.API.CREATION.DTO;
using FIAP.TECH.API.CREATION.Services;
using FIAP.TECH.CORE.DomainObjects;
using Microsoft.AspNetCore.Mvc;

namespace FIAP.TECH.API.CREATION.Controllers
{
    [ApiController]
    //[Authorize]
    [Route("[controller]/Create")]
    public class CreationController : ControllerBase
    {
        private readonly ICreationService _creationService;

        public CreationController(ICreationService creationService)
        {
            _creationService = creationService;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ContactDto contactDTO)
        {
            try
            {
                var contact = new Contact { Email = contactDTO.Email, DDD = contactDTO.DDD, Name = contactDTO.Name, PhoneNumber = contactDTO.PhoneNumber };

                var response = await _creationService.SendMessageAsync(contact);


                if (!response.Success)
                {
                    return BadRequest(response.Message);
                }

                return Ok(response);

            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
        }
    }
}
