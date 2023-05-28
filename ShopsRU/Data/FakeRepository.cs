using ShopsRU.Models;

namespace ShopsRU.Data;

public interface IRepository
{
    User GetUserById(Guid userId);
}

public class FakeRepository : IRepository
{
    private readonly List<User> _users;

    public FakeRepository()
    {
        _users = new List<User>
        {
            new User
            {
                Id = Guid.Parse("dad812c4-fca7-11ed-be56-0242ac120002"),
                FirstName = "John",
                LastName = "Doe",
                CreatedAt = DateTime.UtcNow,
                UserTypeNo = (int)UserType.Employee
            },
            new User
            {
                Id = Guid.Parse("dad81a3a-fca7-11ed-be56-0242ac120002"),
                FirstName = "Jane",
                LastName = "Doe",
                CreatedAt = DateTime.UtcNow,
                UserTypeNo = (int)UserType.Affiliate
            },
            new User
            {
                Id = Guid.Parse("dad81b98-fca7-11ed-be56-0242ac120002"),
                FirstName = "Juan",
                LastName = "Espinoza",
                CreatedAt = DateTime.UtcNow.AddYears(-2),
                UserTypeNo = (int)UserType.Regular
            },
            new User
            {
                Id = Guid.Parse("dad81cce-fca7-11ed-be56-0242ac120002"),
                FirstName = "Jaisnavi",
                LastName = "Kumar",
                CreatedAt = DateTime.UtcNow,
                UserTypeNo = (int)UserType.Regular
            }
        };
    }

    public User GetUserById(Guid userId)
    {
        return _users.First(x => x.Id == userId);
    }
}