using System;

namespace fr.Database.Model.Interfaces
{
    public interface IUpdatedModel
    {
        public DateTime UpdatedTime { get; set; }
        public string UpdatedBy { get; set; }
    }
}
