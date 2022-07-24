using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.DataProtection;

var builder = WebApplication.CreateBuilder(args);

var directoryInfo = new DirectoryInfo(Path.Combine("C:", "secret-storage"));
builder.Services.AddDataProtection()
    .PersistKeysToFileSystem(directoryInfo)
    .SetApplicationName("unique");

builder.Services.AddAuthorization();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme);

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

app.MapGet("/", () => "Hello world from client api");

app.MapGet("/protected", () => "Secret from client api").RequireAuthorization();


app.Run();
