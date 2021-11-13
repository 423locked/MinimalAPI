var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSingleton<CustomerRepository>();

var app = builder.Build();

app.MapGet("/customers", ([FromServices] CustomerRepository repo) =>
{
    return repo.GetAll();
});

app.MapGet("/customers/{id}", ([FromServices] CustomerRepository repo, Guid id) =>
{
    var customer = repo.GetById(id);
    return customer is not null ? Results.Ok(customer) : Results.NotFound();
});

app.MapPost("/customers/", ([FromServices] CustomerRepository repo, Customer customer) =>
{
    repo.Create(customer);
    return Results.Created($"/customers/{customer.Id}", customer);
});

app.MapPut("/customers/{id}", ([FromServices] CustomerRepository repo, Guid id, Customer updatedCustomer) =>
{
    var customer = repo.GetById(id);
    if (customer is null) 
        return Results.NotFound();

    repo.Update(updatedCustomer);
    return Results.Ok(updatedCustomer);
});

app.MapDelete("/customers/{id}", ([FromServices] CustomerRepository repo, Guid id) =>
{
    repo.Remove(id);
    return Results.Ok();
});

app.Run();