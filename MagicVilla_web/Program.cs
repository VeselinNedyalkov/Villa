using MagicVilla_web;
using MagicVilla_web.Services;
using MagicVilla_web.Services.Contacts;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddAutoMapper(typeof(MappingConfig));

builder.Services.AddHttpClient<IVillaSurvice, VillaSurvice>();
builder.Services.AddScoped<IVillaSurvice, VillaSurvice>();

builder.Services.AddHttpClient<IVillaNumberSurvice, VillaNumberSurvice>();
builder.Services.AddScoped<IVillaNumberSurvice, VillaNumberSurvice>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
