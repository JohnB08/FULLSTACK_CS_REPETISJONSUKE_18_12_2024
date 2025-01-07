using System;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;

namespace FULLSTACK_CS_REPETISJONSUKE_18_12_2024.Models;
/// <summary>
/// Dette er modellen vi bruker for å hente info fra en request, og som vi kan mappe over til et Familie objekt.
/// </summary>
public class FamilieDTO
{
    [JsonPropertyName("count")]
    public int? Count {get;set;}
    [JsonPropertyName("name")]
    public string? Name {get;set;}
    [JsonPropertyName("description")]
    public string? Description {get;set;}
    [JsonPropertyName("preferences")]
    public List<string>? Preferences {get;set;}
    [JsonPropertyName("allergies")]
    public List<string>? Allergies {get;set;}
    public string? ImageUrlPath {get;set;}
}
