using System.Collections.Generic;
using Newtonsoft.Json;
using Papago.Model.BaseEntities;

namespace Papago.Model.Entities
{
    public class Client : NamedEntity
    {
        public string Address { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string Email { get; set; }

        [JsonIgnore]
        public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
    }
}
