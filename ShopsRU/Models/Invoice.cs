namespace ShopsRU.Models;

public class Invoice : BaseModel
{
    public Invoice(User user) => this.InvoiceUser = user;

    public User InvoiceUser { get; set; }
    public double TotalAmount { get; set; }
    public double DiscountableAmount { get; set; }
    public double PayableAmount
    {
        get
        {
            var amountWithoutIncrementalDiscount = (TotalAmount - this.DiscountableAmount) + (
                this.DiscountableAmount * this.InvoiceUser.DiscountMultiplier);
            return Math.Round(amountWithoutIncrementalDiscount -
               (Math.Truncate(amountWithoutIncrementalDiscount / 100) * 5), 2);
        }
    }
    public double TotalDiscount
    {
        get
        {
            return Math.Round(this.TotalAmount - this.PayableAmount, 2);
        }
    }
}