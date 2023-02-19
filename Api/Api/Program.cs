using System;
using System.Text.Json;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder();
var app = builder.Build();

app.Run(async (context) =>
{
    var response = context.Response;
    var request = context.Request;
    try
    {
        if (request.Path == "/api/user")
        {
            var responseText = "Некорректные данные";
            if (request.HasJsonContentType())
            {
                var jsonoptions = new JsonSerializerOptions();
                jsonoptions.Converters.Add(new PersonConverter());
                var person = await request.ReadFromJsonAsync<Person>(jsonoptions);
                if (person != null)
                    responseText = $"Id: {person.Id} Name: {person.Name}";
            }
            await response.WriteAsJsonAsync(new { text = responseText });
        }
        else
        {
            response.ContentType = "text/html; charset=utf-8";
            await response.SendFileAsync("index.html");
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Exception caught: {ex.Message}");
        response.StatusCode = 500;
        response.ContentType = "text/plain";
        await response.WriteAsync("Internal Server Error");
    }
});

app.Run();

public record Person(int Id, string Name);

public class PersonConverter : JsonConverter<Person>
{
    public override Person Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var personName = "Undefined";
        var personId = 0;
        while (reader.Read())
        {
            if (reader.TokenType == JsonTokenType.PropertyName)
            {
                var propertyName = reader.GetString();
                reader.Read();
                switch (propertyName?.ToLower())
                {
                    case "id" when reader.TokenType == JsonTokenType.Number:
                        personId = reader.GetInt32();
                        break;
                    case "name":
                        string? name = reader.GetString();
                        if (name != null)
                            personName = name;
                        break;
                }
            }
        }
        return new Person(personId, personName);
    }

    public override void Write(Utf8JsonWriter writer, Person person, JsonSerializerOptions options)
    {
        writer.WriteStartObject();
        writer.WriteNumber("id", person.Id);
        writer.WriteString("name", person.Name);
        writer.WriteEndObject();
    }
}