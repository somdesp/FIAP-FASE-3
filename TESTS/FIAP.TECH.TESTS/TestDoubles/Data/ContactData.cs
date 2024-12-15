using FIAP.TECH.CORE.DOMAIN.Entities;

namespace FIAP.TECH.TESTS.TestDoubles.Data;

public class ContactData
{
    public static List<Contact> Get()
    {
        return
        [
            new() {
                Id = 1,
                Email = "contact1@email.com",
                DDD = "11",
                Name = "Contact 1",
                PhoneNumber = "1234567890",
                CreatedDate = new DateTime(2024, 1, 1)
            },
            new() {
                Id = 2,
                Email = "contact2@email.com",
                DDD = "11",
                Name = "Contact 2",
                PhoneNumber = "1234567891",
                CreatedDate = new DateTime(2024, 1, 1)
            },
            new() {
                Id = 3,
                Email = "contact3@email.com",
                DDD = "13",
                Name = "Contact 3",
                PhoneNumber = "1234567892",
                CreatedDate = new DateTime(2024, 1, 1)
            }
        ];
    }
}
