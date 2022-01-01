using fr.Database.Model.Abstracts;
using fr.Database.Model.Entities.Trees;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace fr.Database.Model.Entities.Icons
{
    [Table(nameof(Icon), Schema = "Icon")]
    public class Icon : FullEntityModel
    {
        private Tree tree;
        private readonly ILazyLoader loader;

        public Icon()
        {

        }
        public Icon(ILazyLoader loader)
        {
            this.loader = loader;
        }

        [Required]
        public string IconName { get; set; }
        [Required]
        public string IconData { get; set; }

        public virtual Tree Tree
        {
            get => tree ?? loader?.Load(this, ref tree) ?? null;
            set => tree = value;
        }
    }
}
