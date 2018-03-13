using Papago.Model.BaseEntities;

namespace Papago.Model.Entities
{
    public class Order : BaseEntity
    {
        public int RestaurantId { get; set; }
        public int ClientId { get; set; }
        public decimal Price { get; set; }
        /// <summary>
        /// Maybe re-do this as collection Item?
        /// </summary>
        public string Text { get; set; }
        public int? DeliveryTime { get; set; }

        public virtual Restaurant Restaurant { get; set; }

        public virtual Client Client { get; set; }
    }
}
