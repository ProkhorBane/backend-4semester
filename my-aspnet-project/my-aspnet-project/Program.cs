using System.Text;
using System.Text.Json;
using System.Xml.Serialization;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();


List<Person> users = new List<Person>
{
    new() { Id = Guid.NewGuid().ToString(), Name = "Stas", Age = 27 },
    new() { Id = Guid.NewGuid().ToString(), Name = "Prokhor", Age = 19 },
    new() { Id = Guid.NewGuid().ToString(), Name = "Gleb", Age = 22 }
};

app.MapGet("api/users", () => users);

app.MapGet("api/users/search", (string? name, int? minAge) =>
{
    var filtered = users.AsEnumerable();
    if  (!string.IsNullOrEmpty(name)) 
        filtered = filtered.Where(u => u.Name.Contains(name));
    if  (minAge.HasValue)
        filtered = filtered.Where(u => u.Age >= minAge.Value);
    
    return Results.Ok(filtered);
});

app.MapGet("api/users/{id}", (string id) =>
{
    Person? user = users.FirstOrDefault(u => u.Id == id);
    if (user is null) return Results.NotFound(new {message = "User not found"});
    
    return Results.Ok(user);
});

app.MapPost("api/users/create", (Person user) =>
{
    user.Id = Guid.NewGuid().ToString();

    users.Add(user);
    return Results.Created($"api/users/{user.Id}", user);
});

app.MapPost("api/users/search", (string? name, int? minAge) =>
{
    var filtered = users.AsEnumerable();
    if  (!string.IsNullOrEmpty(name)) 
        filtered = filtered.Where(u => u.Name.Contains(name));
    if  (minAge.HasValue)
        filtered = filtered.Where(u => u.Age >= minAge.Value);
    
    return Results.Ok(filtered);
});

app.MapPut("api/users/{id}", (string id, Person updatedUser) =>
{
    var existing = users.FirstOrDefault(u => u.Id == id);
    if (existing is null) return Results.NotFound(new { message = "User not found" });

    existing.Name = updatedUser.Name;
    existing.Age = updatedUser.Age;

    return Results.Ok(existing);
});

app.MapPatch("api/users/{id}/update", (string id, JsonElement patchData) =>
{
    var user = users.FirstOrDefault(u => u.Id == id);
    if (user is null) return Results.NotFound(new { message = "User not found" });

    foreach (var patch in patchData.EnumerateObject())
    {
        switch (patch.Name.ToLower())
        {
            case "name":
                user.Name = patch.Value.GetString() ?? "";
                break;
            case "age":
                if (patch.Value.TryGetInt32(out int age))
                    user.Age = age;
                break;
        }
    }
    
    return Results.Ok(user);
});

app.MapGet("formats/html", () =>
{
    var html = """
               <html>
                   <body>
                       <h2>ASP.NET Core HTML</h2>
                       <p>Возврат html</p>
                   </body>
               </html>
               """;

    return Results.Content(html, "text/html; charset=utf-8");
});

app.MapGet("/formats/text", () =>
{
    return Results.Text("Возврат текстовой информации", "text/plain; charset=utf-8");
});

app.MapGet("formats/json", () =>
{
    return Results.Json(new
    {
        message = "Json работает",
        time = DateTime.Now
    });
});

app.MapGet("formats/xml", () =>
{
    var person = new Person { Id = "1", Name = "XML User", Age = 99 };

    var serializer = new XmlSerializer(typeof(Person));
    using var ms = new MemoryStream();
    serializer.Serialize(ms, person);

    return Results.File(ms.ToArray(), "application/xml");
});

app.MapGet("formats/csv", () =>
{
    var csv = new StringBuilder();
    csv.AppendLine("Id,Name,Age");

    foreach (var u in users)
        csv.AppendLine($"{u.Id},{u.Name},{u.Age}");

    return Results.File(
        Encoding.UTF8.GetBytes(csv.ToString()),
        "text/csv",
        "users.csv"
    );
});

app.MapGet("formats/binary", () =>
{
    byte[] data = new byte[] { 10, 20, 30, 40, 255 };
    return Results.File(data, "application/octet-stream", "data.bin");
});

app.MapGet("formats/image", () =>
{
    var path = Path.Combine(Directory.GetCurrentDirectory(), "image.jpg");
    return Results.File(path , "image/jpeg");
});

app.MapGet("formats/pdf", () =>
{
    var path = Path.Combine(Directory.GetCurrentDirectory(), "file.pdf");
    return Results.File(path, "application/pdf");
});

// 302
app.MapGet("formats/redirect", () =>
{
    return Results.Redirect("/formats/html");
});

// 301
app.MapGet("formats/redirect-permanent", () =>
{
    return Results.Redirect("/formats/html", permanent: true);
});

app.MapGet("/", () => "Hello ");
app.MapGet("/about", () => "мой первый aspnet проект");
app.MapGet("/user",  () => new { id = 1, Name = "Prokhor" });


app.Run();

public class Person
{
    public string Id { get; set; } = "";
    public string Name { get; set; } = "";
    public int Age { get; set; }
}