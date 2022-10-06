using ProxyKit;
using UI;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddProxy();
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

var apiSettings = new ApiSettings();

app.Configuration.Bind("Api", apiSettings);

app.Map("/api", api =>
{
    api.RunProxy(async context =>
    {
        var forwardContext = context.ForwardTo(apiSettings.ForwardUrl);

        return await forwardContext.Send();
    });
});


app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");

app.MapFallbackToFile("index.html"); ;

app.Run();
