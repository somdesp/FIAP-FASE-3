using FIAP.TECH.CORE.DomainObjects;
using FIAP.TECH.CORE.DTOs;
using FIAP.TECH.INFRASTRUCTURE.Contexts;
using FluentValidation;
using MassTransit;
using Microsoft.EntityFrameworkCore;

namespace FIAP.TECH.WORK.Events
{
    public class QueueCreateContactConsumer : IConsumer<Contact>
    {
        private readonly ILogger<QueueCreateContactConsumer> _logger;
        private readonly IValidator<Contact> _validator;


        private readonly AppDbContext _context;

        public QueueCreateContactConsumer(IValidator<Contact> validator, ILogger<QueueCreateContactConsumer> logger, AppDbContext context)
        {
            _logger = logger;
            _context = context;
            _validator = validator;
        }


        public async Task Consume(ConsumeContext<Contact> context)
        {
            var message = context.Message;

            try
            {
                var validationResult = await _validator.ValidateAsync(context.Message);

                if (!validationResult.IsValid)
                {
                    // Retornar erros de validação
                    await context.RespondAsync(new ContactResponse(false, validationResult.Errors.Select(e => e.ErrorMessage).ToList()));
                    foreach (var item in validationResult.Errors.Select(e => e.ErrorMessage))
                    {
                        _logger.LogInformation(item);
                    }

                    return;
                }

                //valida regiao
                var region = await _context.Regions.FirstOrDefaultAsync(x => x.DDD == message.DDD);
                if (region == null)
                {
                    // Retornar erros de validação
                    _logger.LogInformation("DDD inexistente na base de dados.");
                    await context.RespondAsync(new ContactResponse(false, ["DDD inexistente na base de dados."]));
                    return;
                }

                message.RegionId = region.Id;

                await _context.Contacts.AddAsync(message);
                await _context.SaveChangesAsync();

                // Simulate success
                await Task.Delay(1000);

                await context.RespondAsync(new ContactResponse(true, ["Contato Inserido com Sucesso"]));
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex, "Error processing Contact updated event for Contact {ContactId}", message.Id);
                throw;
            }

        }
    }
}
