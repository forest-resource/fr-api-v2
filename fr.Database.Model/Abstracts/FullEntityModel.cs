using fr.Database.Model.Interfaces;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace fr.Database.Model.Abstracts
{
    public abstract class FullEntityModel : IFullModel
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Required]
        public DateTime CreatedTime { get; set; }

        [Required]
        public string CreatedBy { get; set; }

        [Required]
        public DateTime UpdatedTime { get; set; }

        [Required]
        public string UpdatedBy { get; set; }

        [Required]
        public bool IsDeleted { get; set; }

        public DateTime? DeletedTime { get; set; }
        public string DeletedBy { get; set; }

        [NotMapped]
        public static readonly string[] AudittingProps = new[]
        {
            nameof(Id),
            nameof(CreatedTime),
            nameof(CreatedBy),
            nameof(UpdatedTime),
            nameof(UpdatedBy),
            nameof(IsDeleted),
            nameof(DeletedTime),
            nameof(DeletedBy)
        };
    }
}
