using System.Linq;
using System;
using ambulance_api.Models;
using ambulance_api.Services;
using Microsoft.AspNetCore.Mvc;

namespace ambulance_api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DevelopersController : ControllerBase
    {
        private readonly IDataRepository myDataRepository;

        public DevelopersController(IDataRepository dataRepository)
        {
            myDataRepository = dataRepository ?? throw new ArgumentNullException(nameof(dataRepository));
        }

        [HttpGet("ambulance/{ambulanceId}")]
        public IActionResult GetAmbulance(string ambulanceId)
        {
            if (string.IsNullOrWhiteSpace(ambulanceId))
            {
                return BadRequest();
            }

            var ambulance = myDataRepository.GetAmbulanceData(ambulanceId);
            if (ambulance == null)
            {
                return NotFound();
            }
            return Ok(ambulance);
        }

        [HttpGet("ambulance/{ambulanceId}/condition")]
        public IActionResult GetConditions(string ambulanceId)
        {
            var ambulance = myDataRepository.GetAmbulanceData(ambulanceId);
            if (ambulance == null)
            {
                return NotFound();
            }
            return Ok(ambulance.Conditions);
        }

        [HttpGet("ambulance/{ambulanceId}/condition/{conditionCode}")]
        public IActionResult GetCondition(string ambulanceId, string conditionCode)
        {
            var ambulance = myDataRepository.GetAmbulanceData(ambulanceId);
            if (ambulance == null)
            {
                return NotFound();
            }
            /*
            // 1. moznost:
            for (int i = 0; i < ambulance.Conditions.Count; i++)
            {
                if (ambulance.Conditions[i].Code.Equals(conditionCode))
                {
                    return Ok(ambulance.Conditions[i]);
                }
            }
            // 2. moznost:
            foreach (var condition in ambulance.Conditions)
            {
                if (condition.Code.Equals(conditionCode))
                {
                    return Ok(condition);
                }
            }
            */
            // 3. moznost:
            var cond = ambulance.Conditions.FirstOrDefault(c => c.Code.Equals(conditionCode));
            if (cond == null)
            {
                return NotFound();
            }
            return Ok(cond);
        }

        [HttpPost("ambulance/{ambulanceId}/entry")]
        public IActionResult CreateWaitingListEntry(string ambulanceId, [FromBody] WaitingListEntry entry)
        {
            if (!FindAmbulance(ambulanceId, out var ambulance))
            {
                return NotFound();
            }
            entry.Id = Guid.NewGuid().ToString();
            ambulance.WaitingList.Add(entry);
            myDataRepository.UpsertAmbulanceData(ambulanceId, ambulance);
            return Ok(entry);
        }

        [HttpGet("ambulance/{ambulanceId}/entry/{entryId}")]
        public IActionResult GetWaitingListEntry(string ambulanceId, string entryId)
        {
            if (!FindAmbulance(ambulanceId, out var ambulance))
            {
                return NotFound();
            }
            var entry = ambulance.WaitingList.FirstOrDefault(e => e.Id == entryId);
            if (entry == null)
            {
                return NotFound();
            }
            return Ok(entry);
        }

        [HttpGet("ambulance/{ambulanceId}/entry")]
        public IActionResult GetWaitingListEntries(string ambulanceId)
        {
            if (!FindAmbulance(ambulanceId, out var ambulance))
            {
                return NotFound();
            }
            return Ok(ambulance.WaitingList);
        }

        [HttpDelete("ambulance/{ambulanceId}/entry/{entryId}")]
        public IActionResult DeleteWaitingListEntry(string ambulanceId, string entryId)
        {
            if (!FindAmbulance(ambulanceId, out var ambulance))
            {
                return NotFound();
            }
            var entry = ambulance.WaitingList.FirstOrDefault(e => e.Id == entryId);
            if (entry == null)
            {
                return NotFound();
            }
            ambulance.WaitingList.Remove(entry);
            myDataRepository.UpsertAmbulanceData(ambulanceId, ambulance);
            return Ok("Entry successfully deleted");
        }

        [HttpPost("ambulance/{ambulanceId}/entry/{entryId}")]
        public IActionResult UpdateWaitingListEntry(string ambulanceId, string entryId, 
            [FromBody] WaitingListEntry entry)
        {
            if (!FindAmbulance(ambulanceId, out var ambulance))
            {
                return NotFound();
            }
            var existing = ambulance.WaitingList.FirstOrDefault(e => e.Id == entryId);
            if (entry == null)
            {
                return NotFound();
            }
            ambulance.WaitingList.Remove(existing);
            ambulance.WaitingList.Add(entry);
            myDataRepository.UpsertAmbulanceData(ambulanceId, ambulance);
            return Ok("Entry successfully updated");
        }

        private bool FindAmbulance(string ambulanceId, out Ambulance ambulance)
        {
            ambulance = myDataRepository.GetAmbulanceData(ambulanceId);
            return ambulance != null;
        }
    }
}