ShoppingList list = new ShoppingList("items.txt");
list.Load();

while (true)
{
    Console.WriteLine();
    list.Print();
    Console.WriteLine();
    Console.WriteLine("1. Lägg till vara");
    Console.WriteLine("2. Ta bort vara");
    Console.WriteLine("3. Spara");
    Console.WriteLine("4. Sök vara");
    Console.WriteLine("5. Avsluta");
    Console.Write("Välj: ");

    int choice;
    // TryParse försöker göra om texten till ett tal. om det inte går frågar vi igen.
    while (!int.TryParse(Console.ReadLine(), out choice))
    {
        Console.Write("Skriv en siffra mellan 1 och 5: ");
    }

    if (choice == 1)
    {
        Console.Write("Namn: ");
        string name = Console.ReadLine();
        Console.Write("Pris: ");
        int price;

        // Samma sak för priset: fråga igen tills användaren skriver ett tal.
        while (!int.TryParse(Console.ReadLine(), out price))
        {
            Console.Write("Priset måste vara ett heltal. Försök igen: ");
        }
        try
        {
            list.Add(new Item(name, price));
        }
        // denna står över argumentexception för att den är en mer specifik typ av den. 
        // Den ärver ifrån argumentexception vilket är bredare. 
        catch (ArgumentOutOfRangeException)
        {
            Console.WriteLine("Priset får inte vara negativt");
        }
        catch (ArgumentException)
        {
            Console.WriteLine("Namnet får inte vara tomt");
        }
    }
    else if (choice == 2)
    {
        Console.Write("Nummer: ");
        int number;

        // Samma sak för numret på varan som ska tas bort.
        while (!int.TryParse(Console.ReadLine(), out number))
        {
            Console.Write("Skriv numret på varan: ");
        }
        // RemoveAt svarar false om numret inte fanns.
        if (!list.RemoveAt(number))
        {
            Console.WriteLine("Det finns ingen vara med det numret.");
        }
    }
    else if (choice == 3)
    {
        list.Save();
    }
    else if (choice == 4)
    {
        Console.Write("Namn att söka efter: ");
        string wanted = Console.ReadLine();
        Item found = list.Find(wanted);

        if (found == null)
        {
            Console.WriteLine("Varan finns inte i listan.");
        }
        else
        {
            Console.WriteLine($"Hittade: {found}");
        }
    }
    else if (choice == 5)
    {
        break;
    }
    else
    {
        Console.WriteLine("Det valet finns inte. Välj 1–5.");
    }
}
