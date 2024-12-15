using FIAP.TECH.CORE.DOMAIN.Entities;

namespace FIAP.TECH.TESTS.TestDoubles.Data;

public class RegionData
{
    public static List<Region> Get()
    {
        return
        [
            new() {
                Id = 1,
                CreatedDate = DateTime.Now,
                Name = "São Paulo",
                UF = "SP",
                DDD = "11"
            },
            new() {
                Id = 2,
                CreatedDate = DateTime.Now,
                Name = "Santos",
                UF = "SP",
                DDD = "13"
            }
        ];
    }
}
