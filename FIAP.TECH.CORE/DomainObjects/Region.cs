namespace FIAP.TECH.CORE.DomainObjects;

public class Region : BaseEntity
{
    public required string Name { get; set; }
    public required string DDD { get; set; }
    public required string UF { get; set; }

    public ICollection<Contact>? Contacts { get; set; }
}
