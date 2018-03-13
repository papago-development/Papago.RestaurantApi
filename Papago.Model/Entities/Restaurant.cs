using System.Collections.Generic;
using Newtonsoft.Json;
using Papago.Model.BaseEntities;

namespace Papago.Model.Entities
{
    public class Restaurant : NamedEntity
    {
        public string Location { get; set; }
        public string Address { get; set; }
        public string Description { get; set; }
        public string LogoUrl { get; set; }
        public bool HasOwnDelivery { get; set; }
        public string WorkingHours { get; set; }
        public decimal MinimumOrderValue { get; set; }
        public short AverageDeliveryDuration { get; set; }
        public decimal Rating { get; set; }

        [JsonIgnore]
        public virtual ICollection<Category> Categories { get; set; } = new List<Category>();
        [JsonIgnore]
        public virtual ICollection<Item> Items { get; set; } = new List<Item>();
        [JsonIgnore]
        public virtual ICollection<Menu> Menus { get; set; } = new List<Menu>();
        [JsonIgnore]
        public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
    }
}
