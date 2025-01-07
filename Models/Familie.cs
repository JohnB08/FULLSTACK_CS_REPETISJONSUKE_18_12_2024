using System;
using System.Text.Json.Serialization;

namespace FULLSTACK_CS_REPETISJONSUKE_18_12_2024.Models;
/// <summary>
/// Dette er vår modell for en familie (uten bilde)
/// Den holder alle verdier vi trenger for å kunne sende informasjon om familier til frontenden.
/// Denne apien jobber via et Model First prinsipp:
/// Det vil si vi har ikke data enda, men vi vet hva form dataen bør ha, basert på modellen vår her.
/// </summary>
public class Familie
{
    [JsonPropertyName("id")]
    public int Id { get; set; }
    [JsonPropertyName("count")]
    public int Count { get; set; }
    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;
    [JsonPropertyName("description")]
    public string Description { get; set; } = string.Empty;
    [JsonPropertyName("preferences")]
    public List<string> Preferences { get; set; } = [];
    [JsonPropertyName("allergies")]
    public List<string> Allergies { get; set; } = [];
    [JsonPropertyName("imageurl")]
    public string ImageUrlPath {get;set;} = string.Empty;
}
