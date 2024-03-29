﻿using System.ComponentModel.DataAnnotations;

namespace fr.Service.Model.Plots
{
    public class PlotPointModel
    {
        public Guid? Id { get; set; }
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

        public string CommonName { get; set; }
        public string ScienceName { get; set; }
        public string Family { get; set; }

        [Required]
        public Guid TreeId { get; set; }
    }
}
