#Dotnet SDK
# <a href="http://kavenegar.com/rest.html">Kavenegar RESTful API Document</a>
If you need to future information about API document Please visit RESTful Document

## Installation
<p>
First of all, You need to make an account on Kavenegar from <a href="https://panel.kavenegar.com/Client/Membership/Register">Here</a>
</p>
<p>
After that you just need to pick API-KEY up from <a href="http://panel.kavenegar.com/Client/setting/index">My Account</a> section.
You can download the C# SDK <a href="https://raw.githubusercontent.com/KaveNegar/kavenegar-csharp/master/Kavenegar/bin/Debug/Kavenegar.dll">Here</a> or just pull it.
Anyway there is good tutorial about <a href="http://gun.io/blog/how-to-github-fork-branch-and-pull-request/">Pull  request</a>
</p>

## Usage
Well, There is an example to Send SMS by C#.

```c#
try
{
	Kavenegar.KavenegarApi api = new Kavenegar.KavenegarApi("Your Api Key");
	var result = api.Send("SenderLine", "Your Receptor", "خدمات پیام کوتاه کاوه نگار");
	foreach (var r in result){
	  Console.Write("r.Messageid.ToString()");
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

#Contribution
Bug fixes, docs, and enhancements welcome! Please let us know <a href="mailto:support@kavenegar.com?Subject=SDK" target="_top">support@kavenegar.com</a>
<hr>

<div dir='rtl'>

<h4 id="">راهنمای فارسی</h4>
<h5 id="-1">راهنما</h5>
<p>در صورتی که مایل هستید راهنمای فارسی کیت توسعه کاوه نگار را مطالعه کنید به صفحه
<a href="http://kavenegar.com/sdk.html">کد ارسال پیامک</a> 
مراجعه کنید.</p>
<h5 id="-2">مستندات</h5>
<p>برای مطالعه مستندات کار با
<a href="http://kavenegar.com"> وب سرویس اس ام اس</a>
کاوه نگار به صفحه <a href="http://kavenegar.com/rest.html">مستندات</a>مراجعه کنید</p>
<h5 id="-3">معرفی وب سرویس کاوه نگار</h5>
<p>برای مشاهده ویژگی های وب سرویس پیامک کاوه نگار به <a href="http://kavenegar.com/%D9%88%D8%A8%D8%B3%D8%B1%D9%88%DB%8C%D8%B3-%D9%BE%DB%8C%D8%A7%D9%85%DA%A9.html">صفحه  وب سرویس</a>مراجعه نمائید.</p>
<h5 id="-4">ایجاد حساب کاربری</h5>
<p>و بالاخره اگر در استفاده از سرویس کاوه نگار مشکلی داشتید یا پیشنهاد همکاری  بود لطفا حتما به ما اطلاع دهید.</p>
<p><a href="mailto:support@kavenegar.com">support@kavenegar.com</a></p>
</div>
r.com">support@kavenegar.com</a>

</p>
</div>
