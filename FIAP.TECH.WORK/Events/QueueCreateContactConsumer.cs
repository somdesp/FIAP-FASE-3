﻿using FIAP.TECH.CORE.DOMAIN.Entities;
using FIAP.TECH.CORE.DOMAIN.Models;
using FIAP.TECH.INFRASTRUCTURE.Contexts;
using FluentValidation;
using MassTransit;
using Microsoft.EntityFrameworkCore;

namespace FIAP.TECH.WORK.Events
{
    public class QueueCreateContactConsumer : IConsumer<Contact>
    {
        private readonly IValidator<Contact> _validator;
        private readonly AppDbContext _dbContext;
        private readonly IPublishEndpoint _publishEndpoint;

        public QueueCreateContactConsumer(
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
            var message = context.Message;

            try
            {
                var validationResult = await _validator.ValidateAsync(context.Message);

                if (!validationResult.IsValid)
                {
                    await _publishEndpoint.Publish(new ContactErros
                    {
                        Errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList(),
                        Data = message,
                        Success = false
                    });
                    return;
                }

                //valida regiao
                var region = await _dbContext.Regions.FirstOrDefaultAsync(x => x.DDD == message.DDD);
                if (region == null)
                {
                    // Retornar erros de validação
                    await _publishEndpoint.Publish(new ContactErros
                    {
                        Errors = ["DDD inexistente na base de dados."],
                        Data = message,
                        Success = false
                    });
                    return;
                }

                message.RegionId = region.Id;

                await _dbContext.Contacts.AddAsync(message);
                await _dbContext.SaveChangesAsync();

                // Simulate success
                await Task.Delay(1000);

                Console.WriteLine("Contato Inserido com Sucesso");
            }
            catch (Exception ex)
            {
                await _publishEndpoint.Publish(new ContactErros
                {
                    Errors = [ex.Message],
                    Data = message,
                    Success = false
                });
                //Console.WriteLine(ex, "Error processing Contact updated event for Contact {ContactId}", message.Id);
                throw;
            }

        }
    }
}
