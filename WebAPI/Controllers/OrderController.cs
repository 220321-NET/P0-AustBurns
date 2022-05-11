using Microsoft.AspNetCore.Mvc;
using BL;
using Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly ISLBL _bl;

        public OrderController(ISLBL bl)
            {
                _bl = bl;
            }


        // POST api/<OrderController>
        [HttpPost("CreateOrder")]
        public async Task<Order> AddCustomerHistoryAsync([FromBody] Order updateHistory)
        {
            return await _bl.AddCustomerHistoryAsync(updateHistory);
        }

    }
}
