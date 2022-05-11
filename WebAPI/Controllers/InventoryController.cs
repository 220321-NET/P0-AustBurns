using Microsoft.AspNetCore.Mvc;
using Models;
using BL;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InventoryController : ControllerBase
    {


        private readonly ISLBL _bl;

        public InventoryController(ISLBL bl)
        {
            _bl = bl;
        }

        // GET: api/<InventoryController>
        [HttpGet("AllProducts")]
        public async Task<List<Product>> Get()
        {
            return await _bl.AllProductsAsync();
        }

        // // GET api/<InventoryController>/5
        // [HttpGet("{id}")]
        // public ActionResult<Product> Get(int id)
        // {
        //     return "value";
        // }

        // POST api/<InventoryController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<InventoryController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<InventoryController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
