using fr.Service.Model.Trees;

namespace fr.Service.Model.Plots
{
    public class PlotModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public string Subtitle { get; set; }
        public string Description { get; set; }
        public bool IsCurrent { get; set; }

        public DateTime LastEdited { get; set; }
        public string By { get; set; }
        
        public ICollection<PlotPointModel> PlotPoints { get; set; }
        public ICollection<TreeModel> Trees { get; set; }
    }
}
