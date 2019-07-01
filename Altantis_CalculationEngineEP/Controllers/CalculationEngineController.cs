using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Altantis_CalculationEngineEP.Controllers
{
    [Produces("application/json")]
    [Route("api/CalculationEngine")]
    public class CalculationEngineController : Controller
    {
        // GET: api/CalculationEngine
        [HttpGet]
        public DateTime Get()
        {
            return Service.Scheduler.Instance.startTime;
        }

        // GET: api/CalculationEngine/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }
        
        // POST: api/CalculationEngine
        [HttpPost]
        public async Task<string> Post()
        {
            using (StreamReader reader = new StreamReader(Request.Body, Encoding.UTF8))
            {
                Service.Scheduler.Instance.AssignRawDataToCache(reader.ReadToEndAsync().Result);
                return await reader.ReadToEndAsync();
            }
        }

        // PUT: api/CalculationEngine/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }
        
        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
