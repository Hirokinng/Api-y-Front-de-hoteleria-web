using HoteleriaApp.Infrastructure.Persistence.Contexto;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();


builder.Services.AddScoped<HoteleriaApp.Core.Application.Interfaces.IClienteRepositorio,
                          HoteleriaApp.Infrastructure.Persistence.Repositories.ClienteRepositorio>();

builder.Services.AddScoped<HoteleriaApp.Core.Application.Interfaces.IClienteServicio,
                          HoteleriaApp.Core.Application.Services.ClienteServicio>();

builder.Services.AddSingleton<HoteleriaApp.Core.Application.Interfaces.IEmailServicio,
                             HoteleriaApp.Infrastructure.Shared.Services.EmailServicio>();

builder.Services.AddDbContext<HoteleriaDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
