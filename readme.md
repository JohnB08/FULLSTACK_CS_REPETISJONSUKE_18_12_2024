### Repetisjon 18.12.2024

Gjennomgang av minstekravet for backend i prosjektoppgaven.<br>

1. Vi så på hvordan vi kan ha en "model first" approach, hvor vi bestemmer datastruktur før vi får data.
2. Vi laget en samlet context, som holder referansen til vår data i minnet, samt alle metodene for å manipulere denne.
3. Vi snakket litt om hvorfor man av og til vil bruke nullverdier, hva som er forskjellen på en nullverdi, og en default verdi.
   - Hvis vi gir en referanse muligheten til å være null, sier vi til den at den har lov å peke til ingenting. <br>
     Dette vil ofte skape en ArgumentNullException i programmet vårt, så det er da også viktig vi håndterer nullreferanser bra.<br>
     Det kan også skape problemer hvor inkommende input kan overskrive vår nullpointer, og skrive sin egen info der, det kan være farlig.
   - En default verdi fyller minneområdet vår referanse peker til med en "tom" eller "default" verdi av datatypen som vår referanseverdi skal<br>
     refererer til. Det er ofte litt lettere for programmet vårt å håndtere, og vil ofte ikke skape Exceptions. Men kan være vanskeligere å<br>
     debugge siden referansen alltid returnerer en gyldig verdi.
4. Vi så på forskjellen på en standard referanseverdi og en med get; og set; metoder.
5. Vi testet endepunktene våre i ThunderClient, for å se at de oppfylgte kravene til oppgaven.


### Filoplasting 07.01.2025

1. Vi så på hvordan man kan hente filer fra en form request. 
2. Vi utvidet vår dto modell og vår familie modell til å inkludere en filpath.
3. Vi så på hvordan vi kan eksponere innholdet i wwwwroot folderen til en api
4. Vi lagret filer i images folderen i wwwroot folderen vår, og knyttet en referanse til pathen til et bilde til et familieobjekt.
5. Vi så på html forms, og hvordan hver "name" attributt der er en nøkkel i "form" objektet vi leser ut av httpRequesten vår. 
