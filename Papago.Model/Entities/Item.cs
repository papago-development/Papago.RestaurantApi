using System.Collections.Generic;
using Newtonsoft.Json;
using Papago.Model.BaseEntities;

namespace Papago.Model.Entities
{
    public class Item : NamedEntity
    {
        public int RestaurantId { get; set; }
        public int? CategoryId { get; set; }
        public string LogoUrl { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }

        public virtual Restaurant Restaurant { get; set; }
        public virtual Category Category { get; set; }

        [JsonIgnore]
        public virtual ICollection<MenuItem> MenuItems { get; set; } = new List<MenuItem>();
    }
}
