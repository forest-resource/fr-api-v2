using fr.Database.Model.Interfaces;
using Microsoft.AspNetCore.Identity;
using System;

namespace fr.Database.Model.Entities.Users
{
    public class Users : IdentityUser<Guid>, IFullModel
    {
        public DateTime CreatedTime { get; set; }
        public string CreatedBy { get; set; }
        public DateTime UpdatedTime { get; set; }
        public string UpdatedBy { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletedTime { get; set; }
        public string DeletedBy { get; set; }
    }
}
