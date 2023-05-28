using ShopsRU.Data;
using ShopsRU.Logic;
using ShopsRU.Models;

namespace ShopsRU.Test;

public class Tests
{
    private Mock<IRepository> _mockRepository;
    private CheckoutHelper _checkoutHelper;

    public Tests()
    {
        _mockRepository = new Mock<IRepository>();
        _checkoutHelper = new CheckoutHelper(_mockRepository.Object);
    }

    [Fact]
    public void GenerateInvoice_ShouldThrowArgumentException_WhenUserIdIsEmpty()
    {
        Assert.Throws<ArgumentException>(() => _checkoutHelper.GenerateInvoice(Guid.Empty, new List<Models.CartItem>()));
    }

    [Fact]
    public void GenerateInvoice_ShouldThrowArgumentException_WhenCartItemsAreNull()
    {
        Assert.Throws<ArgumentException>(() => _checkoutHelper.GenerateInvoice(Guid.NewGuid(), null));
    }

    [Fact]
    public void GenerateInvoice_ShouldThrowArgumentException_WhenCartItemsAreEmpty()
    {
        Assert.Throws<ArgumentException>(() => _checkoutHelper.GenerateInvoice(Guid.NewGuid(), new List<Models.CartItem>()));
    }

    [Fact]
    public void GenerateInvoice_ShouldReturnInvoice()
    {
        var dummyUser = new User
        {
            Id = Guid.NewGuid(),
            FirstName = "Fred",
            LastName = "Flintstone",
            UserTypeNo = (int)UserType.Employee,
            CreatedAt = DateTime.UtcNow
        };

        var dummyCart = new List<CartItem>
        {
            new CartItem
            {
                Price = 100.0,
                IsGrocery = true
            },
            new CartItem
            {
                Price = 149.99,
                IsGrocery = false
            }
        };

        _mockRepository.Setup(x => x.GetUserById(It.IsAny<Guid>()))
            .Returns(dummyUser);

        Assert.NotNull(_checkoutHelper.GenerateInvoice(dummyUser.Id, dummyCart));
    }

    [Fact]
    public void GenerateInvoice_PayableAmountShouldEqualToValue_WhenUserIsAnAffiliate()
    {
        var dummyUser = new User
        {
            Id = Guid.NewGuid(),
            FirstName = "Pyotr",
            LastName = "Lujin",
            CreatedAt = DateTime.UtcNow,
            UserTypeNo = (int)UserType.Affiliate,
        };

        var dummyCart = new List<CartItem>
        {
            new CartItem
            {
                Price = 100,
                IsGrocery = true
            },
            new CartItem
            {
                Price = 100,
                IsGrocery = false
            }
        };

        _mockRepository.Setup(x => x.GetUserById(It.IsAny<Guid>()))
            .Returns(dummyUser);

        Assert.Equal(185, _checkoutHelper.GenerateInvoice(dummyUser.Id, dummyCart).PayableAmount);
    }

    [Fact]
    public void GenerateInvoice_PayableAmountShouldEqualToValue_WhenUserIsALoyalCustomer()
    {
        var dummyUser = new User
        {
            Id = Guid.NewGuid(),
            FirstName = "Dimitriy",
            LastName = "Razumihin",
            CreatedAt = DateTime.UtcNow.AddYears(-2),
            UserTypeNo = (int)UserType.Regular,
        };

        var dummyCart = new List<CartItem>
        {
            new CartItem
            {
                Price = 100,
                IsGrocery = true
            },
            new CartItem
            {
                Price = 100,
                IsGrocery = false
            }
        };

        _mockRepository.Setup(x => x.GetUserById(It.IsAny<Guid>()))
            .Returns(dummyUser);

        Assert.Equal(190, _checkoutHelper.GenerateInvoice(dummyUser.Id, dummyCart).PayableAmount);
    }

    [Fact]
    public void GenerateInvoice_PayableAmountShouldEqualToValue_WhenUserIsARegularCustomer()
    {
        var dummyUser = new User
        {
            Id = Guid.NewGuid(),
            FirstName = "Semyon",
            LastName = "Zakharovich",
            CreatedAt = DateTime.UtcNow,
            UserTypeNo = (int)UserType.Regular,
        };

        var dummyCart = new List<CartItem>
        {
            new CartItem
            {
                Price = 100,
                IsGrocery = true
            },
            new CartItem
            {
                Price = 100,
                IsGrocery = false
            }
        };

        _mockRepository.Setup(x => x.GetUserById(It.IsAny<Guid>()))
            .Returns(dummyUser);

        Assert.Equal(190, _checkoutHelper.GenerateInvoice(dummyUser.Id, dummyCart).PayableAmount);
    }

    [Fact]
    public void GenerateInvoice_TotalDiscountShouldEqualToSubstractionOfTotalAmountAndPayableAmount()
    {
        var dummyUser = new User
        {
            Id = Guid.NewGuid(),
            FirstName = "Kitty",
            LastName = "Softpaws",
            CreatedAt = DateTime.UtcNow,
            UserTypeNo = (int)UserType.Employee,
        };

        var dummyCart = new List<CartItem>
        {
            new CartItem
            {
                Price = 100,
                IsGrocery = true
            },
            new CartItem
            {
                Price = 100,
                IsGrocery = false
            }
        };

        _mockRepository.Setup(x => x.GetUserById(It.IsAny<Guid>()))
            .Returns(dummyUser);

        var dummyInvoice = _checkoutHelper.GenerateInvoice(dummyUser.Id, dummyCart);
        var totalDiscount = Math.Round(dummyInvoice.TotalAmount - dummyInvoice.PayableAmount, 2);

        Assert.Equal(totalDiscount, dummyInvoice.TotalDiscount);
    }

    [Fact]
    public void CartItem_ShouldThrowArgumentException_WhenPriceIsEqualToZeroOrBelow()
    {
        Assert.Throws<ArgumentException>(() => new CartItem { Price = 0, IsGrocery = false});
    }
}