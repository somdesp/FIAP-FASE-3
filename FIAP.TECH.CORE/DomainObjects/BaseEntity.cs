namespace FIAP.TECH.CORE.DomainObjects;

public class BaseEntity
{
    public int Id { get; set; }
    public DateTime CreatedDate { get; set; }

    public BaseEntity() =>
        CreatedDate = DateTime.Now;
}
