using FIAP.TECH.CORE.DomainObjects;
using FIAP.TECH.INFRASTRUCTURE.Contexts;
using MassTransit;

namespace FIAP.TECH.WORK.Events
{
    public class QueueCreateContactConsumer : IConsumer<Contact>
    {
        private readonly ILogger<QueueCreateContactConsumer> _logger;

        private readonly AppDbContext _context;

        public QueueCreateContactConsumer(ILogger<QueueCreateContactConsumer> logger, AppDbContext context)
        {
            _logger = logger;
            _context = context;
        }


        public async Task Consume(ConsumeContext<Contact> context)
        {
            var message = context.Message;

            try
            {
                _logger.LogInformation("Order updated event received: {QueueCreateContactConsumer} -------> Successfully recieved!", message);

                await _context.Contacts.AddAsync(message);
                await _context.SaveChangesAsync();

                await Task.CompletedTask;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error processing Contact updated event for Contact {ContactId}", message.Id);
                throw;
            }

        }
    }

    public class QueueContactConsumerDefinition : ConsumerDefinition<QueueCreateContactConsumer>
    {
        protected override void ConfigureConsumer(IReceiveEndpointConfigurator endpointConfigurator, IConsumerConfigurator<QueueCreateContactConsumer> consumerConfigurator, IRegistrationContext context)
        {
            consumerConfigurator.UseMessageRetry(retry => retry.Interval(3, TimeSpan.FromSeconds(3)));
        }
    }
}
