using FIAP.TECH.CORE.DOMAIN.Entities;

namespace FIAP.TECH.CORE.DOMAIN.Models
{
    public class ContactErros
    {
        public bool Success { get; set; }
        public Contact Data { get; set; }
        public List<string> Errors { get; set; }
    }

    public record ContactResponse(bool Success, string Message, List<Contact> Contacts);
    public record ContactByDDD(string DDD);

}
