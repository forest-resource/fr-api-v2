using fr.Database.Model.Abstracts;
using fr.Database.Model.Entities.Icons;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace fr.Database.Model.Entities.Trees
{
    [Table(nameof(Tree), Schema = "Tree")]
    public class Tree : FullEntityModel
    {
        private Icon icon;
        private ICollection<TreeDetail> treeDetails;
        private readonly ILazyLoader loader;

        public Tree()
        {

        }
        public Tree(ILazyLoader lazyLoader)
        {
            loader = lazyLoader;
        }

        [Required]
        public string CommonName { get; set; }
        public string ScienceName { get; set; }
        public Guid? IconId { get; set; }
        public virtual Icon Icon
        {
            get => icon ?? loader.Load(this, ref icon);
            set => icon = value;
        }
        public virtual ICollection<TreeDetail> TreeDetails
        {
            get => treeDetails ?? loader.Load(this, ref treeDetails);
            set => treeDetails = value;
        }
    }
}
