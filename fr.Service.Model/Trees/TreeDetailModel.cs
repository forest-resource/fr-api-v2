using System.ComponentModel.DataAnnotations;

namespace fr.Service.Model.Trees
{
    public class TreeDetailModel
    {
        public Guid? Id { get; set; }

        [Required]
        public string Key { get; set; }
        [Required]
        public string Value { get; set; }
    }
}
