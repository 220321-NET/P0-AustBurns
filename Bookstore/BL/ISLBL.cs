using Models;
namespace BL;

public interface ISLBL
{
    //This is for the customer
    Task<Customer> CreateCustomerAsync(Customer customerToCreate);
    Task<Order> AddCustomerHistoryAsync(Order updateHistory);

    // Order GetCustomerHistory(Order getHistory);
    Task<Customer> SelectCustomerAsync(string UserName);
    Task<int> CheckLoginAsync(string UserName);

    //This is for store related items
    Task<List<StoreFront>> SelectStoreAsync();
    

    //This is for Inventory/Product related items
    Task<List<Inventory>> GetInventory(int StoreID);
    Product CreateNewProduct(Product newProduct);
    Task<Product> SelectProductAsync(int ProductID);
    Task<List<Product>> AllProductsAsync();
}
