namespace FIAP.TECH.CORE.DOMAIN.Entities;

public class Contact : BaseEntity
{
    public required string Name { get; set; }
    public required string Email { get; set; }
    public required string DDD { get; set; }
    public required string PhoneNumber { get; set; }

    public int RegionId { get; set; }
    public Region? Region { get; set; }
}
