# Robust inköpslista

Kunskapskontroll 2 – Programmering och objektorienterad utveckling i C#

## Felrapport

### Fel 1: Kraschade direkt vid start
**Vad hände:** Programmet kraschade med IndexOutOfRangeException på rad 90 i Load. Sökningen hittade inte heller varor som syntes i listan.

**Varför:** Save skriver \r\n efter varje rad, men Load delade bara på \n. Därför blev \r kvar i slutet av varje namn, så namnet blev "Mjölk\r" och inte "Mjölk". Det blev också en tom rad sist i filen. Den tomma raden har inget semikolon, så parts fick bara en del, och parts[1] fanns inte.

**Hur jag löste det:** Jag delar nu på "\r\n", samma tecken som Save skriver. Jag kollar också att parts.Length == 2 innan jag använder raden, så att den tomma raden hoppas över.

### Fel 2: Kraschade om items.txt saknades
**Vad hände:** Om filen inte fanns kraschade programmet när det startade.

**Varför:** File.ReadAllText kastar FileNotFoundException när filen inte finns, och ingen fångade det.

**Hur jag löste det:** Jag lade läsningen i ett try och fångar FileNotFoundException. Då skriver programmet att filen saknas och börjar med en tom lista.

### Fel 3: int.Parse kraschar vid fel inmatning
**Vad hände:** Ifall man skriver bokstäver så kraschar programmet. Detta gäller i menyn, priset och numret av varan.

**Varför:** Programmet förväntar sig ett tal. int.Parse kan inte göra om till ex "hej" till ett tal.

**Hur jag löste det:** Jag använde TryParse istället som försöker göra om texten. Om det inte går frågar programmet igen istället för att krascha.

### Fel 4: Krasch vid borttagning av icke existerande produkt

**Vad hände:** Om man till exempel vill ta bort vara nummer 99 men det finns bara 3 varor så kraschar programmet.

**Varför:** Numret fanns inte i listan. 99 är ett riktigt tal så att TryParse släppte igenom det, men listan hade bara 3 varor så RemoveAt kastade ArgumentOutOfRangeException.

**Hur jag löste det:** RemoveAt kollar först om numret finns och svarar false om det inte gör det. Då skriver programmet ut ett meddelande.

### Fel 5: Fel total

**Vad hände:** Totalen visade 121kr istället för 136kr. Men det orsaka ingen krasch.

**Varför:** Loopen i Total() började på i = 1. Mjölken som har index 0 räknades aldrig med för loopen hoppade över den. 

**Hur jag löste det:** Jag ändrade i = 1 till i = 0.

### Fel 6: Tom catch i save()

**Vad hände:** Om sparandet misslyckas händer det inget och felet syns inte för användaren. Användaren hade till och med trott att det funkade pågrund av meddelandet som programmet visar. 

**Varför:** Det stod igenting inne i catch plus att raden Console.WriteLine("Listan är sparad."); står efter try och catch så den körs alltid även om sparandet inte gick igenom. 

**Hur jag löste det:** Jag flyttade "Listan är sparad" in till try då den bara skrivs ut om sparandet påriktigt lyckades. Byta den tomma catch mot en riktig catch block som fångar specifikau ndantag och berättar vad som hände Jag testade med Read-only och såg att det kastades UnauthorizedAccessException, så det är det jag fångar

## Designval

## Klassdiagram