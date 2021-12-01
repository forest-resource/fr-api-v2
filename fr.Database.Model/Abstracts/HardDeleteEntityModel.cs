using fr.Database.Model.Interfaces;
using System;
using System.ComponentModel.DataAnnotations;

namespace fr.Database.Model.Abstracts
{
    public abstract class HardDeleteEntityModel : IKeyModel, ICreatedModel, IUpdatedModel
    {
        [Key]
        [Required]
        public Guid Id { get; set; }

        [Required]
        public DateTime CreatedTime { get; set; }

        [Required]
        public string CreatedBy { get; set; }

        [Required]
        public DateTime UpdatedTime { get; set; }

        [Required]
        public string UpdatedBy { get; set; }
    }
}
