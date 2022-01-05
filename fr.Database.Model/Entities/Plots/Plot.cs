using fr.Database.Model.Abstracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace fr.Database.Model.Entities.Plots
{
    [Table(nameof(Plot), Schema = "Plot")]
    [Index(nameof(PlotName), IsUnique = true)]
    public class Plot : FullEntityModel
    {
        private ICollection<PlotPoint> plotPoints;
        private readonly ILazyLoader loader;

        public Plot()
        {

        }

        public Plot(ILazyLoader loader)
        {
            this.loader = loader;
        }

        [Required]
        public string PlotName { get; set; }
        [Required]
        public string Title { get; set; }
        public string Subtitle { get; set; }
        public string Description { get; set; }

        [Required]
        public bool IsCurrent { get; set; }

        public ICollection<PlotPoint> PlotPoints
        {
            get => plotPoints ?? loader.Load(this, ref plotPoints);
            set => plotPoints = value;
        }
    }
}
