using Models;
namespace BL;

public interface ISLBL
{
    //This is for the customer
    Customer CreateCustomer(Customer customerToCreate);
    Order AddCustomerHistory(Order updateHistory);
    Order GetCustomerHistory(Order getHistory);
    Customer SelectCustomer(Customer customer);
    int CheckLogin(Customer login);

    //This is for store related items
    List<StoreFront> SelectStore();
    

    //This is for Inventory/Product related items
    List<Product> GetInventory(StoreFront getInv);
    Product CreateNewProduct(Product newProduct);
    Product SelectProduct(int ProductID);
}
