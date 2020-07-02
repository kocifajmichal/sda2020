using ambulance_api.Models;
using Microsoft.AspNetCore.Mvc;

namespace ambulance_api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DevelopersController : ControllerBase
    {
        [HttpGet("ambulance/{ambulanceId}")]
        public IActionResult GetAmbulance(string ambulanceId)
        {
            return Ok(new Ambulance { Id = "55", Name = "Doktor 1", RoomNumber = "A205" });
        }
    }
}