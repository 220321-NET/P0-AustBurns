using Microsoft.AspNetCore.Mvc;
using BL;
using Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StoreController : ControllerBase
    {

        private readonly ISLBL _bl;

        public StoreController(ISLBL bl)
            {
                _bl = bl;
            }

        // GET: api/<StoreController>
        [HttpGet("SelectStore")]
        public  async Task<List<StoreFront>> Get()
        {
            return await  _bl.SelectStoreAsync();
        }


        // POST api/<StoreController>
        [HttpPost("AddStore")]
        public void Post([FromBody] Customer customerToCreate)
        {
        }

    }
}
