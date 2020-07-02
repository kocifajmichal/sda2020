using System.Collections.Generic;
using System.Runtime.Serialization;

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
    }
}