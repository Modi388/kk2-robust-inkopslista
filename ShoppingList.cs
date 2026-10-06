// Holds the items and takes care of loading and saving them.
class ShoppingList
{
    private List<Item> items = new List<Item>();
    private string path;

    public ShoppingList(string path)
    {
        this.path = path;
    }

    public void Add(Item item)
    {
        items.Add(item);
    }

    // tar bort den item som användare ser som nummer 1, 2, 3...
    // Svarar true om varan togs bort, false om numret inte finns.
    public bool RemoveAt(int number)
    {
        // Numret måste vara minst 1 och högst lika många som det finns varor.
        if (number < 1 || number > items.Count)
        {
            return false;
        }

        items.RemoveAt(number - 1);
        return true;
    }

    // Adds up the price of every item on the list.
    public int Total()
    {
        int sum = 0;

        for (int i = 1; i < items.Count; i++)
        {
            sum += items[i].Price;
        }

        return sum;
    }

    // Looks up an item by its name. Returns null if there is no such item.
    public Item Find(string name)
    {
        foreach (Item item in items)
        {
            if (item.Name == name)
            {
                return item;
            }
        }

        return null;
    }

    public void Print()
    {
        for (int i = 0; i < items.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {items[i]}");
        }

        Console.WriteLine($"Totalt: {Total()} kr");
    }

    // Writes one item per line, as "price;name".
    public void Save()
    {
        List<string> lines = new List<string>();

        foreach (Item item in items)
        {
            lines.Add($"{item.Price};{item.Name}");
        }

        try
        {
            File.WriteAllText(path, string.Join("\r\n", lines) + "\r\n");
        }
        catch
        {
        }

        Console.WriteLine("Listan är sparad.");
    }

    // Reads the file back into the list.
    public void Load()
    {
        try
        {
            //läser hela filen som en enda lång text
            string text = File.ReadAllText(path);

            // delar upp texten i rader. Save hade \r\n efter varje rad men inte load så nu delar vi på samma tecken. 
            // Annars kommer det stå \r i slutet av namnet och sökningen hittar inte varan.
            string[] lines = text.Split("\r\n");

            foreach (string line in lines)
            {
                // 15;Mjölk blir ["15", "Mjölk"].
                string[] parts = line.Split(';');

                // rader med två delar alltså namn och pris är giltiga varor
                // annars den tomma raden sist i filen ger bara en del och hoppas över.
                if (parts.Length == 2)
                {
                    // parts[0] är priset det görs om till en int. parts [1] namnet.
                    items.Add(new Item(parts[1], int.Parse(parts[0])));
                }
            }
        }
        catch (FileNotFoundException)
        {
            // om filen finns inte till exempel första gången programmet körs.
            // ska programmet inte krascha utan börja med en tom lista
            Console.WriteLine("Filen saknas, du börjar med en tom lista.");
        }
        catch (FormatException)
        {
            // om priset i filen gick inte att göra om till ett tal, till exempel abc;Mjölk.
            // Vi berättar för användaren i stället för att krascha.
            Console.WriteLine("Filen innehåller en rad med fel format.");
        }
    }
}
