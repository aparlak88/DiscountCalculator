using ShopsRU.Data;
using ShopsRU.Models;

namespace ShopsRU.Logic;

public class CheckoutHelper
{
    private readonly IRepository _repository;

    public CheckoutHelper(IRepository repository)
    {
        _repository = repository;
    }

    public Invoice GenerateInvoice(Guid userId, List<CartItem> cartItems)
    {
        if (userId == Guid.Empty)
            throw new ArgumentException("Invalid user Id.");

        if (cartItems == null)
            throw new ArgumentException("Cart items are null.");

        if (cartItems.Count < 1)
            throw new ArgumentException("No items in cart.");

        return new Invoice(_repository.GetUserById(userId))
        {
            Id = Guid.NewGuid(),
            CreatedAt = DateTime.UtcNow,
            TotalAmount = cartItems.Sum(x => x.Price),
            DiscountableAmount = cartItems.Where(x=> !x.IsGrocery).Sum(x => x.Price)
        };
    }
}