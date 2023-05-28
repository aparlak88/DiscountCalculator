namespace ShopsRU.Models;

public class User : BaseModel
{
    public string FirstName { get; set; } = "";
    public string LastName { get; set; } = "";
    private int userTypeNo;
    public int UserTypeNo
    {
        get
        {
            return this.userTypeNo;
        }
        set
        {
            this.userTypeNo = (DateTime.UtcNow.Year - this.CreatedAt.Year) > 1 
            ? (int)UserType.Loyal 
            : value;
        }
    }
    public double DiscountMultiplier
    {
        get
        {
            switch (UserTypeNo)
            {
                case (int)UserType.Employee:
                    return 0.7;
                case (int)UserType.Affiliate:
                    return 0.9;
                case (int)UserType.Loyal:
                    return 0.95;
                default:
                    return 1;
            }
        }
    }
}