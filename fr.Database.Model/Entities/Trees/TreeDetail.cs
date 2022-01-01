using fr.Database.Model.Abstracts;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace fr.Database.Model.Entities.Trees
{
    [Table(nameof(TreeDetail), Schema = "Tree")]
    public class TreeDetail : HardDeleteEntityModel
    {
        private Tree tree;
        private readonly ILazyLoader loader;

        public TreeDetail()
        {

        }
        public TreeDetail(ILazyLoader loader)
        {
            this.loader = loader;
        }

        [Required]
        public string Key { get; set; }
        [Required]
        public string Value { get; set; }

        public Guid TreeId { get; set; }
        public virtual Tree Tree
        {
            get => tree ?? loader.Load(this, ref tree);
            set => tree = value;
        }
    }
}
