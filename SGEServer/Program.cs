using DocumentFormat.OpenXml.Office2016.Drawing.ChartDrawing;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Hosting.StaticWebAssets;
using Microsoft.IdentityModel.Tokens;
using MudBlazor;
using MudBlazor.Services;
using SGEServer.Auth;
using SGEServer.Controllers;
using SGEServer.Data;
using System.Text;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

StaticWebAssetsLoader.UseStaticWebAssets(builder.Environment, builder.Configuration);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddMudServices();

// AUTHENTICATION
builder.Services.AddScoped<AuthenticationLogin>();
builder.Services.AddScoped<AuthenticationStateProvider, AuthenticationLogin>(provider => provider.GetRequiredService<AuthenticationLogin>());
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options => {
	options.TokenValidationParameters = new TokenValidationParameters
	{
		ValidateIssuer = false,
		ValidateAudience = false,
		ValidateLifetime = true,
		ValidateIssuerSigningKey = true,
		IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),
	};
});

builder.Services.AddScoped<ExcelHelper>();

// CONTROLLERS
builder.Services.AddSingleton<ProdutosController>();
builder.Services.AddSingleton<ClientesController>();
builder.Services.AddSingleton<GeralController>();
builder.Services.AddSingleton<LoginController_old>();
builder.Services.AddSingleton<Acesso>();
builder.Services.AddSingleton<CotacaoController>();
builder.Services.AddHttpClient<ViaCEPService>();
builder.Services.AddHttpClient<CnpjService>();
builder.Services.AddScoped<PdfHelper>();

// SNAKBAR - MENSAGENS DE AVISO
builder.Services.AddMudServices(config =>
{
    config.SnackbarConfiguration.PositionClass = Defaults.Classes.Position.BottomLeft;

    config.SnackbarConfiguration.PreventDuplicates = false;
    config.SnackbarConfiguration.NewestOnTop = false;
    config.SnackbarConfiguration.ShowCloseIcon = true;
    config.SnackbarConfiguration.VisibleStateDuration = 10000;
    config.SnackbarConfiguration.HideTransitionDuration = 300;
    config.SnackbarConfiguration.ShowTransitionDuration = 300;
    config.SnackbarConfiguration.SnackbarVariant = Variant.Filled;
});


var app = builder.Build();

// Configure a cultura para usar vírgula como separador decimal
var supportedCultures = new[] { "pt-BR" }; // Exemplo para o Brasil
var localizationOptions = new RequestLocalizationOptions().SetDefaultCulture(supportedCultures[0])
    .AddSupportedCultures(supportedCultures)
    .AddSupportedUICultures(supportedCultures);

app.UseRequestLocalization(localizationOptions);


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}



// BANCO DE DADOS
InfoConnection.DataConect = DataBase.LocalConect.QA;

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();