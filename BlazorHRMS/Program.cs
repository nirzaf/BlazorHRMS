using BlazorHRMS.Components;

var builder = WebApplication.CreateBuilder(args);

// Load secrets configuration file if it exists
if (File.Exists(Path.Combine(builder.Environment.ContentRootPath, "appsettings.secrets.json")))
{
    builder.Configuration.AddJsonFile("appsettings.secrets.json", optional: true, reloadOnChange: true);
}

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// Register MongoDB service
builder.Services.AddSingleton<BlazorHRMS.Services.MongoDBService>();

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