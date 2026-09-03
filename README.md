# SolonProject

راه‌اندازی و اجرای پروژه، به‌همراه توضیح دو مشکلی که قبلاً باعث می‌شد پروژه در Visual Studio اجرا نشود.

---

## ۱) ساختار ریپازیتوری

| پروژه | نوع | توضیح |
|---|---|---|
| `WebApiSolon` | ASP.NET Core Web API (`net10.0`) | وب‌سرویس + Swagger |
| `BlazorAppSolon` | Blazor WebAssembly (`net10.0`) | رابط کاربری |
| `CoreSolon` | Class Library | لایه‌ی هسته |
| `DataLayerSolon` | Class Library | لایه‌ی داده |

فایل سلوشن: **`SolutionSolon.slnx`** (فرمت جدید XML).

---

## ۲) پیش‌نیازها (خیلی مهم)

بدون این دو مورد، پروژه در Visual Studio **باز یا اجرا نمی‌شود**:

1. **.NET 10 SDK**
   همه‌ی پروژه‌ها `<TargetFramework>net10.0</TargetFramework>` هستند.
   دانلود: https://dotnet.microsoft.com/download/dotnet/10.0
   بررسی: `dotnet --version` باید چیزی مثل `10.0.xxx` بدهد.
   > اگر SDK قدیمی‌تر نصب باشد خطای `NETSDK1045: The current .NET SDK does not support targeting .NET 10.0` را می‌گیرید.

2. **Visual Studio 2022 نسخه‌ی 17.13 به بالا (یا Visual Studio 2026)**
   چون فایل سلوشن با فرمت جدید **`.slnx`** ذخیره شده است. نسخه‌های قدیمی‌تر (۱۷.۱۰ و قبل‌تر) این فرمت را نمی‌شناسند و سلوشن را باز نمی‌کنند.
   > راه‌حل جایگزین: VS را به‌روزرسانی کنید، یا اگر مجبورید، در VS جدید از `File > Save Solution As…` یک فایل `.sln` کلاسیک بسازید.

---

## ۳) اجرا

```bash
git clone https://github.com/fatemelakzaei75-blip/SolonProject.git
cd SolonProject
dotnet restore
dotnet build
```

اجرای API با Swagger:

```bash
dotnet run --project WebApiSolon
```

یا در Visual Studio: روی `WebApiSolon` کلیک راست → **Set as Startup Project** → کلید **F5**.

آدرس‌ها:

| آدرس | توضیح |
|---|---|
| `https://localhost:7217/swagger` | رابط کاربری Swagger |
| `https://localhost:7217/scalar` | رابط کاربری Scalar |
| `https://localhost:7217/openapi/v1.json` | سند خام OpenAPI |
| `https://localhost:7217/weatherforecast` | تست کنترلر |

برای Blazor هم `BlazorAppSolon` را به‌عنوان Startup Project انتخاب کنید (`https://localhost:7093`).

> نکته: Swagger و OpenAPI فقط در محیط **Development** فعال هستند (`if (app.Environment.IsDevelopment())`). اگر با محیط Production اجرا بگیرید، صفحه‌ی Swagger بالا نمی‌آید.

---

## ۴) دو مشکلی که قبلاً وجود داشت

### مشکل اول: هیچ کدی در Git نبود، فقط یک فایل zip
ریپازیتوری فقط شامل یک فایل `ProjectS.zip` بود (کامیت `9a8a177 - Add files via upload`). یعنی وقتی با **Clone Repository** پروژه را می‌گرفتید، فقط یک فایل فشرده دانلود می‌شد و هیچ `.slnx` / `.csproj` / فایل کدی وجود نداشت؛ به همین دلیل Visual Studio چیزی برای اجرا پیدا نمی‌کرد.
الان کل پروژه به‌صورت فایل‌های واقعی در ریشه‌ی ریپو قرار گرفته و `ProjectS.zip` حذف شده است. فولدرهای `bin`، `obj` و `.vs` هم که نباید در Git باشند با `.gitignore` حذف/نادیده گرفته شدند.

### مشکل دوم: Swagger اصلاً نصب نبود
قالب Web API در **.NET 9 و بعد از آن** دیگر Swashbuckle را به‌صورت پیش‌فرض ندارد؛ به‌جایش پکیج `Microsoft.AspNetCore.OpenApi` آمده که **فقط سند JSON را تولید می‌کند و هیچ رابط کاربری ندارد**. پس `/swagger` وجود نداشت و خطای ۴۰۴ می‌داد — این باگ پروژه نبود، طراحی جدید مایکروسافت است.

کاری که انجام شد (`WebApiSolon/Program.cs` و `WebApiSolon.csproj`):

```csharp
builder.Services.AddOpenApi();      // تولید سند OpenAPI

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();               // -> /openapi/v1.json

    app.UseSwaggerUI(options =>     // -> /swagger
    {
        options.SwaggerEndpoint("/openapi/v1.json", "WebApiSolon v1");
    });

    app.MapScalarApiReference();    // -> /scalar (اختیاری)
}
```

پکیج‌های اضافه‌شده:
- `Swashbuckle.AspNetCore.SwaggerUI` → فقط رابط کاربری Swagger (نه تولید سند)
- `Scalar.AspNetCore` → رابط کاربری مدرن‌تر در `/scalar` (اختیاری؛ اگر نمی‌خواهید، پکیج و خط `MapScalarApiReference()` را حذف کنید)

همچنین در `WebApiSolon/Properties/launchSettings.json` مقدار `launchBrowser` روی `true` و `launchUrl` روی `swagger` تنظیم شد تا با F5 مرورگر مستقیماً روی صفحه‌ی Swagger باز شود (قبلاً `launchBrowser: false` بود و هیچ صفحه‌ای باز نمی‌شد).

---

## ۵) اگر هنوز اجرا نشد

| خطا / علامت | علت | راه‌حل |
|---|---|---|
| `NETSDK1045` | .NET 10 SDK نصب نیست | نصب SDK 10 از سایت مایکروسافت |
| سلوشن باز نمی‌شود | VS قدیمی‌تر از 17.13 | به‌روزرسانی Visual Studio |
| `/swagger` خطای ۴۰۴ | محیط Production یا `MapOpenApi` صدا زده نشده | با profile `https`/`http` در حالت Development اجرا کنید |
| `NU1101` هنگام restore | منبع NuGet در دسترس نیست / پکیج‌ها کش نشده‌اند | اتصال به nuget.org و اجرای `dotnet restore` |
| خطای گواهی HTTPS | dev-certificate نصب نیست | `dotnet dev-certs https --trust` |
