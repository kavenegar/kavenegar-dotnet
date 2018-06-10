namespace Kavenegar.Exceptions
{
 public class HttpException : KavenegarException
 {
	private readonly int _code;
	public HttpException(string message, int code)
	 : base(message)
	{
	 _code = code;
	}

	public int Code
	{
	 get { return _code; }
	}
 }
}