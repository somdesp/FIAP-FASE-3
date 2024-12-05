namespace FIAP.TECH.CORE.DOMAIN.Entities;

public class Region : BaseEntity
{
    public required string Name { get; set; }
    public required string DDD { get; set; }
    public required string UF { get; set; }

    public ICollection<Contact>? Contacts { get; set; }
}
