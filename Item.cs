// One item on the shopping list.
class Item
{
    public string Name { get; set; }
    public int Price { get; set; }

    public Item(string name, int price)
    {
        if (name == "")
        {
            // vägrar skapa en vara med tomt namn
            throw new ArgumentException("Namnet får inte vara tomt", nameof(name));
        }
        if (price < 0)
        {
            // vägrar skapa vara med negativt pris
            throw new ArgumentOutOfRangeException(nameof(price), "Priset får inte vara negativt");
        }
        Name = name;
        Price = price;
    }

    public override string ToString()
    {
        return $"{Name} - {Price} kr";
    }
}
