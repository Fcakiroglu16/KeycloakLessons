using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();


builder.Services.AddAuthentication(options =>
    {
        options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
    }).AddCookie(opts => { opts.AccessDeniedPath = "/Home/AccessDenied"; })
    .AddOpenIdConnect(opts =>
    {
        opts.RequireHttpsMetadata = false;

        opts.Authority = "http://localhost:8080/realms/InnovaTenat";
        opts.ClientId = "web";
        opts.ClientSecret = "sRv7DkLyfGjYbwfhSO6y2dVWBHm3kp1s";
        opts.ResponseType = "code";
        opts.GetClaimsFromUserInfoEndpoint = true;
        opts.SaveTokens = true;
        opts.Scope.Add("profile email address phone roles");
        opts.TokenValidationParameters = new TokenValidationParameters
        {
            NameClaimType = "preferred_username",
            RoleClaimType = "roles"
        };

        //opts.Events.OnRedirectToIdentityProvider += context =>
        //{
        //    context.Options.Authority = "http://localhost:8080/realms/ExampleTenant";

        //    return Task.CompletedTask;
        //};
    });


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();

app.Run();