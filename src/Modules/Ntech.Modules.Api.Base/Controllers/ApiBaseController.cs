using Microsoft.AspNetCore.Mvc;
using Ntech.Contract.Entity.SubDatabase;
using Ntech.Core.Server;
using Ntech.Platform.Repository;
using System.Collections.Generic;

namespace Ntech.Modules.Api.Base.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApiBaseController : ControllerBase
    {
        protected readonly IUnitOfWorkSubDatabase UnitOfWork;

        public ApiBaseController(IUnitOfWorkSubDatabase unitOfWork)
        {
            this.UnitOfWork = unitOfWork;
        }

        // GET api/values
        [HttpGet]
        public IEnumerable<Value> Get()
        {
            // TODO: Just for test
            return this.UnitOfWork.ValueRepository.Entities;
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
