using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace ambulance_api.Models
{
    [DataContract]
    public class Ambulance
    {
        [DataMember]
        public string Id { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string RoomNumber { get; set; }

        [DataMember]
        public IList<Condition> Conditions { get; set; }

        [DataMember]
        public IList<WaitingListEntry> WaitingList { get; set; }

        /// <summary>
        /// Opening time as string. Example: 08:00
        /// </summary>
        [DataMember]
        public string OpeningTime { get; set; } 

        [JsonIgnore]
        public TimeSpan OpeningTimeSpan 
        {
            get => ParseTimeSpan(OpeningTime);
            set => FormatTimeSpan(value);
        }

        /// <summary>
        /// Closing time as string. Example: 08:00
        /// </summary>
        [DataMember]
        public string ClosingTime { get; set; } 

        [JsonIgnore]
        public TimeSpan ClosingTimeSpan 
        {
            get => ParseTimeSpan(ClosingTime);
            set => FormatTimeSpan(value);
        }

        private TimeSpan ParseTimeSpan(string timeAsString)
        {
            // 09:30
            if (string.IsNullOrWhiteSpace(timeAsString))
            {
                return new TimeSpan(0, 0, 0);
            }
            var segments = timeAsString.Split(":");
            if (segments.Length == 2 && 
                int.TryParse(segments[0], out var hours) && 
                int.TryParse(segments[1], out var minutes))
            {
                return new TimeSpan(hours, minutes, 0);
            }
            return default(TimeSpan); // return null;
        }

        private string FormatTimeSpan(TimeSpan timeSpan)
        {
            // return timeSpan.Hours + ":" + timeSpan.Minutes;
            return $"{timeSpan.Hours}:{timeSpan.Minutes}";
        }
    }
}