////////using Modasyafasi.Webapp.Service.IoC;

////////var builder = WebApplication.CreateBuilder(args);
////////ServiceContainer.RegisterServices(builder.Services);
////////// Add services to the container.
////////builder.Services.AddControllersWithViews();

////////var app = builder.Build();

////////// Configure the HTTP request pipeline.
////////if (!app.Environment.IsDevelopment())
////////{
////////    app.UseExceptionHandler("/Home/Error");
////////    app.UseHsts();
////////}

////////app.UseHttpsRedirection();
////////app.UseStaticFiles();

////////app.UseRouting();

////////app.UseAuthorization();

////////app.MapControllerRoute(
////////    name: "default",
////////    pattern: "{controller=Home}/{action=Index}/{id?}");

////////app.Run();

using Modasyafasi.Webapp.Service.IoC;
using Microsoft.AspNetCore.Builder;
using Modasyafasi.Webapp.Models.Interface;
using Modasyafasi.Webapp.Service;

var builder = WebApplication.CreateBuilder(args);


ServiceContainer.RegisterServices(builder.Services);


builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});


builder.Services.AddControllersWithViews();
builder.Services.AddAuthentication("MyCookieAuth").AddCookie("MyCookieAuth", options =>
{
    options.LoginPath = "/Home/Login";
});


builder.Services.AddHttpClient<IKullaniciService, KullaniciService>(client =>
{
    client.BaseAddress = new Uri("https://localhost:44397/");
});
builder.Services.ConfigureApplicationCookie(options =>
{
    options.Events.OnRedirectToLogin = context =>
    {
        // API isteði ise 401 dön
        if (context.Request.Path.StartsWithSegments("/Home") &&
            context.Request.Headers["X-Requested-With"] == "XMLHttpRequest")
        {
            context.Response.StatusCode = 401;
        }
        else
        {
            context.Response.Redirect(context.RedirectUri);
        }

        return Task.CompletedTask;
    };
});


var app = builder.Build();


if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}


app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();


app.UseCors("AllowAll");

app.UseAuthentication();
app.UseAuthorization();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Login}/{id?}");

app.Run();
