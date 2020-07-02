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
            if (ambulanceId != ambulance.Id)
            {
                return BadRequest("Different Ids!");
            }
            var existing = myDataRepository.GetAmbulanceData(ambulanceId);
            if (existing != null)
            {
                return BadRequest("Ambulance with given Id already exists!");
            }
            return Ok(myDataRepository.UpsertAmbulanceData(ambulanceId, ambulance));
        }

        [HttpDelete("ambulance/{ambulanceId}")]
        public IActionResult DeleteAmbulance(string ambulanceId)
        {
            if (myDataRepository.DeleteAmbulanceData(ambulanceId))
            {
                return Ok();
            }
            return NotFound();
        }
    }
}
