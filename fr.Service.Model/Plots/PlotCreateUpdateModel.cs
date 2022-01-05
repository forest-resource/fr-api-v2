using System.ComponentModel.DataAnnotations;

namespace fr.Service.Model.Plots
{
    public class PlotCreateUpdateModel
    {
        public Guid? Id { get; set; }

        [Required]
        public string Name { get; set; }
        [Required]
        public string Title { get; set; }
        public string? Subtitle { get; set; }
        public string? Description { get; set; }
        [Required]
        public bool IsCurrent { get; set; }
        [Required]
        public IEnumerable<PlotPointModel> PlotPoints { get; set; }
    }
}
