using Microsoft.AspNetCore.Mvc;
using ReportsWebApi.Services;

namespace ReportsWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DateTimeController(IDateTimeService service) : ControllerBase
    {
        private readonly IDateTimeService _service = service;

        [HttpGet]
        public IActionResult Get() { 
            var result = _service.GetCurrentDateTime();
            return Ok(result);
        }
    }
}
