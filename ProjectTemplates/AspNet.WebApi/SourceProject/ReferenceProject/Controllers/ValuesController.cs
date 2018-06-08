using Serilog;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace ReferenceProject
{
    /// <summary>
    /// Example
    /// </summary>
    public class ValuesController : ApiController
    {
        private ILogger Logger { get; }

        public ValuesController(ILogger logger)
        {
            Logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        // GET api/<controller>
        public IEnumerable<string> Get()
        {
            Logger.Information("URL: {HttpRequestUrl}");
            return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
#pragma warning disable RECS0154 // Parameter is never used
        public string Get(int id)
#pragma warning restore RECS0154 // Parameter is never used
        {
            return "value";
        }

        // POST api/<controller>
#pragma warning disable RECS0154 // Parameter is never used
        public void Post([FromBody]string value)
#pragma warning restore RECS0154 // Parameter is never used
        {
        }

        // PUT api/<controller>/5
#pragma warning disable RECS0154 // Parameter is never used
#pragma warning disable RECS0154 // Parameter is never used
        public void Put(int id, [FromBody]string value)
#pragma warning restore RECS0154 // Parameter is never used
#pragma warning restore RECS0154 // Parameter is never used
        {
        }

        // DELETE api/<controller>/5
#pragma warning disable RECS0154 // Parameter is never used
        public void Delete(int id)
#pragma warning restore RECS0154 // Parameter is never used
        {
        }
    }
}