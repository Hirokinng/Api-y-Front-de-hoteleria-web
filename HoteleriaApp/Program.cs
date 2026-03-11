using HoteleriaApp.Infrastructure.Persistence.Contexto;
using HoteleriaApp.Infrastructure.Persistence.Repositories;
using HoteleriaApp.Infrastructure.Shared.Services;
using HoteleriaApp.Core.Application.Interfaces;
using HoteleriaApp.Core.Application.Services;
using HoteleriaApp.Core.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using HoteleriaApp.Infrastructure.Persistence.Contexts;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();

builder.Services.AddScoped<IPisoService, PisoService>();
builder.Services.AddScoped<IPisoRepository, PisoRepository>();

builder.Services.AddScoped<IClienteRepositorio, ClienteRepositorio>();
builder.Services.AddScoped<IClienteServicio, ClienteServicio>();
builder.Services.AddSingleton<IEmailServicio, EmailServicio>();

builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();

builder.Services.AddDbContext<HoteleriaDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();