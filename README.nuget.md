# Kavenegar C# / .NET SDK

The official .NET client library for the [Kavenegar REST API](https://kavenegar.com). Easily send SMS, bulk messages, voice calls, and verification tokens (OTP) in your .NET applications.

---

## Supported Frameworks

| Target | Compatibility | Async Support |
| :--- | :--- | :---: |
| **.NET** | 10.0, 8.0, 6.0 | Yes (`async`/`await`) |
| **.NET Standard** | 2.0+ (.NET Core 2.0+, .NET 5+) | Yes (`async`/`await`) |
| **.NET Framework** | 3.5+ | Synchronous |

---

## Installation

### .NET CLI
```bash
dotnet add package Kavenegar
```

### Package Manager (Visual Studio Console)
```powershell
Install-Package Kavenegar
```

### PackageReference
```xml
<PackageReference Include="Kavenegar" Version="2.0.1" />
```

---

## Quickstart

### 1. Send SMS (Asynchronous — Recommended)

For modern .NET applications (.NET 6 / 8 / 10):

```csharp
using System;
using System.Threading.Tasks;
using Kavenegar;
using Kavenegar.Exceptions;

class Program
{
    static async Task Main(string[] args)
    {
        var api = new KavenegarApi("YOUR_API_KEY");

        try
        {
            var result = await api.SendAsync("1000100055", "09120000000", "Hello from Kavenegar!");
            Console.WriteLine($"Message sent successfully! ID: {result.Messageid}");
        }
        catch (ApiException ex)
        {
            // API returned a non-success status code
            Console.WriteLine($"API Error [{ex.Code}]: {ex.Message}");
        }
        catch (HttpException ex)
        {
            // Network / connectivity issue
            Console.WriteLine($"HTTP Error [{ex.Code}]: {ex.Message}");
        }
    }
}
```

### 2. Send SMS (Synchronous)

Compatible with all frameworks, including legacy .NET Framework 3.5:

```csharp
using System;
using Kavenegar;

var api = new KavenegarApi("YOUR_API_KEY");
var result = api.Send("1000100055", "09120000000", "Hello from Kavenegar!");
Console.WriteLine($"Message ID: {result.Messageid}, Status: {result.Status}");
```

### 3. Send Verification Code (Lookup / OTP)

Send instant OTP tokens using predefined Kavenegar templates:

```csharp
// Async (Recommended)
var result = await api.VerifyLookupAsync(
    receptor: "09120000000",
    token: "12345",
    template: "verify"
);

Console.WriteLine($"Verification SMS sent. ID: {result.Messageid}");
```

---

## ASP.NET Core & Dependency Injection

Integrate cleanly with ASP.NET Core using `AddHttpClient` to benefit from automatic connection pooling and HTTP lifecycle management:

### 1. Configure in `appsettings.json`
```json
{
  "Kavenegar": {
    "ApiKey": "YOUR_API_KEY"
  }
}
```

### 2. Register Service in `Program.cs`
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

### 3. Inject in Controllers or Services
```csharp
public class AuthService
{
    private readonly IKavenegarApi _kavenegar;

    public AuthService(IKavenegarApi kavenegar)
    {
        _kavenegar = kavenegar;
    }

    public async Task SendLoginOtp(string phoneNumber, string code)
    {
        await _kavenegar.VerifyLookupAsync(phoneNumber, code, "login-otp");
    }
}
```

---

## Error Handling

The SDK provides specific exception types for precise error handling:

* **`Kavenegar.Exceptions.ApiException`**: Raised when the Kavenegar API rejects a request (e.g. invalid sender number, insufficient balance, invalid receptor). Contains `Code` and `Message`.
* **`Kavenegar.Exceptions.HttpException`**: Raised when a network or HTTP connection problem occurs before reaching the API service.

---

## Resources & Documentation

* **Official Website:** [kavenegar.com](https://kavenegar.com)
* **REST API Documentation:** [kavenegar.com/rest.html](https://kavenegar.com/rest.html)
* **GitHub Repository:** [github.com/kavenegar/kavenegar-dotnet](https://github.com/kavenegar/kavenegar-dotnet)
* **Report Issues:** [github.com/kavenegar/kavenegar-dotnet/issues](https://github.com/kavenegar/kavenegar-dotnet/issues)
* **Support Email:** [support@kavenegar.com](mailto:support@kavenegar.com)
