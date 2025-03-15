using Common.Database.Transaction;
using Common.Database.Transaction.Base;
using Common.Entity;
using Common.Entity.Contracts;
using Microsoft.AspNetCore.Mvc;
using MiniValidation;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapPost("/create", ([FromBody] JsonInvoice jsonInvoice) => {

    var isValid = MiniValidator.TryValidate(jsonInvoice, out var errors);

    string errorMessage = string.Join(" | ", errors.Values.Select(values => values.First()));
    if (!isValid)
        throw new Exception(errorMessage);

    using var dbTransaction = new DatabaseTransaction();

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
});

app.Run();
