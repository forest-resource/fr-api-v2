namespace fr.Core.Exceptions
{
    public class KeyAlreadyExistsException : AppException
    {
        public KeyAlreadyExistsException(string keyValue)
            : base(409, "DUPLICATE", $"{keyValue} already in the System")
        {

        }
    }
}
