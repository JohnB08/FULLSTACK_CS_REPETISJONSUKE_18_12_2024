using FULLSTACK_CS_REPETISJONSUKE_18_12_2024.Context;
using FULLSTACK_CS_REPETISJONSUKE_18_12_2024.Models;
using Microsoft.AspNetCore.Http.HttpResults;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
//Vi lager vår instans av Context her.
var context = new Context();

var app = builder.Build();

//Her lar vi apiet vårt eksponere vår static file folder, default vår wwwroot folder. 
//Alle folderene og filene som eksisterer her blir tilrettelagt som egne "paths" i vår api. 
//På samme måte som vi kan mappe forskjellige metoder til paths, som vi gjør under.
//Får også filer og mapper samme paths.

//f.eks vi kan accesse et bilde som ligger i image mappen via:
//apiurl/images/image.jpg
//Dette er også grunnen til at vi kan accesse index.html filen via apiurl/index.html
app.UseStaticFiles();
app.UseDefaultFiles();

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

app.MapPost("/api/upload", async (HttpRequest req) => 
{
    //Her bruker vi vår FromForm metode i Context for å hente ut data fra en httpForm for å lage en ny familie, inkludert bilder. 
    //Her må vi passe inn en referanse til builderen vår slik at vi kan hente ut wwwroot folderen vår fra environmentet.
    var dto = await context.FromForm(req, builder);

    //Vi bruker så Add metoden vår for å adde et nytt familieobjekt til context via vår dto. 
    context.Add(dto);

    //Vi returnerer OK. 
    return Results.Ok();

});

app.UseHttpsRedirection();

//Her forteller vi siden vår å redirekte til staticfilen index.html hvis noen prøver et endepunkt som ikke eksisterer. 
//Hvis f.eks noen går rett til http://localhost:5244, så blir de redirected til http://localhost:5244/index.html.
app.MapFallbackToFile("index.html");

app.Run();

