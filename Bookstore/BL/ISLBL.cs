using Models;
namespace BL;

public interface ISLBL
{
    Customer CreateCustomer(Customer customerToCreate);

    int CheckLogin(Customer login);

}
