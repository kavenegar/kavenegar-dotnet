namespace Kavenegar.Exceptions
{
    public class HttpException : KavenegarException
    {
        public HttpException(string message, int code)
            : base(message)
        {
            Code = code;
        }

        public int Code { get; private set; }
    }
}
