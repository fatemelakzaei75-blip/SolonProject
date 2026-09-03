using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// تولید سند OpenAPI توسط خودِ ASP.NET Core (از .NET 9 به بعد Swashbuckle دیگر در قالب پروژه نیست)
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    // سند OpenAPI را در آدرس /openapi/v1.json در دسترس می‌گذارد
    app.MapOpenApi();

    // رابط کاربری Swagger در آدرس /swagger
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/openapi/v1.json", "WebApiSolon v1");
    });

    // رابط کاربری Scalar در آدرس /scalar (اختیاری - اگر لازم ندارید حذفش کنید)
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
