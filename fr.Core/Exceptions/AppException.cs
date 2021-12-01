using System;

namespace fr.Core.Exceptions
{
    public class AppException : Exception
    {
        public int Status { get; internal set; }
        public string Code { get; internal set; }

        public AppException(int status, string code, string message) : base(message)
        {
            Status = status;
            Code = code;
        }

        public AppException(string message): base(message) => Code = $"{Status = -1}";
    }
}
