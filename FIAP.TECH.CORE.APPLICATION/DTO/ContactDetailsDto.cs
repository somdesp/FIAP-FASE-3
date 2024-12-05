namespace FIAP.TECH.CORE.APPLICATION.DTO;

public class ContactDetailsDto
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Email { get; set; }
    public string? PhoneNumber { get; set; }
    public string? DDD { get; set; }
    public RegionDetailsDto? Region { get; set; }
}