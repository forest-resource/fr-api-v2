namespace fr.Service.Model.Trees
{
    public class TreeModel
    {
        public Guid Id { get; set; }
        public string CommonName { get; set; }
        public string ScienceName { get; set; }
        public string Family { get; set; }
        public Guid? IconId { get; set; }
        public string? IconData { get; set; }

        public ICollection<TreeDetailModel> TreeDetails{ get; set; }
    }
}
