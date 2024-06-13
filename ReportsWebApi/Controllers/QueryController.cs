using Microsoft.AspNetCore.Mvc;
using ReportsWebApi.Services;

namespace ReportsWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QueryController : ControllerBase
    {
        private readonly IQueryService _service;

        public QueryController(IQueryService service)
        {
            _service = service;
        }

        [HttpPost("sql")]
        public async Task<IActionResult> HandleSqlQueryAsync()
        {
            var requestBody = await Request.Body.ReadAsStringAsync();

            var parms = Request.Query.ToDictionary();

            var result = _service.HandleSqlQuery(requestBody, parms);

            if (result.FirstOrDefault()?.Error != null)
            {
                return BadRequest(result.FirstOrDefault());
            }

            return Ok(result);
        }


        [HttpPost("openquery/{db}/{take?}")]
        public async Task<IActionResult> HandleOpenqueryAsync(string db, int? take)
        {
            var requestBody = await Request.Body.ReadAsStringAsync();

            var parms = Request.Query.ToDictionary();

            var result = _service.HandleOpenquery(requestBody, parms, db, take);

            if (result.FirstOrDefault()?.Error != null)
            {
                return BadRequest(result.FirstOrDefault());
            }

            return Ok(result);
        }
    }
}
