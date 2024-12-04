using FIAP.TECH.CORE.DomainObjects;
using FIAP.TECH.CORE.DTOs;
using MassTransit;

namespace FIAP.TECH.API.CREATION.Services
{
    public class CreationService : ICreationService
    {
        private readonly IBusControl _busControl;
        private readonly IRequestClient<Contact> _requestClient;

        public CreationService(IBusControl busControl, IRequestClient<Contact> requestClient)
        {
            _requestClient = requestClient;
            _busControl = busControl;
        }

        public async Task<ContactResponse> SendMessageAsync(Contact message)
        {
            try
            {
                var response = await _requestClient.GetResponse<ContactResponse>(message);
                return response.Message;
            }
            catch (Exception ex)
            {
                return new ContactResponse(false, [ex.Message]);
            }
        }
    }
}
