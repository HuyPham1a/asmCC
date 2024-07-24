namespace ConsoleApp1;

public class BankSystem
{
    private List<User> users = new List<User>();
    private User? currentUser;
    
    public void RegisterUser(string username, string password, string fullName, string phoneNumber)
    {
        if (users.Any(u => u.Username == username))
        {
            Console.WriteLine("Username already exists.");
            return;
        }

        users.Add(new User(username, password, fullName, phoneNumber));
        Console.WriteLine("User registered successfully.");
    }

    public bool Login(string username, string password)
    {
        currentUser = users.FirstOrDefault(u => u.Username == username && u.Password == password);
        return currentUser != null;
    }

    public void Logout()
    {
        currentUser = null;
    }

    public User? GetCurrentUser()
    {
        return currentUser;
    }
    public void DisplayTransactionHistory(User user, int page, int pageSize)
    {
        var transactions = user.Transactions.Skip((page - 1) * pageSize).Take(pageSize).ToList();
        foreach (var transaction in transactions)
        {
            Console.WriteLine(transaction);
        }

        Console.WriteLine($"Page {page} of {Math.Ceiling((double)user.Transactions.Count / pageSize)}");
    }
}
