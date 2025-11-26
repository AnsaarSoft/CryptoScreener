using Blazored.SessionStorage;
using Microsoft.EntityFrameworkCore;
using MudBlazor;
using MudBlazor.Services;
using Portfolio.Components;
using Portfolio.Models;
using Portfolio.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddBlazoredSessionStorage();
builder.Services.AddMudServices(options =>
{
    options.SnackbarConfiguration.PositionClass = Defaults.Classes.Position.BottomRight;
    options.SnackbarConfiguration.PreventDuplicates = false;
    options.SnackbarConfiguration.NewestOnTop = false;
    options.SnackbarConfiguration.ShowCloseIcon = false;
    options.SnackbarConfiguration.VisibleStateDuration = 4000;
    options.SnackbarConfiguration.HideTransitionDuration = 500;
    options.SnackbarConfiguration.ShowTransitionDuration = 500;
    options.SnackbarConfiguration.SnackbarVariant = Variant.Filled;
});
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite("Data Source=Models/Database/app.db"));
builder.Services.AddHttpClient<DashboardServices>();
builder.Services.AddScoped<DashboardServices>();
builder.Services.AddScoped<OrderServices>();
builder.Services.AddScoped<UserServices>();
builder.Services.AddScoped<DownloadService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();


app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
