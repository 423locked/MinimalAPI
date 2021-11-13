﻿public record Customer(Guid Id, string FullName);

public class CustomerRepository
{
    private readonly Dictionary<Guid, Customer> _customers = new();
    
    public void Create(Customer customer)
    {
        if (customer is null) return;
        _customers[customer.Id] = customer;
    }

    public Customer GetById(Guid id) => _customers[id];

    public List<Customer> GetAll() => _customers.Values.ToList();

    public void Remove(Guid id) => _customers.Remove(id);

    public void Update(Customer newCustomer)
    {
        var existingCustomer = GetById(newCustomer.Id);
        if (existingCustomer is null) return;
        _customers[newCustomer.Id] = newCustomer;
    }
}
