using FIAP.TECH.CORE.DOMAIN.Models;
using MassTransit;

namespace FIAP.TECH.WORK.Events
{
    public class QueueErrosContactConsumer : IConsumer<ContactErros>
    {
        public async Task Consume(ConsumeContext<ContactErros> context)
        {
            var message = context.Message;

            try
            {
                await Task.Delay(1000);
                Console.WriteLine(message.Data != null ? message.Data.ToString() : "");
                Console.WriteLine(message.Data != null ? $"Error for Contact {message.Data.Id}: {string.Join(", ", message.Errors)}" : "");

                await Task.CompletedTask;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
