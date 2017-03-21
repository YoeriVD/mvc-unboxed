# MVC Unboxed

### Deze applicatie is gebruikt om demo's te geven op Karel de Grote Hogeschool op 17/03/2017

## Setup

### ter informatie geef ik mee hoe ik de hele solution heb opgezet

1. nieuw project maken (naam: unboxed.web)
1. ASP.NET
    + MVC aanvinken
    + test project aanvinken
    + al de rest uitvinken (zeker die application insights, zeer nuttig maar niet nu)
1. 2 nieuwe projecten toevoegen
    + core project (unboxed.dll)
    + migrations apart houden (trust me)
1. Rechtermuisknop op solution -> manage nuget packages
1. EntityFramework toevoegen aan alle projecten
1. Nieuwe klasse aanmaken `{Naam}DbContext.cs` en overerven van `DbContext`
1. `View` -> `Other Windows`-> `Package Manager Console`
    + het migrations project selecteren in de dropdown
    + volgend commando uitvoeren: 
    ```
    enable-migrations -ContextProjectName unboxed -StartUpProjectName unboxed.web
    ```
1. Je eerste migration maken (nog steeds in Package manager console)
    + `Add-Migration MyFirstMigration`

### Opmerking
+ T4MVC runt niet vanzelf :-) na een code aanpassing even rechterklik op `T4MVC.tt` en `Run custom tool` aanklikken.
