using FIAP.TECH.API.CREATION.DTO;
using FIAP.TECH.API.CREATION.Validation;
using FIAP.TECH.CORE.DomainObjects;
using FluentValidation;
using MassTransit;
using Microsoft.AspNetCore.Mvc;

namespace FIAP.TECH.API.CREATION.Controllers
{
    [ApiController]
    //[Authorize]
    [Route("[controller]/Create")]
    public class CreationController : ControllerBase
    {
        private readonly IBus _bus;

        public CreationController(IBus bus)
        {
            _bus = bus;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ContactDto contactDTO)
        {
            try
            {
                var contact = new Contact { Email = contactDTO.Email, DDD = contactDTO.DDD, Name = contactDTO.Name, PhoneNumber = contactDTO.PhoneNumber };

                //Valida se os dados estão corretos
                var resultValidation = new ContactInsertValidation();
                FluentValidation.Results.ValidationResult results = await resultValidation.ValidateAsync(contact);

                if (results.Errors.Count != 0)
                    throw new ValidationException(results.Errors);

                await _bus.Publish(contact);

                return Ok(new { message = "Contato criado com sucesso." });
            }
            catch (ValidationException vex)
            {
                return BadRequest(new { errors = vex.Errors.Select(e => e.ErrorMessage) });
            }
        }
    }
}
