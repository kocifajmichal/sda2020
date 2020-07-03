using System;
using System.Linq;
using System.Collections.Generic;

namespace ambulance_api.Models
{
    public static class AmbulanceExtension
    {
        public static Ambulance EstimateAndSortWaitingList(this Ambulance ambulance)
        {
            if (ambulance.WaitingList == null)
            {
                ambulance.WaitingList = new List<WaitingListEntry>();
            }

            ambulance.WaitingList = ambulance.WaitingList.OrderBy(entry => entry.Since).ToList();

            var lastLeaveTime = DateTime.Today + ambulance.OpeningTimeSpan;
            foreach (var entry in ambulance.WaitingList)
            {
                if (entry.Estimated - DateTime.Today < ambulance.OpeningTimeSpan)
                {
                    entry.Estimated = DateTime.Today + ambulance.OpeningTimeSpan;
                }

                if (entry.Estimated < entry.Since)
                {
                    entry.Estimated = entry.Since;
                }

                if (entry.Estimated < lastLeaveTime)
                {
                    entry.Estimated = lastLeaveTime;
                }

                lastLeaveTime = entry.Estimated + TimeSpan.FromMinutes(entry.EstimatedDurationMinutes ?? 15);

                // entry.Estimated = 3. 7. 2020 7:30
                // DateTime.Today = 3. 7. 2020 00:00:00
                // entry.Estimated - DateTime.Today = 7:30
                // ambulance.OpeningTimeSpan = 8:00
                // DateTime.Today + ambulance.OpeningTimeSpan = 3. 7. 2020 8:00
            }
            return ambulance;
        }
    }
}