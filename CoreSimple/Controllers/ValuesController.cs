using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CoreSimple.Service;
using CoreSimple.Model;
using System.Net;
using Microsoft.Extensions.Logging;
using CoreSimple.Logger;

namespace CoreSimple.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        private readonly ServiceAsync<City> _serviceAsync;
        private readonly IService<City> _service;
        private readonly ILogger _logger;
        public ValuesController(ServiceAsync<City> serviceAsync, IService<City> service, ILoggerFactory logger)
        {
            _serviceAsync = serviceAsync;
            _service = service;
            _logger = logger.CreateLogger("FileLogger");
        }
        // GET api/values
        [HttpGet]
        public IEnumerable<City> Get()
        {
            _logger.LogInformation("{TIME} - CityController: Get method. ", DateTime.Now);
            return _service.Get();
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            _logger.LogInformation("{TIME} - CityController: Get method with id {ID}.", DateTime.Now, id);
            var item = await _serviceAsync.GetAsync(t => t.Id == id);
            if (item == null)
            {
                _logger.LogInformation("{TIME} - CityController: Get method with {ID}. Error - Not found!", DateTime.Now, id);
                return NotFound();
            }

            _logger.LogInformation("{TIME} - CityController: Get method with {ID}. Success - Element Found!", DateTime.Now, id);
            return new ObjectResult(item);
        }

        // POST api/values
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]City city)
        {
            _logger.LogInformation("{TIME} - CityController: Post method ", DateTime.Now);

            if (city == null)
            {
                _logger.LogInformation("{TIME} - CityController: Post method. Error - City is empty! ", DateTime.Now);
                return NotFound();
            }
            await _serviceAsync.AddAsync(city);

            _logger.LogInformation("{TIME} - CityController: Post method. Success!", DateTime.Now);
            return Ok();
        }
        // PUT api/values/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody]City city)
        {
            _logger.LogInformation("{TIME} - CityController: Put method ", DateTime.Now);
            if (city == null )
            {
                _logger.LogInformation("{TIME} - CityController: Put method. Error - City is empty! ", DateTime.Now);
                return BadRequest();
            }
           
            city.Id = id;
            await _serviceAsync.EditAsync(city);

            _logger.LogInformation("{TIME} - CityController: Put method. Success!", DateTime.Now);
            return new NoContentResult();
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            _logger.LogInformation("{TIME} - CityController: Delete method ", DateTime.Now);
            var item= _service.Get(t => t.Id == id);

            if (item == null)
            {
                _logger.LogInformation("{TIME} - CityController: Delete method with {ID}. Error - Not found!", DateTime.Now, id);
                return NotFound();
            }
            await _serviceAsync.RemoveAsync(item);

            _logger.LogInformation("{TIME} - CityController: Delete method. Success!", DateTime.Now);
            return new NoContentResult();
        }
    }
}
