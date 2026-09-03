# SolonProject

پروژه‌ی سالن زیبایی — **ASP.NET Core Web API (.NET 8)** + **Blazor WebAssembly (.NET 8)** + **EF Core 8 (SQL Server)**.

---

## ۱) ساختار سلوشن

فایل سلوشن: **`SolutionSolon.sln`** (فرمت کلاسیک — با همه‌ی نسخه‌های Visual Studio باز می‌شود).

| پروژه | نوع | توضیح |
|---|---|---|
| `WebApiSolon` | ASP.NET Core Web API (`net8.0`) | وب‌سرویس + Swagger + ثبت `DatabaseContext` در DI |
| `BlazorAppSolon` | Blazor WebAssembly (`net8.0`) | رابط کاربری |
| `DataLayer` | Class Library (`net8.0`) | `DatabaseContext`، ۱۶ موجودیت `Tbl_*` و Migration |
| `DataCore` | Class Library (`net8.0`) | (فعلاً فقط `Class1.cs`) |

پکیج‌های اصلی:

- `Swashbuckle.AspNetCore` **6.6.2** → Swagger UI در `/swagger`
- `Microsoft.EntityFrameworkCore` / `.SqlServer` / `.Design` / `.Tools` **8.0.0**
- `Microsoft.AspNetCore.Components.WebAssembly` **8.0.14**

---

## ۲) پیش‌نیازها

1. **.NET 8 SDK** → https://dotnet.microsoft.com/download/dotnet/8.0
   بررسی: `dotnet --version` باید `8.0.xxx` بدهد.
2. **Visual Studio 2022 (17.8 به بالا)** — چون سلوشن `.sln` کلاسیک است، نسخه‌های قدیمی هم کار می‌کنند.
3. **SQL Server** — رشته‌ی اتصال پیش‌فرض به `.\sqlexpress` اشاره دارد:
   ```json
   "DefaultConnection": "Server=.\\sqlexpress;Database=SalonDB;Trusted_Connection=True;TrustServerCertificate=True;"
   ```
   اگر SQL Server شما instance پیش‌فرض است، `Server=.` یا `Server=localhost` بگذارید.

---

## ۳) اجرا

```bash
git clone https://github.com/fatemelakzaei75-blip/SolonProject.git
cd SolonProject
dotnet restore
dotnet build
dotnet run --project WebApiSolon
```

در Visual Studio: `WebApiSolon` را **Set as Startup Project** کنید و **F5** بزنید
(یا از پروفایل چندپروژه‌ای `New Profile` که هم Blazor و هم API را با هم بالا می‌آورد).

| آدرس | توضیح |
|---|---|
| `https://localhost:7182/swagger` | Swagger UI |
| `https://localhost:7182/weatherforecast` | تنها کنترلر موجود (قالب پیش‌فرض) |
| `https://localhost:5187` | همان API روی HTTP |

> Swagger فقط در محیط **Development** فعال است (`if (app.Environment.IsDevelopment())`).

---

## ۴) دیتابیس و Migration

`DataLayer/Migrations/20260901203010_CreatTbls.cs` موجود است و در `WebApiSolon/Program.cs`
هنگام start-up برنامه به‌صورت خودکار اجرا می‌شود:

```csharp
using (var scope = app.Services.CreateScope())
{
    scope.ServiceProvider.GetRequiredService<DatabaseContext>().Database.Migrate();
}
```

یعنی **دیتابیس `SalonDB` و جدول‌ها در اولین اجرا خودشان ساخته می‌شوند** (به شرط اینکه SQL Server در دسترس باشد).
اگر SQL Server بالا نباشد، خطا فقط در لاگ نوشته می‌شود و API همچنان بالا می‌آید تا بتوانید Swagger را ببینید.

اجرای دستی Migration (اختیاری):

```bash
dotnet tool install --global dotnet-ef
dotnet ef database update --project DataLayer --startup-project WebApiSolon
```

ساخت Migration جدید:

```bash
dotnet ef migrations add MyNewMigration --project DataLayer --startup-project WebApiSolon
```

---

## ۵) وضعیت فعلی / کارهای باقی‌مانده

- تنها کنترلر موجود `WeatherForecastController` است؛ **هیچ کنترلری برای موجودیت‌های `Tbl_*` نوشته نشده**، پس در Swagger فقط `GET /weatherforecast` دیده می‌شود.
- پروژه‌های `DataCore` و `DataLayer` فقط در API ارجاع شده‌اند؛ `BlazorAppSolon` هنوز به API وصل نیست (صفحه‌ی `Weather.razor` از `wwwroot/sample-data/weather.json` می‌خواند، نه از وب‌سرویس). برای وصل شدن، `BaseAddress` در `BlazorAppSolon/Program.cs` باید روی آدرس API تنظیم شود و API هم `AddCors` بگیرد.
- نسخه‌ی پکیج‌های EF Core `8.0.0` است؛ آخرین نسخه‌ی پچ سری ۸ یعنی `8.0.30` روی nuget.org موجود است و ارتقا به آن توصیه می‌شود (فقط شماره‌ی نسخه در `WebApiSolon.csproj` و `DataLayer.csproj` عوض شود).

---

## ۶) تغییرات اخیر در ریپازیتوری

- فولدرهای `bin`، `obj`، `.vs` و فایل‌های `*.user` که اشتباهی commit شده بودند (۱۴۴۶ فایل از ۱۵۰۵ فایل، حدود ۱۵۰ مگابایت) از Git حذف شدند و `.gitignore` اضافه شد.
  این فایل‌ها خروجی build هستند و نباید در Git باشند؛ هر بار با build دوباره ساخته می‌شوند.
- `ProjectS.zip` و `SolutionSolon.zip` (که قبلاً کل پروژه بودند) حذف شدند؛ الان کد واقعی در ریپو است.
- `Database.Migrate()` هنگام startup اضافه شد.

---

## ۷) اگر اجرا نشد

| خطا / علامت | علت | راه‌حل |
|---|---|---|
| `NETSDK1045` | .NET 8 SDK نصب نیست | نصب SDK 8 |
| `A network-related or instance-specific error ... (26)` یا `Cannot open database "SalonDB"` | SQL Server Express در دسترس نیست / instance اشتباه | سرویس `SQL Server (SQLEXPRESS)` را Start کنید یا `Server=` را اصلاح کنید |
| `Login failed for user ...` | `Trusted_Connection=True` ولی Windows Authentication غیرفعال است | Windows Authentication را در SQL Server فعال کنید یا از user/password استفاده کنید |
| `/swagger` خطای ۴۰۴ | محیط Production | با پروفایل `https` یا `http` (Development) اجرا کنید |
| `NU1101` هنگام restore | عدم دسترسی به nuget.org | اتصال اینترنت / پاک‌کردن کش: `dotnet nuget locals all --clear` |
| خطای گواهی HTTPS | dev-certificate نصب نیست | `dotnet dev-certs https --trust` |
| پروفایل `IIS Express` کار نمی‌کند | فایل `.vs/.../applicationhost.config` از Git حذف شده | یک بار سلوشن را در VS باز کنید تا VS خودش آن را بازسازی کند، یا از پروفایل `https` استفاده کنید |
