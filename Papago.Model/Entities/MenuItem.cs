using Papago.Model.BaseEntities;

namespace Papago.Model.Entities
{
    public class MenuItem : BaseEntity
    {
        public int MenuId { get; set; }
        public int ItemId { get; set; }

        public virtual Menu Menu { get; set; }
        public virtual Item Item { get; set; }
    }
}
