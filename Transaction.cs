namespace ConsoleApp1;

public class Transaction
{
    public string Type { get; set; }
    public double Amount { get; set; }
    public string? Recipient { get; set; }
    public DateTime Date { get; set; } = DateTime.Now;

    public Transaction(string type, double amount, string? recipient = null)
    {
        Type = type;
        Amount = amount;
        Recipient = recipient;
    }

    public override string ToString()
    {
        return $"{Date}: {Type} - ${Amount}" + (Recipient != null ? $" to {Recipient}" : "");
    }
}
