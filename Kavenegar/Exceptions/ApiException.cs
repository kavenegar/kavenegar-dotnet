using Kavenegar.Models.Enums;

namespace Kavenegar.Exceptions
{
    public class ApiException : KavenegarException
    {
        public ApiException(string message, int code)
            : base(message)
        {
            Code = (MetaCode)code;
        }

        public MetaCode Code { get; private set; }
    }
}
