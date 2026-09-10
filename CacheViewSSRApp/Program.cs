using CacheViewSSRApp.Components;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents();
builder.Services.AddHttpContextAccessor();
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options => options.LoginPath = "/sign-in");
builder.Services.AddAuthorization();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapStaticAssets();
app.MapPost("/account/sign-in/{userName}", async (string userName, HttpContext context) =>
{
    var displayName = userName.ToLowerInvariant() switch
    {
        "alice" => "Alice",
        "bob" => "Bob",
        _ => null
    };

    if (displayName is null)
    {
        return Results.NotFound();
    }

    var identity = new ClaimsIdentity(
        [new Claim(ClaimTypes.Name, displayName)],
        CookieAuthenticationDefaults.AuthenticationScheme);

    await context.SignInAsync(
        CookieAuthenticationDefaults.AuthenticationScheme,
        new ClaimsPrincipal(identity));

    return Results.Redirect("/cache-view");
});
app.MapPost("/account/sign-out", async (HttpContext context) =>
{
    await context.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
    return Results.Redirect("/sign-in");
});
app.MapRazorComponents<App>();

app.Run();
