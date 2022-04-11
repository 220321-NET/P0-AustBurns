// the class will be accessible through the Models namespace
namespace Models;
// class for line item model
public class LineItems
{
    public int Id { get; set;}
    public int storeId {get; set;}
    public int productId { get; set; }
    public int quantity { get; set; }
}