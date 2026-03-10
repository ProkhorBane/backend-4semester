using System.Text;
using System.Text.Json;
using System.Xml.Serialization;
using Microsoft.AspNetCore.Mvc;



var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.MapControllers();
app.UseHttpsRedirection();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


List<Person> users = new()
{
    new Person{Id = "1", Name = "Bob", Age = 29},
    new Person{Id = "2", Name = "John", Age = 45},
    new Person{Id = "3", Name = "Jack", Age = 20},
};

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

app.Run();

public class Person
{
    public string Id { get; set; } = "";
    public string Name { get; set; } = "";
    public int Age { get; set; }
}