using System.ComponentModel.DataAnnotations;

namespace fr.Service.Model.Trees
{
    public class TreeCreateAndUpdateModel
    {
        public Guid? Id { get; set; }
        [Required]
        public string CommonName { get; set; }
        [Required]
        public string ScienceName { get; set; }

        public Guid? IconId { get; set; }

        public ICollection<TreeDetailModel> TreeDetails { get; set; }
    }
}
