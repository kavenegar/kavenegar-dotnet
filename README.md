# Kavenegar .NET SDK

[![NuGet Version](https://img.shields.io/nuget/v/kavenegar.svg?style=flat-square&color=blue)](https://www.nuget.org/packages/kavenegar/)
[![Supported .NET Versions](https://img.shields.io/badge/.NET-10.0%20%7C%208.0%20%7C%206.0%20%7C%20Standard%202.0%20%7C%203.5-blueviolet.svg?style=flat-square)](#)

A high-performance C# client library for integrating **Kavenegar** SMS, OTP Verification, and Voice services into modern or legacy .NET applications.

---

## 🖥️ Supported .NET Versions

The SDK is compiled to support the following target frameworks:

* **⚡ Modern .NET**: `.NET 10.0`, `.NET 8.0`, `.NET 6.0`
* **📦 Standard Integration**: `.NET Standard 2.0`
* **🧱 Legacy Platforms**: `.NET Framework 3.5` (highly compatible with enterprise systems)

---

## ⚡ Features

* **🔄 Native Connection Pooling**: Fully reuses underlying `HttpClient` connections to eliminate socket exhaustion.
* **💉 Dependency Injection Friendly**: Built-in constructors support seamless integration with `AddHttpClient` and typed DI container registrations.
* **📚 Code-Level Intellisense**: Includes detailed XML comments on every API method detailing parameter rules, validation, and types.
* **🌐 Broad Platform Support**: Runs on modern .NET runtimes (.NET 6.0/8.0/10.0) as well as legacy systems (.NET Framework 3.5).

---

## 📦 Installation

Choose your preferred package manager to install:

### Package Manager
```bash
Install-Package kavenegar -Version 1.2.4
```

### .NET CLI
```bash
dotnet add package kavenegar --version 1.2.4
```

---

## 🛠️ Usage

### A. Simple Send SMS Example
Here is the default example of sending a simple SMS message:

```csharp
try
{
	var api = new Kavenegar.KavenegarApi("Your Api Key");
	var result = api.Send("sender", "receptor", "خدمات پیام کوتاه کاوه نگار");
	foreach (var r in result)
	{
		Console.WriteLine(r.Messageid.ToString());
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

### B. Modern Dependency Injection Registration (.NET 6 / 8 / 10)
For ASP.NET Core applications, register the client as a Typed Client to pool connections:

```csharp
using Kavenegar;

// Program.cs
builder.Services.AddHttpClient<IKavenegarApi, KavenegarApi>()
    .AddTypedClient((httpClient, sp) => 
    {
        var apiKey = builder.Configuration["Kavenegar:ApiKey"];
        return new KavenegarApi(apiKey, httpClient);
    });
```

---

## 🤝 Contributing

Bug fixes, docs, and enhancements are welcome! Please let us know by sending an email to [support@kavenegar.com](mailto:support@kavenegar.com?Subject=SDK).



<div dir='rtl'>

## 🇮🇷 راهنما

### معرفی سرویس کاوه نگار

کاوه نگار یک وب سرویس ارسال و دریافت پیامک و تماس صوتی است که به راحتی میتوانید از آن استفاده نمایید.

### ساخت حساب کاربری

اگر در وب سرویس کاوه نگار عضو نیستید میتوانید از [لینک عضویت](http://panel.kavenegar.com/client/membership/register) ثبت نام  و اکانت آزمایشی برای تست API دریافت نمایید.

### مستندات

برای مشاهده اطلاعات کامل مستندات [وب سرویس پیامک](http://kavenegar.com/وب-سرویس-پیامک.html)  به صفحه [مستندات وب سرویس](http://kavenegar.com/rest.html) مراجعه نمایید.

### راهنمای فارسی

در صورتی که مایل هستید راهنمای فارسی کیت توسعه کاوه نگار را مطالعه کنید به صفحه [کد ارسال پیامک](http://kavenegar.com/sdk.html) مراجعه نمایید.

### اطلاعات بیشتر
برای مطالعه بیشتر به صفحه معرفی [وب سرویس اس ام اس ](http://kavenegar.com) کاوه نگار مراجعه نمایید .

اگر در استفاده از کیت های سرویس کاوه نگار مشکلی یا پیشنهادی داشتید ما را با یک Pull Request یا ارسال ایمیل به support@kavenegar.com خوشحال کنید.

##

[http://kavenegar.com](http://kavenegar.com)	

</div>
