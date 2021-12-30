using System.ComponentModel.DataAnnotations;

namespace fr.Service.Model.Icons
{
    public class IconCreateUpdateModel
    {
        public Guid? Id { get; set; }

        [Required]
        public string IconName { get; set; }

        [Required]
        public string IconData { get; set; }
    }
}
