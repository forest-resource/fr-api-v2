namespace fr.AppServer.Models
{
    public class SuccessResponseModel : ResponseModel
    {
        public object Data;

        public SuccessResponseModel()
        {
            Code = "OK";
        }
    }
}
