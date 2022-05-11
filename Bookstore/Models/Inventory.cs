public class Inventory
{

    public Product invProduct {get; set;} = new Product();

    public int quantity {get; set;}

    public override string ToString()
    {
        return $"{invProduct.title}, {invProduct.content}, {invProduct.cost}, [{quantity} in stock]";
    } 

}