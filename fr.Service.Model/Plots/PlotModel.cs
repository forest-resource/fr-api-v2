using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fr.Service.Model.Plots
{
    public class PlotModel
    {
        public Guid Id { get; set; }
        public string PlotName { get; set; }
        public string PlotSubName { get; set; }
        public string PlotDescription { get; set; }
        public bool IsCurrent { get; set; }
        
        public ICollection<PlotPointModel> PlotPoint { get; set; }
    }
}
