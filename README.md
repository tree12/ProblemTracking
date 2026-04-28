# ProblemTracking
This project is just demo for full stack with angular

# Tool
nodejs version: v24.14.1

Visual studio 2026

.Net core 10.0

Nswag studio version 14.1.1

# How to install 
1. Change **DBConnectionString** in **appsettings.json** of **ProblemTracking.Web project** to be your DB configulation.
2. Browser to the path => "ProblemTracking.Web\ClientApp" open command prompt and run **npm install** (please use nodejs version v24.14.1 if possible)
3. Run web application(This step the program run migration scripts).
4. Open Nswag studio open file "api.client.swagger.nswag" at "ProblemTracking.Web\ClientApp\src\app\shared\services".
5. Normally the program automately refresh the page if not manually reflesh browser again.
6. if login page appear use deme users below to login <br/>
    user: user1 
    
    password: 12345
    
    role: Admin
    
    <br/>
    user: user2
    
    password: 12345
    
    role: User
    
# Overall
This is the demo program to show how to implement backend(c#) and frontend(Angular) work together. I create this project for problem solving of machine. This system has 2 role Admin and User roles.
### User role
Add the problem and how to solve the problem.

<img src="./ProblemTracking.Web/user_screen.png" width="450" />

### Admin role
View the report how many problems and the status of problems can solve or not.

<img src="./ProblemTracking.Web/admin_screen.png" width="450" />

# How it work
This project contains 3 project 
## ProblemTracking.Entity
This project use to interact with database. It contain entities and migration script.

## ProblemTracking.Repository
This project use to manipulate the entity such as query, insert data. It is the middle of web project and entity project. It contaian of repository interface and unit of work class.

## ProblemTracking.Web
This project is the web project which interact with user. It contain web api and Angular component. I upgraded .net5.0 to 10.0. I needed to modify the code such as merge StartUp.cs and Program.cs to be one file(Program.cs), and changed some code to auto run frondend while running backend. I recommend to run separately between frondend and backend, so you can ignore the code below. 
```Program.cs
.........
........
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
........
........
........

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
.........
........
```


