using Models;

namespace DL;

public interface IRepository    // This space you define all of the Functions that will be used inside your code and they all must be 
                                // used inside of the Database Repository
{

    //Customer stuff
    Customer CreateCustomer(Customer customerToCreate);
    Order AddCustomerHistory(Order updateHistory);
    Order GetCustomerHistory(Order getHistory);
    Customer SelectCustomer(Customer customer);
    int CheckLogin(Customer login);

    //Store stuff
    List<Product> GetInventory(StoreFront getInv);
    List<StoreFront> SelectStore();
    Product SelectProduct(int ProductID );

    //Inventory/product stuff
    Product CreateNewProduct(Product newProduct);

}