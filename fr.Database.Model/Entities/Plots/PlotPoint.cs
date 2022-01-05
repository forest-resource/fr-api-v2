using fr.Database.Model.Abstracts;
using fr.Database.Model.Entities.Trees;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace fr.Database.Model.Entities.Plots
{
    [Table(nameof(PlotPoint), Schema = "Plot")]
    public class PlotPoint : HardDeleteEntityModel
    {
        private Plot plot;
        private Tree tree;
        private readonly ILazyLoader loader;

        public PlotPoint()
        {

        }
        public PlotPoint(ILazyLoader loader)
        {
            this.loader = loader;
        }

        [Required]
        public string Sub { get; set; }
        [Required]
        public string Code { get; set; }
        [Required]
        public double X { get; set; }
        [Required]
        public double Y { get; set; }
        [Required]
        public double Diameter { get; set; }
        [Required]
        public double Height { get; set; }

        [Required]
        public Guid PlotId { get; set; }
        [Required]
        public Guid TreeId { get; set; }

        public virtual Plot Plot
        {
            get => plot ?? loader.Load(this, ref plot);
            set => plot = value;
        }
        public virtual Tree Tree
        {
            get => tree ?? loader.Load(this, ref tree);
            set => tree = value;
        }
    }
}
