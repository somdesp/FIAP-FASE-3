namespace FIAP.TECH.CORE.DOMAIN.Entities;

public class BaseEntity
{
    public int Id { get; set; }
    public DateTime CreatedDate { get; set; }

    public BaseEntity() => 
        CreatedDate = DateTime.Now;
}
