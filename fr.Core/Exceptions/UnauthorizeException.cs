namespace fr.Core.Exceptions
{
    public class UnauthorizeException : AppException
    {
        public UnauthorizeException() : base(401, "UNAUTHORIZE", "Unauthorize") { }
    }
}
