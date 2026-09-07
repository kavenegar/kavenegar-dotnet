<div align="center">

<br/>

# 📱 Kavenegar .NET SDK

**A high-performance C# client for SMS, OTP & Voice — built for modern .NET.**

<br/>

[![NuGet](https://img.shields.io/nuget/v/kavenegar.svg?style=for-the-badge&color=5C2D91&logo=nuget&label=NuGet)](https://www.nuget.org/packages/kavenegar/)
[![Downloads](https://img.shields.io/nuget/dt/kavenegar?style=for-the-badge&color=00b4d8&logo=nuget&label=Downloads)](https://www.nuget.org/packages/kavenegar/)
[![.NET](https://img.shields.io/badge/.NET-6%20%7C%208%20%7C%2010-512BD4?style=for-the-badge&logo=dotnet)](https://dotnet.microsoft.com)

<br/>

[📦 Install](#-installation) · [🚀 Quick Start](#-quick-start) · [💉 Dependency Injection](#-aspnet-core--dependency-injection) · [📖 Docs](http://kavenegar.com/rest.html) · [🇮🇷 فارسی](#-راهنما)

<br/>

</div>

---

## ✨ Why Kavenegar SDK?

<table>
<tr>
<td width="50%">

**🔄 Connection Pooling**
Fully reuses `HttpClient` connections — no socket exhaustion, no `new HttpClient()` anti-patterns.

</td>
<td width="50%">

**⚡ Async / Await**
Every method ships with a `*Async` twin for non-blocking, high-throughput workloads.

</td>
</tr>
<tr>
<td>

**💉 DI Ready**
First-class `IKavenegarApi` interface designed to integrate cleanly with `AddHttpClient<>`.

</td>
<td>

**🌐 Broad Compatibility**
Targets `.NET 10 / 8 / 6`, `.NET Standard 2.0`, and `.NET Framework 3.5` — one package.

</td>
</tr>
<tr>
<td>

**📚 IntelliSense Docs**
Full XML documentation on every method — parameters, validation rules, and return types.

</td>
<td>

**🔧 Flexible Config**
Supports direct key, `KavenegarOptions` object, or `IConfiguration` binding — your choice.

</td>
</tr>
</table>

---

## 🖥️ Platform Support

| Framework | Version | Async |
|---|---|:---:|
| **.NET** | 10.0, 8.0, 6.0 | ✅ |
| **.NET Standard** | 2.0 | ✅ |
| **.NET Framework** | 3.5 | ❌ (sync only) |

---

## 📦 Installation

<details open>
<summary><b>🖥️ .NET CLI</b></summary>

```bash
dotnet add package kavenegar
```

</details>

<details>
<summary><b>📦 Package Manager Console</b></summary>

```powershell
Install-Package kavenegar
```

</details>

<details>
<summary><b>🗂️ PackageReference (csproj)</b></summary>

```xml
<PackageReference Include="kavenegar" Version="*" />
```

</details>

---

## 🚀 Quick Start

### Synchronous (all platforms)

```csharp
using Kavenegar;

var api = new KavenegarApi("YOUR_API_KEY");

try
{
    // ── Single recipient ──────────────────────────────────────────────────
    var result = api.Send("SENDER_NUMBER", "09100000001", "Hello from Kavenegar!");
    Console.WriteLine($"✅ Sent  |  ID: {result.Messageid}  |  Status: {result.Status}");

    // ── Multiple recipients ───────────────────────────────────────────────
    var receptors = new List<string> { "09100000001", "09200000002" };
    var results = api.Send("SENDER_NUMBER", receptors, "Bulk SMS via Kavenegar!");
    foreach (var r in results)
        Console.WriteLine($"✅ Sent  |  ID: {r.Messageid}  |  Receptor: {r.Receptor}");
}
catch (Kavenegar.Exceptions.ApiException ex)
{
    // API returned a non-success status code
    Console.WriteLine($"❌ API Error [{ex.Code}]: {ex.Message}");
}
catch (Kavenegar.Exceptions.HttpException ex)
{
    // Network / connectivity failure
    Console.WriteLine($"❌ HTTP Error [{ex.Code}]: {ex.Message}");
}
```

---

### ⚡ Async — Recommended for .NET 6 / 8 / 10

```csharp
using Kavenegar;

var api = new KavenegarApi("YOUR_API_KEY");

try
{
    // ── Single recipient (async) ──────────────────────────────────────────
    var result = await api.SendAsync("SENDER_NUMBER", "09100000001", "Hello async!");
    Console.WriteLine($"✅ Sent  |  ID: {result.Messageid}  |  Cost: {result.Cost}");

    // ── Bulk send (async) ─────────────────────────────────────────────────
    var receptors = new List<string> { "09100000001", "09200000002" };
    var results = await api.SendAsync("SENDER_NUMBER", receptors, "Bulk async message!");
    foreach (var r in results)
        Console.WriteLine($"✅ Sent  |  ID: {r.Messageid}  |  Receptor: {r.Receptor}");
}
catch (Kavenegar.Exceptions.ApiException ex)
{
    Console.WriteLine($"❌ API Error [{ex.Code}]: {ex.Message}");
}
catch (Kavenegar.Exceptions.HttpException ex)
{
    Console.WriteLine($"❌ HTTP Error [{ex.Code}]: {ex.Message}");
}
```

---

## 💉 ASP.NET Core — Dependency Injection

Register once, inject everywhere. Uses `AddHttpClient<>` for built-in connection pooling.

**`appsettings.json`**
```json
{
  "Kavenegar": {
    "ApiKey": "YOUR_API_KEY"
  }
}
```

**`Program.cs`**
```csharp
using Kavenegar;

builder.Services.AddHttpClient<IKavenegarApi, KavenegarApi>((httpClient, sp) =>
{
    var options = sp.GetRequiredService<IConfiguration>()
                    .GetSection("Kavenegar")
                    .Get<KavenegarOptions>();

    return new KavenegarApi(options, httpClient);
});
```

**Usage in any service or controller:**
```csharp
public class SmsService
{
    private readonly IKavenegarApi _sms;

    public SmsService(IKavenegarApi sms) => _sms = sms;

    public async Task SendOtpAsync(string phone, string code)
    {
        var result = await _sms.SendAsync("SENDER_NUMBER", phone, $"Your OTP: {code}");
        Console.WriteLine($"OTP sent — ID: {result.Messageid}");
    }
}
```

---

## ⚙️ Options-Based Configuration

Configure with `KavenegarOptions` — ideal for console apps, background workers, or tests:

```csharp
using Kavenegar;

var api = new KavenegarApi(new KavenegarOptions
{
    ApiKey  = "YOUR_API_KEY",
    BaseUrl = "https://api.kavenegar.com"   // optional — this is the default
});

var result = await api.SendAsync("SENDER_NUMBER", "09100000001", "Hello!");
Console.WriteLine($"Message ID: {result.Messageid}");
```

---

## 🤝 Contributing

Contributions, bug reports, and feature ideas are always welcome!

| | |
|---|---|
| 🐛 **Bug?** | Open an [issue](../../issues) |
| 💡 **Idea?** | Submit a [pull request](../../pulls) |
| 📧 **Help?** | [support@kavenegar.com](mailto:support@kavenegar.com?Subject=SDK) |

---

<br/>

<div dir="rtl">

## 🇮🇷 راهنما

### معرفی سرویس کاوه نگار

کاوه نگار یک وب سرویس ارسال و دریافت پیامک و تماس صوتی است که به راحتی میتوانید از آن استفاده نمایید.

### ساخت حساب کاربری

اگر در وب سرویس کاوه نگار عضو نیستید میتوانید از [لینک عضویت](http://panel.kavenegar.com/client/membership/register) ثبت نام و اکانت آزمایشی برای تست API دریافت نمایید.

### مستندات

برای مشاهده اطلاعات کامل مستندات [وب سرویس پیامک](http://kavenegar.com/وب-سرویس-پیامک.html) به صفحه [مستندات وب سرویس](http://kavenegar.com/rest.html) مراجعه نمایید.

### راهنمای فارسی

در صورتی که مایل هستید راهنمای فارسی کیت توسعه کاوه نگار را مطالعه کنید به صفحه [کد ارسال پیامک](http://kavenegar.com/sdk.html) مراجعه نمایید.

### اطلاعات بیشتر

برای مطالعه بیشتر به صفحه معرفی [وب سرویس اس ام اس](http://kavenegar.com) کاوه نگار مراجعه نمایید.

اگر در استفاده از کیت‌های سرویس کاوه نگار مشکلی یا پیشنهادی داشتید ما را با یک Pull Request یا ارسال ایمیل به [support@kavenegar.com](mailto:support@kavenegar.com) خوشحال کنید.

---

[http://kavenegar.com](http://kavenegar.com)

</div>
