using fr.Database.Model.Abstracts;
using System.ComponentModel.DataAnnotations;

namespace fr.Database.Model.Entities.Icons
{
    public class Icon : FullEntityModel
    {
        [Required]
        public string IconName { get; set; }
        [Required]
        public string IconData { get; set; }
    }
}
