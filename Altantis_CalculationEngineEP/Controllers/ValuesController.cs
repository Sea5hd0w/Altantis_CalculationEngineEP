using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Altantis_CalculationEngineEP.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        // GET api/values
        [HttpGet]
        public string Get()
        {
            return Service.Scheduler.Instance.Status;
        }

        // GET api/values/5
        /*[HttpGet("{id}")]
        public Dictionary<string, Business.CacheDataDeviceSensor> Get(int id)
        {
            return Service.Scheduler.Instance.Cache;
        }*/
        [HttpGet("{id}")]
        public List<Dictionary<string, Dictionary<string, double>>> Get(int id)
        {
            List<Dictionary<string, Dictionary<string, double>>> temp = new List<Dictionary<string, Dictionary<string, double>>>();
            temp.Add(Service.Scheduler.Instance.Cache.First().Value.testc());
            temp.Add(Service.Scheduler.Instance.Cache.Last().Value.testc());
            //temp.Add(Service.Scheduler.Instance.Cache.First().Value.testc());


            return temp;
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
