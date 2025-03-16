using Common.Database.Transaction;
using Common.Database.Transaction.Base;
using Common.Entity;
using Common.Entity.Contracts;
using Common.Util;
using Microsoft.AspNetCore.Mvc;
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

app.MapPost("/create", ([FromBody] JsonInvoice jsonInvoice) => {

    var isValid = MiniValidator.TryValidate(jsonInvoice, out var errors);

    if (!isValid)
    {
        string errorMessage = string.Join(" | ", errors.Values.Select(values => values.First()));
        return Results.BadRequest(new { errorMessage });
    }

    using var dbTransaction = new DatabaseTransaction();
    try
    {
        var products = jsonInvoice.ProductsInvoice.Select(productInvoice =>
        {
            var product = new ProductTransaction(dbTransaction).GetProductByCode(productInvoice.Code)
                ?? throw new Exception();

            return product;
        }).ToList();

        Invoice invoice = new Invoice()
        {
            State = InvoiceState.Open,
            CreationDate = DateTime.Now
        };

        new InvoiceTransaction(dbTransaction).Insert(invoice);

        var invoiceProducts = products.Select(product => new InvoiceProduct()
        {
            InvoiceId = invoice.Id,
            ProductId = product.Id,
            CreationDate = DateTime.Now
        }).ToList();

        new InvoiceProductsTransaction(dbTransaction).InsertMany(invoiceProducts);

        dbTransaction.Commit();

        return Results.Ok(jsonInvoice);
    }
    catch (Exception)
    {
        dbTransaction.Commit();
        return Results.BadRequest(new { errorMessage = ApiMessage.EXC01 });
    }
});

app.MapGet("/allInvoices", () =>
{
    using var dbTransaction = new DatabaseTransaction();
    try
    {
        var invoices = new InvoiceTransaction(dbTransaction).GetAllInvoices();

        return Results.Ok(invoices);
    }
    catch (Exception)
    {
        return Results.BadRequest(new { errorMessage = ApiMessage.EXC01 });
    }
});


app.MapGet("/invoiceProductsByInvoiceId/{id}", (int id) =>
{
    using var dbTransaction = new DatabaseTransaction();
    try
    {
        var invoiceProducts = new InvoiceProductsTransaction(dbTransaction).GetInvoiceProductsByInvoiceId(id);
        invoiceProducts.ForEach((invoice) =>
        {
            invoice.Product = new ProductTransaction(dbTransaction).GetProductById(invoice.ProductId);
            invoice.Invoice = new InvoiceTransaction(dbTransaction).GetInvoiceById(invoice.InvoiceId);
        });

        return Results.Ok(invoiceProducts);
    }
    catch (Exception)
    {
        return Results.BadRequest(new { errorMessage = ApiMessage.EXC01 });
    }
});


app.Run();
