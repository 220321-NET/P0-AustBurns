using Models;

namespace DL;

public interface IRepository    // This space you define all of the Functions that will be used inside your code and they all must be 
                                // used inside of the Database Repository
{

    //Customer stuff
    Task<Customer> CreateCustomerAsync(Customer customerToCreate);
    Task<Order> AddCustomerHistoryAsync(Order updateHistory);
    Task<List<Order>> GetCustomerHistory(Order getHistory);
    Task<Customer> SelectCustomerAsync(string UserName);
    Task<int> CheckLoginAsync(string UserName);

    //Store stuff
    Task<List<Inventory>> GetInventory(int StoreID);
    Task<List<StoreFront>> SelectStoreAsync();
    Task<Product> SelectProductAsync(int ProductID );

    //Inventory/product stuff
    Product CreateNewProduct(Product newProduct);
    Task<List<Product>> AllProductsAsync();
}