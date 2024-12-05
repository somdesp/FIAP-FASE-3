using FIAP.TECH.CORE.DOMAIN.Entities;
using FIAP.TECH.CORE.DOMAIN.Models;
using FIAP.TECH.INFRASTRUCTURE.Contexts;
using FluentValidation;
using MassTransit;
using Microsoft.EntityFrameworkCore;

namespace FIAP.TECH.WORK.Events
{
    public class QueueConsultContactConsumer : IConsumer<ContactByDDD>
    {
        private readonly IValidator<Contact> _validator;
        private readonly AppDbContext _dbContext;
        private readonly IPublishEndpoint _publishEndpoint;

        public QueueConsultContactConsumer(IValidator<Contact> validator,
            AppDbContext dbContext,
            IPublishEndpoint publishEndpoint)
        {
            _validator = validator;
            _dbContext = dbContext;
            _publishEndpoint = publishEndpoint;
        }

        public async Task Consume(ConsumeContext<ContactByDDD> context)
        {
            var message = context.Message;

            try
            {
                if (string.IsNullOrWhiteSpace(context.Message.DDD))
                {
                    await _publishEndpoint.Publish(new ContactErros
                    {
                        Errors = ["DDD obrigatorio"],
                        Data = null,
                        Success = false
                    });
                    return;
                }

                var result = await _dbContext.Contacts
                      .AsNoTracking()
                      .Include(c => c.Region)
                      .Where(c => c.DDD == context.Message.DDD)
                      .ToListAsync();

                // Simulate success
                await Task.Delay(1000);

                Console.WriteLine("Sucesso no retorno dos contatos");
                await context.RespondAsync(new ContactResponse(true, "Sucesso no retorno dos contatos", result));
            }
            catch (Exception ex)
            {
                await _publishEndpoint.Publish(new ContactErros
                {
                    Errors = [ex.Message],
                    Data = null,
                    Success = false
                });
                //Console.WriteLine(ex, "Error processing Contact updated event for Contact {ContactId}", message.Id);
                throw;
            }

        }
    }
}
