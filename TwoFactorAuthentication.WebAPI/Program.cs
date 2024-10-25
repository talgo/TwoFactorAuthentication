using TwoFactorAuthentication.Application.Interfaces;
using TwoFactorAuthentication.Application.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<IRedisService, RedisService>();
builder.Services.AddScoped<ITwoFactorAuthenticationEmail, TwoFactorAuthenticationEmail>();
builder.Services.AddScoped<ITwoFactorAuthenticationSms, TwoFactorAuthenticationSms>();
builder.Services.AddScoped<ITwoFactorAuthentication, TwoFactorAuthentication.Application.Services.TwoFactorAuthentication>();
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddScoped<ISmsService, SmsService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
