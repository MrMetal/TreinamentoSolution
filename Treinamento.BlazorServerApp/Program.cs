using Blazored.LocalStorage;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Treinamento.BlazorServerApp.Components;
using Treinamento.BlazorServerApp.Data.ApiEndpoints;
using Treinamento.BlazorServerApp.Data.Endpoints;
using Treinamento.BlazorServerApp.Data.Endpoints.Empresas;
using Treinamento.BlazorServerApp.Data.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddBlazoredLocalStorage();

builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
        options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    })
    .AddCookie(options =>
    {
        options.LoginPath = "/login";
        options.ExpireTimeSpan = TimeSpan.FromMinutes(60);
    });

//builder.Services.AddAuthorization();
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("SomenteAdmins", policy =>
        policy.Requirements.Add(new ClaimRequirement("Admin", "Create", "Read", "Update", "Delete")));

    options.AddPolicy("PermissaoRead", policy =>
        policy.Requirements.Add(new ClaimRequirement("Admin", "Read")));

    options.AddPolicy("PermissaoDelete", policy =>
        policy.Requirements.Add(new ClaimRequirement("Admin", "Delete")));

    // Se quiser checar também para o perfil "Comum"
    options.AddPolicy("PermissaoComumRead", policy =>
        policy.Requirements.Add(new ClaimRequirement("Comum", "Read")));
});

builder.Services.AddScoped(typeof(IApiEndpoints<>), typeof(ApiEndpoints<>));
builder.Services.AddScoped<SignInUserFromJwtAsyncService>();
builder.Services.AddScoped<ObterTodasEmpresasEndpoint>();
builder.Services.AddScoped<ObterEmpresaPorIdEndpoint>();

builder.Services.AddSingleton<JwtTokenFactory>();
builder.Services.AddSingleton<IAuthorizationHandler, PermissaoHandler>();


builder.Services.AddHttpClient(); // necessário para IHttpClientFactory
builder.Services.AddHttpContextAccessor();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseAntiforgery();

app.AuthEndpointMap();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
