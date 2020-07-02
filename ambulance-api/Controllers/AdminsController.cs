using System.Data.Common;
using System;
using ambulance_api.Models;
using ambulance_api.Services;
using Microsoft.AspNetCore.Mvc;

namespace ambulance_api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AdminsController : ControllerBase
    {
        private readonly IDataRepository myDataRepository;

        public AdminsController(IDataRepository dataRepository)
        {
            myDataRepository = dataRepository ?? throw new ArgumentNullException(nameof(dataRepository));
        }

        [HttpPost("ambulance/{ambulanceId}")]
        public IActionResult CreateAmbulance([FromRoute] string ambulanceId, [FromBody] Ambulance ambulance)
        {
            if (string.IsNullOrWhiteSpace(ambulanceId) || ambulance == null)
            {
                return BadRequest();
            }
            return Ok(myDataRepository.UpsertAmbulanceData(ambulanceId, ambulance));
        }

        
    }
}
