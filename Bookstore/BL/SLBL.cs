using System.Diagnostics;
using DL;
using Models;

namespace BL;


public class SLBL : ISLBL
    {

        private readonly IRepository _repo;

        public SLBL(IRepository repo)
        {
            _repo = repo;
        }

        public Customer CreateCustomer(Customer customerToCreate)
        {
            return _repo.CreateCustomer(customerToCreate);
        }

        public Order AddCustomerHistory(Order updateHistory)
        {
            return _repo.AddCustomerHistory(updateHistory);
        }

        public Order GetCustomerHistory(Order getHistory)
        {
            return _repo.GetCustomerHistory(getHistory);
        }

        public int CheckLogin(Customer login)
        {
            return _repo.CheckLogin(login);
        }

        public Customer SelectCustomer(Customer customer)
    {
        return _repo.SelectCustomer(customer);
    }

        public List<StoreFront> SelectStore()
        {
            return _repo.SelectStore();
        }

        public Product SelectProduct (int ProductID)
        {
            return _repo.SelectProduct(ProductID);
        }



        public List<Product> GetInventory(StoreFront getInv)
        {
            return _repo.GetInventory(getInv);
        }

        public Product CreateNewProduct(Product newProduct)
        {
            return _repo.CreateNewProduct(newProduct);
        }
    }
