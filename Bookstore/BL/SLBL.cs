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

        public async Task<Customer> CreateCustomerAsync(Customer customerToCreate)
        {
            return await _repo.CreateCustomerAsync(customerToCreate);
        }

        public async Task<Order> AddCustomerHistoryAsync(Order updateHistory)
        {
            return  await _repo.AddCustomerHistoryAsync(updateHistory);
        }

        public async Task<List<Order>> GetCustomerHistory(Order getHistory)
        {
            return await _repo.GetCustomerHistory(getHistory);
        }

        public async Task<int> CheckLoginAsync(string UserName)
        {
            return await _repo.CheckLoginAsync(UserName);
        }

        public async Task<Customer> SelectCustomerAsync(string UserName)
        {
        return await _repo.SelectCustomerAsync(UserName);
        }

        public async Task<List<StoreFront>> SelectStoreAsync()
        {
            return await _repo.SelectStoreAsync();
        }

        public async Task<Product> SelectProductAsync(int ProductID)
        {
            return await _repo.SelectProductAsync(ProductID);
        }



        public async Task<List<Inventory>> GetInventory(int StoreID)
        {
            return await _repo.GetInventory(StoreID);
        }

        public Product CreateNewProduct(Product newProduct)
        {
            return _repo.CreateNewProduct(newProduct);
        }

        public async Task<List<Product>> AllProductsAsync()
        {
            return await _repo.AllProductsAsync();
        }

    }
