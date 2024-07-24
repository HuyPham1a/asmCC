namespace ConsoleApp1;

public class Program
{
    private static BankSystem bankSystem = new BankSystem();

    public static void Main(string[] args)
    {
        while (true)
        {
            ShowMainMenu();
        }
    }

    private static void ShowMainMenu()
    {
        Console.WriteLine("---- Ngân hàng Spring Hero Bank ----");
        Console.WriteLine("1. Đăng ký tài khoản.");
        Console.WriteLine("2. Đăng nhập hệ thống.");
        Console.WriteLine("3. Thoát.");
        Console.Write("Nhập lựa chọn của bạn (1,2,3): ");
        
        var choice = Console.ReadLine();
        switch (choice)
        {
            case "1":
                Register();
                break;
            case "2":
                Login();
                break;
            case "3":
                Environment.Exit(0);
                break;
            default:
                Console.WriteLine("Lựa chọn không hợp lệ.");
                break;
        }
    }

    private static void Register()
    {
        Console.Write("Nhập username: ");
        string username = Console.ReadLine();
        Console.Write("Nhập password: ");
        string password = Console.ReadLine();
        Console.Write("Nhập họ tên: ");
        string fullName = Console.ReadLine();
        Console.Write("Nhập số điện thoại: ");
        string phoneNumber = Console.ReadLine();

        bankSystem.RegisterUser(username, password, fullName, phoneNumber);
    }

    private static void Login()
    {
        Console.Write("Nhập username: ");
        string username = Console.ReadLine();
        Console.Write("Nhập password: ");
        string password = Console.ReadLine();

        if (bankSystem.Login(username, password))
        {
            Console.WriteLine("Đăng nhập thành công.");
            var user = bankSystem.GetCurrentUser();
            if (user != null)
            {
                UserMenu(user);
            }
        }
        else
        {
            Console.WriteLine("Đăng nhập thất bại.");
        }
    }

    private static void UserMenu(User user)
    {
        while (true)
        {
            Console.WriteLine("---- Ngân hàng Spring Hero Bank ----");
            Console.WriteLine($"Chào mừng {user.FullName} quay trở lại. Vui lòng chọn thao tác.");
            Console.WriteLine("1. Gửi tiền.");
            Console.WriteLine("2. Rút tiền.");
            Console.WriteLine("3. Chuyển khoản.");
            Console.WriteLine("4. Truy vấn số dư.");
            Console.WriteLine("5. Thay đổi thông tin cá nhân.");
            Console.WriteLine("6. Thay đổi thông tin mật khẩu.");
            Console.WriteLine("7. Truy vấn lịch sử giao dịch.");
            Console.WriteLine("8. Thoát.");
            Console.Write("Nhập lựa chọn của bạn (Từ 1 đến 8): ");
            
            var choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    Deposit(user);
                    break;
                case "2":
                    Withdraw(user);
                    break;
                case "3":
                    Transfer(user);
                    break;
                case "4":
                    Console.WriteLine($"Số dư hiện tại: ${user.Balance}");
                    break;
                case "5":
                    UpdatePersonalInfo(user);
                    break;
                case "6":
                    UpdatePassword(user);
                    break;
                case "7":
                    ShowTransactionHistory(user);
                    break;
                case "8":
                    bankSystem.Logout();
                    return;
                default:
                    Console.WriteLine("Lựa chọn không hợp lệ.");
                    break;
            }
        }
    }

    private static void Deposit(User user)
    {
        Console.Write("Nhập số tiền gửi: ");
        if (double.TryParse(Console.ReadLine(), out double amount))
        {
            user.Deposit(amount);
            Console.WriteLine("Gửi tiền thành công.");
        }
        else
        {
            Console.WriteLine("Số tiền không hợp lệ.");
        }
    }

    private static void Withdraw(User user)
    {
        Console.Write("Nhập số tiền rút: ");
        if (double.TryParse(Console.ReadLine(), out double amount))
        {
            if (user.Withdraw(amount))
            {
                Console.WriteLine("Rút tiền thành công.");
            }
            else
            {
                Console.WriteLine("Số dư không đủ.");
            }
        }
        else
        {
            Console.WriteLine("Số tiền không hợp lệ.");
        }
    }

    private static void Transfer(User sender)
    {
        Console.Write("Nhập username người nhận: ");
        string recipientUsername = Console.ReadLine();
        Console.Write("Nhập số tiền chuyển: ");
        if (double.TryParse(Console.ReadLine(), out double amount))
        {
            var recipient = bankSystem.GetCurrentUser();
            if (recipient != null && recipient != sender)
            {
                sender.Transfer(recipient, amount);
                Console.WriteLine("Chuyển khoản thành công.");
            }
            else
            {
                Console.WriteLine("Người nhận không hợp lệ.");
            }
        }
        else
        {
            Console.WriteLine("Số tiền không hợp lệ.");
        }
    }

    private static void UpdatePersonalInfo(User user)
    {
        Console.Write("Nhập họ tên mới: ");
        string fullName = Console.ReadLine();
        Console.Write("Nhập số điện thoại mới: ");
        string phoneNumber = Console.ReadLine();

        user.UpdatePersonalInfo(fullName, phoneNumber);
        Console.WriteLine("Cập nhật thông tin cá nhân thành công.");
    }

    private static void UpdatePassword(User user)
    {
        Console.Write("Nhập mật khẩu mới: ");
        string newPassword = Console.ReadLine();
        user.UpdatePassword(newPassword);
        Console.WriteLine("Cập nhật mật khẩu thành công.");
    }

    private static void ShowTransactionHistory(User user)
    {
        int page = 1;
        int pageSize = 5;

        while (true)
        {
            bankSystem.DisplayTransactionHistory(user, page, pageSize);
            Console.Write("Nhập lựa chọn (>, <, ESC): ");
            var input = Console.ReadLine();
            if (input == ">")
            {
                page++;
                if (page > Math.Ceiling((double)user.Transactions.Count / pageSize))
                {
                    page = 1;
                }
            }
            else if (input == "<")
            {
                page--;
                if (page < 1)
                {
                    page = (int)Math.Ceiling((double)user.Transactions.Count / pageSize);
                }
            }
            else if (input.ToUpper() == "ESC")
            {
                return;
            }
            else
            {
                Console.WriteLine("Lựa chọn không hợp lệ.");
            }
        }
    }
}
