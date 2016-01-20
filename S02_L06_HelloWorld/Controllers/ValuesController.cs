using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace S02_L06_HelloWorld.Controllers
{
    [Authorize]
    [AllowAnonymous]
    public class ValuesController : ApiController
    {
        // GET api/values
        [HttpGet]
        public IEnumerable<string> List()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet]
        public string Single(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Submit([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut]
        public void Update(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete]
        public void Remove(int id)
        {
        }
    }
}
