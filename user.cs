namespace ConsoleApp1;

public class User
{
    public string Username { get; set; }
    public string Password { get; set; }
    public string FullName { get; set; }
    public string PhoneNumber { get; set; }
    public double Balance { get; set; }
    public List<Transaction> Transactions { get; set; } = new List<Transaction>();

    public User(string username, string password, string fullName, string phoneNumber)
    {
        Username = username;
        Password = password;
        FullName = fullName;
        PhoneNumber = phoneNumber;
        Balance = 0.0;
    }

    public void Deposit(double amount)
    {
        Balance += amount;
        Transactions.Add(new Transaction("Deposit", amount));
    }

    public bool Withdraw(double amount)
    {
        if (amount <= Balance)
        {
            Balance -= amount;
            Transactions.Add(new Transaction("Withdraw", amount));
            return true;
        }
        return false;
    }

    public void Transfer(User recipient, double amount)
    {
        if (Withdraw(amount))
        {
            recipient.Deposit(amount);
            Transactions.Add(new Transaction("Transfer", amount, recipient.Username));
        }
    }

    public void UpdatePersonalInfo(string fullName, string phoneNumber)
    {
        FullName = fullName;
        PhoneNumber = phoneNumber;
    }

    public void UpdatePassword(string newPassword)
    {
        Password = newPassword;
    }
}
