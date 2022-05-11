using Microsoft.AspNetCore.Mvc;
using Models;
using BL;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {


        private readonly ISLBL _bl;

        public CustomerController(ISLBL bl)
        {
            _bl = bl;
        }

        // GET: api/<CustomerController>
        [HttpGet("GetLogin/{username}")]
        public async Task<int> CheckLoginAsync(string UserName)
        {
            return await _bl.CheckLoginAsync(UserName);
        }

        [HttpGet("GetCustomer/{username}")]
        public async Task<Customer> SelectCustomerAsync(string username)
        {
            return await _bl.SelectCustomerAsync(username);
        }

        // POST api/<CustomerController>
        [HttpPost("CreateCustomer")]
        public async Task<Customer> CreateCustomerAsync([FromBody] Customer customerToCreate)
        {
            return await _bl.CreateCustomerAsync(customerToCreate);
        }

    }
}
