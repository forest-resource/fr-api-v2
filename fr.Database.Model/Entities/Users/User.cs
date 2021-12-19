using fr.Database.Model.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;

namespace fr.Database.Model.Entities.Users
{
    [Index(nameof(StudentCode), IsUnique = true)]
    public class User : IdentityUser<Guid>, IFullModel
    {
        [Required]
        public string StudentCode { get; set; }
        [Required]
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [Required]
        public DateTime DayOfBird { get; set; }

        public DateTime CreatedTime { get; set; }
        public string CreatedBy { get; set; }
        public DateTime UpdatedTime { get; set; }
        public string UpdatedBy { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletedTime { get; set; }
        public string DeletedBy { get; set; }
    }
}
