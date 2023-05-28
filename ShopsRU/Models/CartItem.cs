namespace ShopsRU.Models;

public class CartItem
{
    private double price;
    public double Price
    {
        get
        {
            return Math.Round(this.price, 2);
        }
        set
        {
            if (value <= 0 || value >= double.MaxValue)
                throw new ArgumentException(string.Format("Price should be higher than zero or less than {0}", double.MaxValue));
            
            this.price = Math.Round(value, 2);
        }
    }
    public bool IsGrocery { get; set; } = false;
}