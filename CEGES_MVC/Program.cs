
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using CEGES_DataAccess.Persistence;
using CEGES_DataAccess.Repository;
using CEGES_DataAccess.Repository.IRepository;
using CEGES_Services.Interfaces;
using CEGES_Services.Implementation;
using Microsoft.Extensions.Options;
using CEGES_MVC.ModelBinders;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews(options =>
{
    options.ModelBinderProviders.Insert(0, new EquipementVMEntityBinderProvider());
});


builder.Services.AddDbContext<ApplicationDbContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IEntrepriseService, EntrepriseService>();
builder.Services.AddScoped<IGroupeService, GroupService>();
builder.Services.AddScoped<IEquipementService, EquipementService>();


builder.Services.AddAutoMapper(typeof(Program).Assembly);
builder.Services.AddRazorPages().AddRazorRuntimeCompilation();
builder.Services.AddDistributedMemoryCache();

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.MapControllerRoute(
    name: "Configuration",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");
app.MapAreaControllerRoute(
    name: "default",
    areaName: "Configuration",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();