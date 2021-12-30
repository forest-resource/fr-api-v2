using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace fr.Service.Model.Icons
{
    public class IconModel
    {
        public Guid Id { get; set; }
        public string IconName { get; set; }
        public string IconData { get; set; }
        public string LastUpdatedBy { get; set; }
        public DateTime LastUpdatedTime { get; set; }
    }
}
