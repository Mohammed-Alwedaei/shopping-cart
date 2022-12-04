using Microsoft.AspNetCore.Mvc;
using Products.API.Models;
using Products.API.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<ProductsStoreDatabaseConfig>(
    builder.Configuration.GetSection("ProductsDatabase"));

builder.Services.AddHttpClient("ShoppingCart");
builder.Services.AddSingleton<ProductsService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

var products = app.MapGroup("/api/products");

products.MapGet("/{id}", async ([FromServices] ProductsService productsService, string id) =>
{
    if (string.IsNullOrEmpty(id)) return Results.BadRequest();

    var product = await productsService.GetProductAsync(id);

    if (product is null) return Results.NotFound();

    return Results.Ok(product);
});

products.MapGet("/", async ([FromServices] ProductsService productsService) =>
{
    var products = await productsService.GetProductsAsync();

    if (products.Any() is false) return Results.NotFound();

    return Results.Ok(products);
});

products.MapPost("/", async ([FromServices] ProductsService productsService, [FromServices] HttpClient client, ProductModel product) =>
{
    try
    {
        await productsService.CreateProductAsync(product);

        client.BaseAddress = new Uri("https://localhost:7050");

        await client.PostAsJsonAsync("/api/products", product);

        return Results.Ok(product);
    }
    catch (Exception exception)
    {
        await productsService.RemoveProductAsync(product.Id);
        return Results.Problem(exception.Message);
    }
});

products.MapDelete("/{id}", async ([FromServices] ProductsService productsService, string id) =>
{
    await productsService.RemoveProductAsync(id);

    return Results.Ok(id);
});

app.Run();
