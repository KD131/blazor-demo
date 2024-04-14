using BlazorDemo.Components;
using Microsoft.FluentUI.AspNetCore.Components;
using Model.Mapping;
using MudBlazor.Services;
using _Imports = BlazorDemo.Client._Imports;

namespace BlazorDemo;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddRazorComponents()
            .AddInteractiveServerComponents()
            .AddInteractiveWebAssemblyComponents();

        builder.Services.AddScoped(_ => new HttpClient
        {
            BaseAddress = new Uri("http://localhost:5055")
        });

        builder.Services.AddAutoMapper(typeof(ViewModelDtoProfile));

        builder.Services.AddMudServices();
        builder.Services.AddFluentUIComponents();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseWebAssemblyDebugging();
        }
        else
        {
            app.UseExceptionHandler("/Error", true);
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();

        app.UseStaticFiles();
        app.UseAntiforgery();

        app.MapRazorComponents<App>()
            .AddInteractiveServerRenderMode()
            .AddInteractiveWebAssemblyRenderMode()
            .AddAdditionalAssemblies(typeof(_Imports).Assembly);

        app.Run();
    }
}