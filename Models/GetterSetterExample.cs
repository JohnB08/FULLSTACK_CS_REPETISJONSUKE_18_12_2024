using System;

namespace FULLSTACK_CS_REPETISJONSUKE_18_12_2024.Models;
/// <summary>
/// Her ser vi forskjellen på hvordan en Property med getter og setter fungerer, og hvordan en vanlig reference property fungerer.
/// </summary>
public class GetterSetterExample
{
    /* Når vi bruker {get;set;} lager vi noen skjulte underliggende properties, samt noen metoder som mangen innebygde metoder og attributer
    ofte leter etter.  */
    public int Id { get; set; }
    /* Det som egentlig skjer når man har en getter og en setter */
    /* Vi lager først en Privat versjon av propertien, som lever skjult inni vår Id property. */
    private int _Id;
    /* Vi lager så to metoder som også lever inni vår Id, som oppererer på den skjulte private _Id propertien vår. */
    public int Get()
    {
        return _Id;
    }
    public void Set(int input)
    {
        _Id = input;
    }
    /* Denne har ingen innebygde getters eller setters, er spesifikt bare en referanseverdi.
    Som vi så i repetisjonstimen vil mange innebygde class attributter ikke klare å opperere med denne,
    for disse attributene sine metoder leter ofte etter en Get eller en Set method. */
    public int NoGetterInput;
}
