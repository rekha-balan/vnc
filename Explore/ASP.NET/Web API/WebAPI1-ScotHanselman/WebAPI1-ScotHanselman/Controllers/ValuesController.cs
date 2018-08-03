using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebAPI1_ScotHanselman.Controllers
{
    public class PersonController : ApiController
    {
        public class Person
        {

            public int ID { get; set; }
            public string First { get; set; }
            public string Last { get; set; }

        }

        // Below is from template

        // GET api/values
        public IEnumerable<Person> Get()
        {
            return new List<Person>
            {
                new Person { ID = 0, First = "Vikki", Last = "Schanz"},
                new Person { ID = 1, First = "Natalie", Last = "Rhodes"},
                new Person { ID = 2, First = "Christopher", Last = "Rhodes"}
            };
        }

        // GET api/values/5
        public Person Get(int id)
        {
            return null;
        }

        // POST api/values
        public void Post([FromBody]Person value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]Person value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }

    // Below is from template

    //public class ValuesController : ApiController
    //{
    //    // Below is from template

    //    // GET api/values
    //    public IEnumerable<string> Get()
    //    {
    //        return new string[] { "value1", "value2" };
    //    }

    //    // GET api/values/5
    //    public string Get(int id)
    //    {
    //        return "value";
    //    }

    //    // POST api/values
    //    public void Post([FromBody]string value)
    //    {
    //    }

    //    // PUT api/values/5
    //    public void Put(int id, [FromBody]string value)
    //    {
    //    }

    //    // DELETE api/values/5
    //    public void Delete(int id)
    //    {
    //    }
    //}
}
