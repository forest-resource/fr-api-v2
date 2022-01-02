using fr.Service.Model.Trees;
using System.ComponentModel.DataAnnotations;

namespace fr.Service.Model.Plots
{
    public class PlotPointModel
    {
        public Guid? Id { get; set; }
        [Required]
        public double X { get; set; }
        [Required]
        public double Y { get; set; }

        public TreeModel Tree { get; set; }
    }
}
