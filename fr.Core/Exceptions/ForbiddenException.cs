namespace fr.Core.Exceptions
{
    public class ForbiddenException : AppException
    {
        public ForbiddenException() : base(403, "FORBIDDEN", "Forbidden") { }
    }
}
