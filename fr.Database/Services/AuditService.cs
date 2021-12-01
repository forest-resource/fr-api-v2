namespace fr.Database.Services
{
    public interface IAuditService
    {
        string UserName { get; set; }
    }

    public class AuditService : IAuditService
    {
        public string UserName { get; set; }
    }
}
