namespace fr.AppServer.Models
{
    public abstract class ResponseModel
    {
        public int Status { get; set; }
        public string Code { get; set; }
    }
}
