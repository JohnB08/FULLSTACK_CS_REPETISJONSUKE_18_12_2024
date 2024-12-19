using FULLSTACK_CS_REPETISJONSUKE_18_12_2024.Context;
using FULLSTACK_CS_REPETISJONSUKE_18_12_2024.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//Vi lager vår instans av Context her.
var context = new Context();

var app = builder.Build();

// Configure the HTTP request pipeline.
//Her er vår MapGet for å hente en familie basert på id
app.MapGet("/api/familie/{id}", (int id) =>
{
    return context.Get(id);
});

//Dette er vår MapGet som kan ta inn en HttpRequest som input, og returnerer et søk basert på søkeparameteret.
app.MapGet("/api/familie", (HttpRequest req) =>
{
    var queryParams = req.Query;
    var dto = context.GetQueryParams(queryParams);
    return context.Get(dto);
});

//Dette er vår MapPost som kan lage en ny familie basert på inkommende Json fra HttpRequest.
app.MapPost("/api/familie", async (HttpRequest req) =>
{
    var dto = await req.ReadFromJsonAsync<FamilieDTO>();
    context.Add(dto);
});

//Dette er vår MapPatch som tar inn en dto fra HttpRequest, men også en id fra url parameteret.
app.MapPatch("/api/familie/{id}", async (HttpRequest req, int id) =>
{
    var dto = await req.ReadFromJsonAsync<FamilieDTO>();
    context.Update(id, dto);
});

//Dette er vår MapDelete som tar inn en id som parameter fra url, og sletter basert på id.
app.MapDelete("/api/familie/{id}", (int id) =>
{
    context.Delete(id);
});

app.UseHttpsRedirection();

app.Run();

