using System.Runtime.Serialization;

namespace ambulance_api.Models
{
    /// <summary>
    /// Contract with information about disease or other health problem
    /// </summary>
    [DataContract]
    public class Condition
    {
        [DataMember]
        public string Code { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string Reference { get; set; }
    }
}