using Models;

namespace DL;

public interface IRepository
{
    Customer CreateCustomer(Customer customerToCreate);
    int CheckLogin(Customer login);
}