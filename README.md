# KavenegarPlus - Compatible with .NET Standard

# <a href="http://kavenegar.com/rest.html">Kavenegar RESTful API Document</a>
If you need information about API document Please visit RESTful Document at <a href="https://kavenegar.com/">Kavenegar</a>

## Installation
<p>
First of all, You need to make an account on Kavenegar from <a href="https://panel.kavenegar.com/Client/Membership/Register">Here</a>
</p>
<p>
After that you just need to pick API-KEY up from <a href="http://panel.kavenegar.com/Client/setting/index">My Account</a> section.

## Installation

With dotnet cli

    dotnet add package KavenegarPlus
----
Or with nuget package manager console
    
    Install-Package KavenegarPlus


We also accept <a href="http://gun.io/blog/how-to-github-fork-branch-and-pull-request/">Pull  Requests</a> .
</p>

## Usage For ASP.NET Core

If you are using `ASP.NET Core` you can configure the services in your `statrtup.cs`.

```c#
public void ConfigureServices(IServiceCollection services)
{
	.
	.
	.
	services.AddKavenegar(settings => settings.ApiKey = "YOUR_API_KEY");
}
```

Then you can use dependency injection to get the service:

```c#
public class MyController : Controller
{
	public IKavenegarApi Api { get; }

	MyController(IKavenegarApi api)
	{
		this.Api = api;
	}

	[HttpGet]
	public async Task<IActionResult> SendSms()
	{
		try
		{
			var result = await this.Api.SendAsync("SenderLine", "Your Receptor", "خدمات پیام کوتاه کاوه نگار");
			foreach (var r in result)
			{
				Console.Write(r.Messageid.ToString());
		    }
			return Ok();
		}
		catch (Kavenegar.Exceptions.ApiException ex) 
		{
			// در صورتی که خروجی وب سرویس 200 نباشد این خطارخ می دهد.
			Console.Write("Message : " + ex.Message);
			return BadRequest();
		}
		catch (Kavenegar.Exceptions.HttpException ex) 
		{
			// در زمانی که مشکلی در برقرای ارتباط با وب سرویس وجود داشته باشد این خطا رخ می دهد
			Console.Write("Message : " + ex.Message);
		}

		return StatusCode(500);
	}
}
```


----

## Usage For .NET Franework ( Anything except .NET Core which supports .NET Standard 2.0)

If you are not using `ASP.NET Core` e.g `ASP.NET Web API` you should pass Options and your own implementation of `System.Net.Http.IHttpClientFactory` to `Kavenegar.KavenegarApi`'s Constructor.

```c#
public class MyHttpClientFactory : System.Net.Http.IHttpClientFactory
{
	public HttpClient CreateClient(string name)
	{
		return new HttpClient();
	}
}
```

```c#
try
{
	var options = Microsoft.Extensions.Options.Options.Create(new KavenegarSettings
	{
		ApiKey = "YOUR_API_KEY"
	});
	Kavenegar.KavenegarApi api = new KavenegarApi(options, new MyHttpClientFactory());

	var result = await api.SendAsync("SenderLine", "Your Receptor", "خدمات پیام کوتاه کاوه نگار");

	foreach (var r in result){
	  Console.Write(r.Messageid.ToString());
  }
}
catch (Kavenegar.Exceptions.ApiException ex) 
{
	// در صورتی که خروجی وب سرویس 200 نباشد این خطارخ می دهد.
	Console.Write("Message : " + ex.Message);
}
catch (Kavenegar.Exceptions.HttpException ex) 
{
	// در زمانی که مشکلی در برقرای ارتباط با وب سرویس وجود داشته باشد این خطا رخ می دهد
	Console.Write("Message : " + ex.Message);
}
```

## Contribution
Bug fixes, docs, and enhancements are welcome! Please let us know <a href="mailto:support@kavenegar.com?Subject=SDK" target="_top">support@kavenegar.com</a>
<hr>
<div dir='rtl'>
	
## راهنما

### معرفی سرویس کاوه نگار

کاوه نگار یک وب سرویس ارسال و دریافت پیامک و تماس صوتی است که به راحتی میتوانید از آن استفاده نمایید.

### ساخت حساب کاربری

اگر در وب سرویس کاوه نگار عضو نیستید میتوانید از [لینک عضویت](http://panel.kavenegar.com/client/membership/register) ثبت نام  و اکانت آزمایشی برای تست API دریافت نمایید.

### مستندات

برای مشاهده اطلاعات کامل مستندات [وب سرویس پیامک](http://kavenegar.com/وب-سرویس-پیامک.html)  به صفحه [مستندات وب سرویس](http://kavenegar.com/rest.html) مراجعه نمایید.

### راهنمای فارسی

در صورتی که مایل هستید راهنمای فارسی کیت توسعه کاوه نگار را مطالعه کنید به صفحه [کد ارسال پیامک](http://kavenegar.com/sdk.html) مراجعه نمایید.

### اطالاعات بیشتر
برای مطالعه بیشتر به صفحه معرفی
[وب سرویس اس ام اس ](http://kavenegar.com)
کاوه نگار
مراجعه نمایید .

 اگر در استفاده از کیت های سرویس کاوه نگار مشکلی یا پیشنهادی  داشتید ما را با یک Pull Request  یا  ارسال ایمیل به support@kavenegar.com  خوشحال کنید.
 
##
![http://kavenegar.com](http://kavenegar.com/public/images/logo.png)		

[http://kavenegar.com](http://kavenegar.com)	

</div>


