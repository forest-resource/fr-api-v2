namespace fr.Service.Model.Icons
{
    public class IconModel
    {
        public Guid Id { get; set; }
        public string IconName { get; set; }
        public string IconData { get; set; }
        public string TreeCommonNameUsed { get; set; }
        public string TreeScienceNameUsed { get; set; }
        public string LastUpdatedBy { get; set; }
        public DateTime LastUpdatedTime { get; set; }
    }
}
