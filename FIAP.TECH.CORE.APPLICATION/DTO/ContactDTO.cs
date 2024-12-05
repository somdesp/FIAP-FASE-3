namespace FIAP.TECH.CORE.APPLICATION.DTO;

public class ContactDto
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string Email { get; set; }
    public required string PhoneNumber { get; set; }
    public required string DDD { get; set; }
}
