using System.Diagnostics;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SpaServices;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ProblemTracking.Entity;
using ProblemTracking.Repository;
using ProblemTracking.Web.Services;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;
var services = builder.Services;

services.AddHttpContextAccessor();
services.AddControllersWithViews();

var connectionString = configuration.GetConnectionString("DBConnectionString") ?? string.Empty;
services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(connectionString, sqlBuilder =>
    {
        sqlBuilder.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.GetName().Name);
    });
});

services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = false;
        options.SaveToken = true;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = configuration["Jwt:Issuer"],
            ValidAudience = configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:SecretKey"]!)),
            ClockSkew = TimeSpan.Zero
        };
    });

services.AddCors();

services.AddAuthorization(config =>
{
    config.AddPolicy("Admin", new AuthorizationPolicyBuilder().RequireAuthenticatedUser().RequireRole("Admin").Build());
    config.AddPolicy("User", new AuthorizationPolicyBuilder().RequireAuthenticatedUser().RequireRole("User").Build());
});

services.AddSwaggerDocument();

services.AddSpaStaticFiles(spa =>
{
    spa.RootPath = "ClientApp/dist";
});

services.AddScoped<IServiceFactory, ServiceFactory>();
services.AddScoped<UnitOfWork, UnitOfWork>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseOpenApi();
app.UseSwaggerUi();

app.UseCors(b => b.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

app.UseHttpsRedirection();
app.UseStaticFiles();
if (!app.Environment.IsDevelopment())
{
    app.UseSpaStaticFiles();
}

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller}/{action=Index}/{id?}");
});

app.UseSpa(spa =>
{
    spa.Options.SourcePath = "ClientApp";

    if (app.Environment.IsDevelopment())
    {
        spa.Options.StartupTimeout = TimeSpan.FromSeconds(300);
        StartAngularDevServer(spa.Options.SourcePath);
        spa.UseProxyToSpaDevelopmentServer(async () =>
        {
            await WaitForPortAsync(4200, TimeSpan.FromSeconds(300));
            return new Uri("http://localhost:4200");
        });
    }
});

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    dbContext.Database.Migrate();
}

app.Run();


static void StartAngularDevServer(string sourcePath)
{
    if (IsPortInUse(4200))
    {
        Console.WriteLine("[Angular] Port 4200 already in use — assuming dev server is running.");
        return;
    }

    var workingDir = Path.GetFullPath(sourcePath);
    var isWindows = OperatingSystem.IsWindows();

    var psi = isWindows
        ? new ProcessStartInfo
        {
            FileName = "cmd.exe",
            Arguments = "/c start \"Angular Dev Server\" cmd /k npm start",
            WorkingDirectory = workingDir,
            UseShellExecute = true
        }
        : new ProcessStartInfo
        {
            FileName = "npm",
            Arguments = "start",
            WorkingDirectory = workingDir,
            UseShellExecute = false
        };

    try
    {
        Process.Start(psi);
        Console.WriteLine($"[Angular] Launched dev server (cwd: {workingDir}). Waiting for port 4200...");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"[Angular] Failed to launch dev server: {ex.Message}");
    }
}

static async Task WaitForPortAsync(int port, TimeSpan timeout)
{
    var deadline = DateTime.UtcNow.Add(timeout);
    while (DateTime.UtcNow < deadline)
    {
        if (IsPortInUse(port))
        {
            Console.WriteLine($"[Angular] Port {port} is ready.");
            return;
        }
        await Task.Delay(1000);
    }
    throw new TimeoutException(
        $"Angular dev server did not start on port {port} within {timeout.TotalSeconds}s. " +
        "Check the Angular Dev Server window for errors, or run 'npm start' manually in ClientApp/.");
}

static bool IsPortInUse(int port)
{
    try
    {
        using var client = new System.Net.Sockets.TcpClient();
        var result = client.BeginConnect("localhost", port, null, null);
        var success = result.AsyncWaitHandle.WaitOne(TimeSpan.FromMilliseconds(500));
        if (success)
        {
            client.EndConnect(result);
            return true;
        }
    }
    catch
    {
    }
    return false;
}
