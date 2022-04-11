
using System.ComponentModel.DataAnnotations;

public class Order : BasicID
{
    private double total = 0.00; 
    private List<Product> cartItems = new List<Product>(); 
    private int customerID = 0;
    private int storeID = 0;

//------------------------------------------------------------------

    public DateTime DatePurchased { get; set; }

    public void AddTotal (double addPrice)
    {
        total += addPrice;
    } 

    public double Total()
    {
        return total;
    }

    public int CustomerID {
        get => customerID; 
        set
        {
            if(value <= 0)
                throw new ValidationException("There was an issue with the Customer Id");
            
            customerID = value;
        }
    }

    public int StoreID {
        get => storeID; 
        set
        {
            if(value <= 0)
                throw new ValidationException("There was an issue with the store's Id");
            
            storeID = value;
        }
    }

    

    public void AddCartItems (Product adding)
    {
        cartItems.Add(adding);
        AddTotal(adding.cost);
    }

    public List<Product> CartItem ()
    {
        return cartItems;
    }


    public override string ToString()
    {
        return $"[{Id}]:{DatePurchased}, {CustomerID}, {StoreID}, {Total} ";
    }
    
}