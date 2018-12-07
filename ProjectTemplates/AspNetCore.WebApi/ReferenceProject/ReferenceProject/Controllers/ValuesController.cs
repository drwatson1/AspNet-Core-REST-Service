using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace ReferenceProject.Controllers
{
#pragma warning disable RECS0154 // Parameter is never used

    [Route("[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        // GET values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }

#pragma warning restore RECS0154 // Parameter is never used
}
