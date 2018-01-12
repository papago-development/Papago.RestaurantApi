using System.Collections.Generic;
using Newtonsoft.Json;
using Papago.Model.BaseEntities;

namespace Papago.Model.Entities
{
    public class Category : NamedEntity
    {
        public int RestaurantId { get; set; }
        public string LogoUrl { get; set; }
        public virtual Restaurant Restaurant { get; set; }

        [JsonIgnore]
        public virtual ICollection<Item> Items { get; set; } = new List<Item>();
    }
}
