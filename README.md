# Robust inköpslista

Kunskapskontroll 2 – Programmering och objektorienterad utveckling i C#

## Felrapport

### Fel 1: Kraschade direkt vid start
**Vad hände:** Programmet kraschade med `IndexOutOfRangeException` på rad 90 i `Load`. Sökningen hittade inte heller varor som syntes i listan.

**Varför:** `Save` skriver `\r\n` efter varje rad, men `Load` delade bara på `\n`. Därför blev `\r` kvar i slutet av varje namn, så namnet blev "Mjölk\r" och inte "Mjölk". Det blev också en tom rad sist i filen. Den tomma raden har inget semikolon, så `parts` fick bara en del, och `parts[1]` fanns inte.

**Hur jag löste det:** Jag delar nu på `"\r\n"`, samma tecken som `Save` skriver. Jag kollar också att `parts.Length == 2` innan jag använder raden, så att den tomma raden hoppas över.

### Fel 2: Kraschade om items.txt saknades
**Vad hände:** Om filen inte fanns kraschade programmet när det startade.

**Varför:** `File.ReadAllText` kastar `FileNotFoundException` när filen inte finns, och ingen fångade det.

**Hur jag löste det:** Jag lade läsningen i ett `try` och fångar `FileNotFoundException`. Då skriver programmet att filen saknas och börjar med en tom lista.


## Designval

## Klassdiagram