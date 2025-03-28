using System.Data.Common;
using Common.Database.Transaction;
using Common.Database.Transaction.Base;
using Common.Entity;
using Common.Entity.Contracts;
using Common.Util;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using MiniValidation;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "_myAllowSpecificOrigins",
                      policy =>
                      {
                          policy.WithOrigins("http://localhost:4200/");
                      });
});

var app = builder.Build();

app.UseCors(x => x.AllowAnyOrigin().AllowAnyHeader().SetIsOriginAllowed(origin => true));

app.MapPost("/create", ([FromBody] JsonProducts jsonProduct) =>
{
    var isValid = MiniValidator.TryValidate(jsonProduct, out var errors);

    if (!isValid)
    {
        string errorMessage = string.Join(" | ", errors.Values.Select(values => values.First()));
        return Results.BadRequest( new { errorMessage } );
    }
    
    using var dbTransaction = new DatabaseTransaction();
    try
    {
        Product product = new Product()
        {
            Name = jsonProduct.Name,
            Code = jsonProduct.Code,
            CreationDate = DateTime.Now,
            InventoryBalance = jsonProduct.InventoryBalance,
            Price = jsonProduct.Price
        };

        new ProductTransaction(dbTransaction).Insert(product);
        dbTransaction.Commit();

        return Results.Ok(jsonProduct);
    }
    catch (DbUpdateException dbUpdateException)
    {
        dbTransaction.Rollback();
        if (dbUpdateException.InnerException is SqlException sqlException && sqlException.Number == 2627)
            return Results.BadRequest(new { errorMessage = string.Format(ApiMessage.EXC02, jsonProduct.Code) });
        return Results.BadRequest(new { errorMessage = ApiMessage.EXC01 });
    }
    catch (Exception)
    {
        dbTransaction.Rollback();
        return Results.BadRequest(new { errorMessage = ApiMessage.EXC01 });
    }
});

app.MapGet("/all", () => {
    try
    {
        using var dbTransaction = new DatabaseTransaction();
        var products = new ProductTransaction(dbTransaction).GettAllProducts();

        return Results.Ok(products);
    }
    catch (Exception)
    {
        return Results.BadRequest(new { errorMessage = ApiMessage.EXC01 });
    }
});

app.Run();
