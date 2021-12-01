namespace fr.AppServer.Models
{
    public class ErrorResponseModel : ResponseModel
    {
        public string Message { get; set; }

        public ErrorModel[] Errors { get; set; }
    }

    public class ErrorModel
    {
        public string Message { get; set; }
    }
}
