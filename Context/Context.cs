using System;
using System.Text.Json;
using FULLSTACK_CS_REPETISJONSUKE_18_12_2024.Models;

namespace FULLSTACK_CS_REPETISJONSUKE_18_12_2024.Context;
/// <summary>
/// Dette er vår datacontext, og inneholder alle metodene våre for å hente, lagre, sende og manipulere dataen vår.
/// </summary>
public class Context
{
    public List<Familie> Families = [];
    public int Incrementor = 0;
    /// <summary>
    /// Dette er vår overload av metoden Get, som tar inn en int id som parameter, og returnerer Familien som matcher Id.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public Familie? Get(int id)
    {
        return Families.FirstOrDefault(family => family.Id == id);
    }
    /// <summary>
    /// Dette er vår metode for å mappe en DTO til, enten en nytt familieobjekt,
    /// eller et eksisterende familieobjekt, hvis vi velger å legge det med som parameter.
    /// </summary>
    /// <param name="dto">dette er vår Data Transfer Object som representerer et sett med json parametere.</param>
    /// <param name="familie">Dette er et valgfritt parameter, som lar map metoden vår mappe direkte til et nytt objekt isteden
    /// for å lage et nytt. Dette kunne også vært en Overload, og hvis man har mange conditional parametere, kan ofte en Overload være en
    /// god måte å separere logikk på.</param>
    /// <returns></returns>
    public Familie Map(FamilieDTO dto, Familie? familie = null)
    {
        if (familie == null)
        {
            familie = new();
            Incrementor++;
            familie.Id = Incrementor;
        }
        if (dto.Count.HasValue)
        {
            familie.Count = (int)dto.Count;
        }
        if (!string.IsNullOrEmpty(dto.Name))
        {
            familie.Name = dto.Name;
        }
        if (!string.IsNullOrEmpty(dto.Description))
        {
            familie.Description = dto.Description;
        }
        if (dto.Preferences != null && dto.Preferences.Count > 0)
        {
            familie.Preferences = dto.Preferences;
        }
        if (dto.Allergies != null && dto.Allergies.Count > 0)
        {
            familie.Allergies = dto.Allergies;
        }
        if (!string.IsNullOrEmpty(dto.ImageUrlPath))
        {
            familie.ImageUrlPath = dto.ImageUrlPath;
        }
        return familie;
    }
    /// <summary>
    /// Dette er vår metode som fjerner et familieobjekt basert på en familie id.
    /// </summary>
    /// <param name="id"></param>
    public void Delete(int id)
    {
        var fam = Families.FirstOrDefault(fam => fam.Id == id);
        if (fam != null) Families.Remove(fam);
        SaveChanges();
    }
    /// <summary>
    /// Dette er vår overload av Get, som tar in en DTO som parameter
    /// og finner en familie basert på et sett med parametere.
    /// </summary>
    /// <param name="dto">representerer vår Data Transer Object med parametere vi skal bruke i søket vårt.</param>
    /// <returns></returns>
    public Familie? Get(FamilieDTO dto)
    {
        var query = Families.AsQueryable();
        if (!string.IsNullOrEmpty(dto.Name))
            query = query.Where(fam => fam.Name.Contains(dto.Name, StringComparison.InvariantCultureIgnoreCase));
        if (!string.IsNullOrEmpty(dto.Description))
            query = query.Where(fam => fam.Description.Contains(dto.Description, StringComparison.InvariantCultureIgnoreCase));
        if (dto.Count.HasValue)
            query = query.Where(fam => fam.Count == dto.Count);
        if (dto.Allergies != null && dto.Allergies.Count > 0)
            query = query.Where(fam => dto.Allergies.Any(allergy => fam.Allergies.Contains(allergy)));
        if (dto.Preferences != null && dto.Preferences.Count > 0)
            query = query.Where(fam => dto.Preferences.Any(pref => fam.Preferences.Contains(pref)));
        return query.FirstOrDefault();
    }
    /// <summary>
    /// Dette er en metode for oss å ta imot en DTO fra en request, og mappe det til et nytt Familieobject, før det blir addet
    /// til vår Families liste.
    /// </summary>
    /// <param name="dto"></param>
    public void Add(FamilieDTO dto)
    {
        Families.Add(Map(dto));
        SaveChanges();
    }
    /// <summary>
    /// Denne metoden finner først en eksisterende familie i listen, så bruker den DTO og map for å mappe de nye parameterene
    /// fra DTO til Familien.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="dto"></param>
    public void Update(int id, FamilieDTO dto)
    {
        var existingFam = Get(id);
        if (existingFam == null) return;
        Map(dto, existingFam);
        SaveChanges();
    }
    /// <summary>
    /// Dette er options for å passe på at våre json Metoder kan camelCasifisere objetkene korrekt. 
    /// </summary>
    private JsonSerializerOptions _options = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
    };
    /// <summary>
    /// Dette er vår metode som leser listen Families og lagrer den som en fil Familie.json
    /// </summary>
    public void SaveChanges()
    {
        var jsonString = JsonSerializer.Serialize(Families, _options);
        File.WriteAllText("Familie.json", jsonString);
    }
    /// <summary>
    /// Dette er vår constructor som leser filen "Familie.json" hvis den eksisterer, hvis ikke lager den default verdier isteden. 
    /// </summary>
    public Context()
    {
        try
        {
            var jsonString = File.ReadAllText("Familie.json");
            Families = JsonSerializer.Deserialize<List<Familie>>(jsonString, _options) ?? [];
            if (Families.Count > 0)
            {
                Incrementor = Families.Last().Id;
            }
        }
        catch (Exception)
        {
            Families = [];
            Incrementor = 0;
        }
    }
    /// <summary>
    /// Dette er en metode som lager en DTO basert på en samling av IQueryCollections fra HttpRequest. 
    /// Vi skal se etter nyttår hvordan denne kan forbedres via noe som heter refleksjon. 
    /// </summary>
    /// <param name="query">Samling av key/values fra UrlSearchParam delen av url.</param>
    /// <returns></returns>
    public FamilieDTO GetQueryParams(IQueryCollection query)
    {
        var dto = new FamilieDTO();
        var queryCount = query["count"].ToString();
        var queryName = query["name"].ToString();
        var queryDescription = query["description"].ToString();
        var queryPref = query["preferences"].ToString();
        var queryAll = query["allergies"].ToString();
        dto.Count = int.TryParse(queryCount, out int countVal) ? countVal : null;
        dto.Name = string.IsNullOrEmpty(queryName) ? null : queryName;
        dto.Description = string.IsNullOrEmpty(queryDescription) ? null : queryDescription;
        dto.Preferences = string.IsNullOrEmpty(queryPref) ? null : [.. queryPref.Split(",")];
        dto.Allergies = string.IsNullOrEmpty(queryAll) ? null : [.. queryAll.Split(",")];
        return dto;
    }

    public async Task<FamilieDTO> FromForm(HttpRequest req, WebApplicationBuilder builder)
    {
        //Vi lager et nytt data transfer objekt.
        var dto = new FamilieDTO();

        //Vi leser ut formen fra httpRequesten.
        var form = await req.ReadFormAsync();

        //Vi finner første filen fra formen, som vi forventer å være et bilde.
        var file = form.Files.FirstOrDefault();

        //Vi finner vår wwwwroot static file folder, og bildefilen som eksisterer der.
        var folderPath = Path.Combine(builder.Environment.WebRootPath, "./images/");

        //Vi genererer et unikt filnavn til bildet.
        var uniqueFileName = Path.GetRandomFileName() + file.FileName;

        //Vi lager en referanse til pathen som vår lokale kopi av bildet skal ha. 
        var filePathUrl = Path.Combine(folderPath + uniqueFileName);

        //Vi åpner en ny "bitstream" som lar oss kopiere bits og metadata fra filen i httpRequesten til vår lokale kopi av filen.
        using (var fileStream = new FileStream(filePathUrl, FileMode.Create))
        {
            //Vi kopierer filen fra httpRequesten til vår nye bildefil.
            file.CopyTo(fileStream);
        }

        //Vi går inn i formen og finner verdien bak nøkkelen "count".
        var queryCount = form["count"].ToString();

        //Vi går inn i formen og finner verdien bak nøkkelen "name".
        var queryName = form["name"].ToString();

        //Vi går inn i formen og finner verdien bak nøkkelen "description".
        var queryDescription = form["description"].ToString();

        //vi går inn i formen og finner verdien bak nøkkelen "preferences".
        var queryPref = form["preferences"].ToString();

        //vi går inn i formen og finner verdien bak nøkkelen "allergies"
        var queryAll = form["allergies"].ToString();

        //vi definerer dto sin Count property som enten int Countvall, eller null
        dto.Count = int.TryParse(queryCount, out int countVal) ? countVal : null;

        //Vi definerer dto sin Name som enten null eller queryName
        dto.Name = string.IsNullOrEmpty(queryName) ? null : queryName;

        //Vi definerer dto sin description som enten null eller queryDescription.
        dto.Description = string.IsNullOrEmpty(queryDescription) ? null : queryDescription;

        //Her definerer vi preferences som enten null eller som et nytt array med queryPref stringen splittet på kommakarakteren
        dto.Preferences = string.IsNullOrEmpty(queryPref) ? null : [.. queryPref.Split(",")];

        //her definerer vi allergies som enten null eller som et nytt array med queryAll stringen splittet på kommakarakteren.
        dto.Allergies = string.IsNullOrEmpty(queryAll) ? null : [.. queryAll.Split(",")];

        //Her lager vi en path som er relativ til wwwroot folderen. Denne kan serene brukes sammen med url til apiet for å hente ut filen. Vi knytter den til ImageUrlPath slik av den kan hentes ut av et family objekt senere.
        dto.ImageUrlPath = "/images/" + uniqueFileName;
        return dto;
    }
}
