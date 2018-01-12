using System;

namespace Papago.Model.BaseEntities {
    public abstract class BaseEntity {
        public int Id { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime LastUpdate { get; set; }
        public string UpdatedBy { get; set; }
    }
}