namespace Basket.API.Entities;

public class ShoppingCart
{
    public ShoppingCart()
    { }

    public ShoppingCart(string userName)
    {
        UserName = userName;
    }
    
    public string UserName { get; set; }
    public List<ShoppingCartItems> Items { get; set; } = new List<ShoppingCartItems>();

    public decimal TotalCost
    {
        get
        {
            return Items.Sum(item => item.Price * item.Quantity);
        }
    }
}