using Common.Database.Transaction;
using Common.Entity;
using Common.Entity.Contracts;
using Common.Util.Validators;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

app.MapPost("/create", ([FromBody] JsonProducts jsonProduct) =>
{
    var (isValid, validationErrors) = ModelValidator.Validate(jsonProduct);

    if (!isValid)
        return Results.BadRequest(validationErrors);

    try
    {
        Product product = new Product()
        {
            Name = jsonProduct.Name,
            Code = jsonProduct.Code,
            CreationDate = DateTime.Now,
            InventoryBalance = 0,
            Price = (decimal)jsonProduct.Price
        };

        new ProductTransaction().Insert(product);

        return Results.Ok(jsonProduct);
    }
    catch (DbUpdateException dbUpdateException)
    {
        if (dbUpdateException.InnerException is SqlException sqlException && sqlException.Number == 2627)
            return Results.BadRequest("MENSAGEM");
        return Results.BadRequest();
    }
    catch (Exception)
    {
        return Results.BadRequest();
    }
});

app.MapGet("/all", () => {
    var products = new ProductTransaction().GettAllProducts();

    return Results.Ok(products);
});

app.Run();
