public class CustomerService
{
    private readonly Queue<Customer> _queue;
    private readonly int _maxSize;

    public CustomerService(int maxSize)
    {
        // If size is invalid (<= 0), default to 10
        _maxSize = maxSize <= 0 ? 10 : maxSize;
        _queue = new Queue<Customer>();
    }

    public void AddNewCustomer(string name, string accountId, string problem)
    {
        if (_queue.Count >= _maxSize)
        {
            Console.WriteLine("Error: Queue is full! Cannot add more customers.");
            return;
        }
        _queue.Enqueue(new Customer(name, accountId, problem));
        Console.WriteLine($"Added: {name} - {problem}");
    }

    public void ServeCustomer()
    {
        if (_queue.Count == 0)
        {
            Console.WriteLine("Error: Queue is empty! No customers to serve.");
            return;
        }
        var customer = _queue.Dequeue();
        Console.WriteLine($"Serving: {customer.Name} (Account: {customer.AccountId}) - {customer.Problem}");
    }
}

public class Customer
{
    public string Name { get; set; }
    public string AccountId { get; set; }
    public string Problem { get; set; }

    public Customer(string name, string accountId, string problem)
    {
        Name = name;
        AccountId = accountId;
        Problem = problem;
    }
}