using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using ReportsWebApi.Models;
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

        [HttpGet]
        public IActionResult Get()
        {
            List<Models.Response> result = _service.GetMdxResponse();
            var response = result.Take(100);
            return Ok(response);
        }


        [HttpPost("openquery/{db}/{take?}")]
        public async Task<IActionResult> HandleOpenqueryAsync(string db, int? take)
        {
            var requestBody = await Request.Body.ReadAsStringAsync();

            var parms = Request.Query.ToDictionary();

            var result = _service.HandleOpenquery(db, requestBody, parms, take);
            
            return Ok(result);
        }

        //[HttpPost("sql")]
        //public async Task<IActionResult> HandleSqlQueryAsync()
        //{
        //    var requestBody = await Request.Body.ReadAsStringAsync();
        //    var request = $"select * from openquery([{db}],'{requestBody}')";
        //    var result = _service.GetMdxResponseByDapper(request);

        //    return Ok(result);
        //}
    }
}
