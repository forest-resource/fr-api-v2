using System;

namespace fr.Database.Model.Interfaces
{
    public interface ICreatedModel
    {
        public DateTime CreatedTime { get; set; }
        public string CreatedBy { get; set; }
    }
}
