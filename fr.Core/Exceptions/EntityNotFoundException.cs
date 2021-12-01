namespace fr.Core.Exceptions
{
    public class EntityNotFoundException : AppException
    {
        public EntityNotFoundException(string message) : base(404, "NOTFOUND", message)
        {
        }
    }
}
