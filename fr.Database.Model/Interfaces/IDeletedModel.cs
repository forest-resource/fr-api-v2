using System;

namespace fr.Database.Model.Interfaces
{
    public interface IDeletedModel
    {
        public bool IsDeleted { get; set; }
        public DateTime? DeletedTime { get; set; }
        public string DeletedBy { get; set; }
    }
}
