var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Hello ");
app.MapGet("/about", () => "мой первый aspnet проект");
app.MapGet("/user",  () => new { id = 1, Name = "Prokhor" });


app.Run();