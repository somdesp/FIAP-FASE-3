using FIAP.TECH.CORE.DOMAIN.Entities;
using FIAP.TECH.CORE.DOMAIN.Models;
using FIAP.TECH.INFRASTRUCTURE.Contexts;
using FluentValidation;
using MassTransit;
using Microsoft.EntityFrameworkCore;

namespace FIAP.TECH.WORK.Events
{
    public class QueueCreateUpdateContactConsumer : IConsumer<Contact>
    {
        private readonly IValidator<Contact> _validator;
        private readonly AppDbContext _dbContext;
        private readonly IPublishEndpoint _publishEndpoint;

        public QueueCreateUpdateContactConsumer(
            IValidator<Contact> validator,
            AppDbContext dbContext,
            IPublishEndpoint publishEndpoint)
        {
            _validator = validator;
            _dbContext = dbContext;
            _publishEndpoint = publishEndpoint;
        }

        public async Task Consume(ConsumeContext<Contact> context)
        {
            var contact = context.Message;
            Console.Clear();
            try
            {
                var validationResult = await _validator.ValidateAsync(context.Message);

                if (!validationResult.IsValid)
                {
                    await _publishEndpoint.Publish(new ContactErros
                    {
                        Errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList(),
                        Data = contact,
                        Success = false
                    });
                    return;
                }

                //valida regiao
                var region = await _dbContext.Regions.FirstOrDefaultAsync(x => x.DDD == contact.DDD);
                if (region == null)
                {
                    // Retornar erros de validação
                    await _publishEndpoint.Publish(new ContactErros
                    {
                        Errors = ["DDD inexistente na base de dados."],
                        Data = contact,
                        Success = false
                    });
                    return;
                }

                contact.RegionId = region.Id;
                bool isUpdate = false;
                //Valida se e update ou create
                if (contact.Id > 0)
                {
                    var contactUpdate = await _dbContext.Contacts.FirstOrDefaultAsync(x => x.Id == contact.Id);
                    if (contactUpdate == null)
                    {
                        // Retornar erros de validação
                        await _publishEndpoint.Publish(new ContactErros
                        {
                            Errors = ["Contato com ID informado não existe."],
                            Data = contact,
                            Success = false
                        });
                        return;
                    }

                    contactUpdate.Name = contact.Name;
                    contactUpdate.Email = contact.Email;
                    contactUpdate.PhoneNumber = contact.PhoneNumber;

                    isUpdate = true;
                    _dbContext.Contacts.Update(contactUpdate);
                }
                else
                    await _dbContext.Contacts.AddAsync(contact);

                await _dbContext.SaveChangesAsync();

                // Simulate success
                await Task.Delay(1000);
                Console.Clear();
                Console.WriteLine(!isUpdate ? "Contato Inserido com Sucesso" : "Contato Atualizado com Sucesso");
            }
            catch (Exception ex)
            {
                await _publishEndpoint.Publish(new ContactErros
                {
                    Errors = [ex.Message],
                    Data = contact,
                    Success = false
                });
                //Console.WriteLine(ex, "Error processing Contact updated event for Contact {ContactId}", message.Id);
                throw;
            }

        }
    }
}
