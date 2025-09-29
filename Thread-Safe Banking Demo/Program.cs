

using System.Collections.Concurrent;

public class BankAccount
{
    private decimal _balance;
    private readonly object _lock = new object();

    // Thread-safe transaction log
    private readonly ConcurrentQueue<string> _transactions = new ConcurrentQueue<string>();

    public BankAccount(decimal initialBalance)
    {
        _balance = initialBalance;
    }

    public void Deposit(decimal amount)
    {
        lock (_lock)
        {
            _balance += amount;
            _transactions.Enqueue($"[{DateTime.Now:HH:mm:ss}] Deposit {amount:C} | Balance: {_balance:C}");
        }
    }

    public void Withdraw(decimal amount)
    {
        lock (_lock)
        {
            if (_balance >= amount)
            {
                _balance -= amount;
                _transactions.Enqueue($"[{DateTime.Now:HH:mm:ss}] Withdraw {amount:C} | Balance: {_balance:C}");
            }
            else
            {
                _transactions.Enqueue($"[{DateTime.Now:HH:mm:ss}] Withdraw FAILED {amount:C} | Balance: {_balance:C}");
            }
        }
    }

    public decimal GetBalance()
    {
        lock (_lock)
        {
            return _balance;
        }
    }

    public void PrintTransactions()
    {
        Console.WriteLine("\n=== Transaction Log ===");
        foreach (var t in _transactions)
        {
            Console.WriteLine(t);
        }
    }




    public class Program
    {
        static async Task Main(string[] args)
        {
            var account = new BankAccount(1000m);

            // 10 Clients
            Task[] tasks = new Task[10];

            for (int i = 0; i < tasks.Length; i++)
            {
                int clientId = i + 1;
                tasks[i] = Task.Run(() =>
                {
                    var rand = new Random();

                    for (int j = 0; j < 5; j++)
                    {
                        decimal amount = rand.Next(50, 200);

                        if (rand.Next(2) == 0)
                            account.Deposit(amount);
                        else
                            account.Withdraw(amount);

                        Thread.Sleep(rand.Next(50, 200)); // simulate some delay
                    }

                    Console.WriteLine($"Client {clientId} finished transactions.");
                });
            }

            await Task.WhenAll(tasks);

            Console.WriteLine($"\nFinal Balance: {account.GetBalance():C}");
            account.PrintTransactions();
        }
    }
}

