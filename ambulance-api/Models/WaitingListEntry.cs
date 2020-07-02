using System;
using System.Runtime.Serialization;

namespace ambulance_api.Models
{
    [DataContract]
    public class WaitingListEntry
    {
        [DataMember]
        public string Id { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string PatientId { get; set; }

        /// <summary>
        /// Time when patient entered the waiting room
        /// </summary>
        [DataMember]
        public DateTime Since { get; set; }

        /// <summary>
        /// Estimated time when patient goes into ambulance
        /// </summary>
        [DataMember]
        public DateTime Estimated { get; set; }

        [DataMember]
        public int? EstimatedDurationMinutes { get; set; }

        [DataMember]
        public Condition Condition { get; set; }
    }
}