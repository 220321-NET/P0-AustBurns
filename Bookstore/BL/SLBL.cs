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

        public int CheckLogin(Customer login)
        {
            return _repo.CheckLogin(login);
        }
    }
