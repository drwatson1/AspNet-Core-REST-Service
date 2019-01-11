using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace ReferenceProject.Controllers
{
#pragma warning disable RECS0154 // Parameter is never used

    /// <summary>
    /// Standart ASP.Net Core Example Controller
    /// </summary>
    [Route("[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class ValuesController : ControllerBase
    {
        /// <summary>
        /// Get values
        /// </summary>
        /// <response code="200">List of values</response>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        /// <summary>
        /// Add new value
        /// </summary>
        /// <param name="value">New value</param>
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
